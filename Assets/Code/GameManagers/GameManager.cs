using UnityEngine;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        //Here we can add code to comunicate with cloud services e.t.c.
        Debug.Log("Game Started from GameManager");

        //TODO: Ask game designer if we need those default dragons.
        int defualtDragonsCount = FindObjectsOfType<DragonAI>().Length;

        GameStats.Instance.Initialize(defualtDragonsCount);

        GameEvents.onGameStart.Invoke();
    }

    private void OnApplicationQuit()
    {
        //Here we can implement cloud services or save system e.t.c.
        Debug.Log("Game Ended from GameManager");

        GameStats.Instance.Utilize();

        GameEvents.onGameEnd.Invoke();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Level complete, stopping playmode\n");
    }
}