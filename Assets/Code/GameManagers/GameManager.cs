using System.Collections;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Description("This value is describe how long we need to wait before exit game after Player died")]
    [SerializeField] private float secondsBeforeExit = 3f;
    
    

    private void OnEnable()
    {
        GameEvents.onPlayerDeath.AddListener(StopGame);
    }

    private void OnDisable()
    {
        GameEvents.onPlayerDeath.RemoveListener(StopGame);
    }

    IEnumerator Start()
    {
        //Here we can add code to comunicate with cloud services e.t.c.
        Debug.Log("Game Started from GameManager");

        //TODO: Ask game designer if we need those default dragons.
        int defualtDragonsCount = FindObjectsOfType<DragonAI>().Length;

        GameStats.Instance.Initialize(defualtDragonsCount);

        GameEvents.onGameLoading.Invoke();

        //TODO: Bind this to UI button
        yield return new WaitForSeconds(3f); //Simulate loading sreen
        GameEvents.onGameStart.Invoke();
    }

    void StopGame()
    {
        //Here we can implement cloud services or save system e.t.c.
        Debug.Log("Game Ended from GameManager");

        GameStats.Instance.Utilize();
    
        //IDEA: We can implement "Extra live" system here
        StartCoroutine(StopGameCoroutine());
    }

    IEnumerator StopGameCoroutine()
    {
        yield return new WaitForSeconds(secondsBeforeExit);
        GameEvents.onGameEnd.Invoke();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Debug.Log("Level complete, stopping playmode\n");
    }
}