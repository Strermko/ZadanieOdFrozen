using System;
using System.Collections;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [SerializeField] int hp = 30;

    [Range(0, 20)]
    [SerializeField]
    private int chasingSpeed = 5;

    [SerializeField] float timeBeforeBodyDestroy = 5f;


    private PlayerController player;
    private DragonAnimator dragonAnimator;
    private Rigidbody rigidBody;
    private Transform myTransform;

    private bool initialized = false;

    enum State
    {
        Chasing,
        Attacking,
        Dead,
    }

    private State state;

    void OnEnable()
    {
        GameEvents.onGameStart.AddListener(Initialise);
    }

    void OnDisable()
    {
        GameEvents.onGameStart.RemoveListener(Initialise);
    }

    void Update()
    {
        //TODO: replace with state machine after implementing IEnemy interface
        //QUESTION: Is this code from CodeMonkey? :D
        if (!initialized)
            return;

        switch (state)
        {
            case State.Chasing:
                myTransform.LookAt(player.transform);
                dragonAnimator.PlayWalkAnimation();
                myTransform.Translate(Vector3.forward * (chasingSpeed * Time.deltaTime));
                if (Vector3.Distance(myTransform.position, player.transform.position) < 10)
                    state = State.Attacking;
                break;
            case State.Attacking:
                //TODO: Ask designer do we want to enable player to dodge dragon attack
                dragonAnimator.PlayAttackAnimation();
                player.Died();
                break;
            case State.Dead:
                //TODO: Ask game designer if we want to spawn something, or just destroy dragon
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trap"))
            PlayDead();
    }

    void Initialise()
    {
        try
        {
            state = State.Chasing;

            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var ren in renderers)
            {
                for (int i = 0; i < ren.materials.Length; i++)
                {
                    if (ren.materials[i] == null)
                        Debug.LogError($"Missing material on Dragon {name} renderer {ren.name} slot{i}");
                }
            }

            player = FindObjectOfType<PlayerController>();
            dragonAnimator = GetComponent<DragonAnimator>();
            rigidBody = GetComponent<Rigidbody>();
            myTransform = transform;

            initialized = true;
        }
        catch (Exception e)
        {
            Debug.LogError($"DragonAI failed to initialise with message: {e.Message}");
            initialized = false;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0 && !state.Equals(State.Dead))
        {
            PlayDead();
        }
    }

    void PlayDead()
    {
        state = State.Dead;
        dragonAnimator.PlayDeadAnimation();
        rigidBody.isKinematic = true;
        StartCoroutine(DestroyBody());

        GameEvents.onEnemyDeath.Invoke();
    }

    public void Spawn(Vector3 spawnPosition)
    {
        //TODO: Remove this line if no enemies on scene before game start
        if(!initialized) Initialise();
        
        gameObject.SetActive(true);
        state = State.Chasing;
        rigidBody.isKinematic = false;
        myTransform.position = spawnPosition;
    }

    IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(timeBeforeBodyDestroy);
        gameObject.SetActive(false);
        myTransform.localPosition = Vector3.zero;
        myTransform.localRotation = Quaternion.identity;
    }
}