using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] int damage = 10;
    [SerializeField] float defaultLifeTime = 5f;

    private Rigidbody rigidBody;
    private Transform myTransform;
    private bool isDeadly;
    readonly Color[] colors = { Color.yellow, Color.red, Color.white, Color.blue, Color.green };

    void OnEnable()
    {
        SetRandomColor();
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
    }

    public void Init(Vector3 velocity, Vector3 bulletSpawnPosition)
    {
        gameObject.SetActive(true);
        rigidBody.useGravity = false;
        rigidBody.freezeRotation = true;
        isDeadly = true;
        
        myTransform.position = bulletSpawnPosition;
        rigidBody.velocity = velocity;
        
        StartCoroutine(DisableAfterTime(defaultLifeTime));
    }

    void SetRandomColor()
    {
        meshRenderer.material.color = colors[Random.Range(0, colors.Length)];
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isDeadly)
            return;
        
        rigidBody.useGravity = true;
        rigidBody.freezeRotation = false;
        isDeadly = false;
        
        //TODO: replace component with IEnemy interface
        if (other.gameObject.TryGetComponent<DragonAI>(out var dragon))
            dragon.TakeDamage(damage);
        
        StopAllCoroutines();
        StartCoroutine(DisableAfterTime(2f));
    }

    IEnumerator DisableAfterTime(float timeBeforeDestroy)
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        
        myTransform.localRotation = Quaternion.identity;
        myTransform.localPosition = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
        
        gameObject.SetActive(false);
    }
    
}