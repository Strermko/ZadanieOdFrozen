using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class DragonAI : MonoBehaviour
{
    [SerializeField] int hp = 30;

    [Range(0, 20)]
    [SerializeField]
    private int chaisingSpeed = 5;

    [SerializeField] float timeBeforeBodyDestroy = 5f;


    private PlayerController player;
    private DragonAnimator dragonAnimator;
    private Rigidbody rigidBody;

    enum State
    {
        Chasing,
        Attacking,
        Dead,
    }

    private State state;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        dragonAnimator = GetComponent<DragonAnimator>();
        rigidBody = GetComponent<Rigidbody>();

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
    }

    void Update()
    {
        //TODO: replace with state machine after implementing IEnemy interface
        //QUESTION: Is this code from CodeMonkey? :D
        switch (state)
        {
            case State.Chasing:
                transform.LookAt(player.transform);
                dragonAnimator.PlayWalkAnimation();
                transform.Translate(Vector3.forward * (chaisingSpeed * Time.deltaTime));
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
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

        GameEvents.onDragonDeath.Invoke();
    }

    IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(timeBeforeBodyDestroy);
        Destroy(gameObject);
    }
}