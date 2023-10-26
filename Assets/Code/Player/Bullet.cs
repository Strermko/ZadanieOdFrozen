using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] int damage = 10;

    private Rigidbody rigidBody;

    readonly Color[] colors = { Color.yellow, Color.red, Color.white, Color.blue, Color.green };

    void OnEnable()
    {
        SetRandomColor();
        rigidBody = GetComponent<Rigidbody>();
        
        //Destroy bullet after 10 seconds, in case we start shoot to the sky
        Destroy(gameObject, 10f);
    }

    public void Init(Vector3 velocity)
    {
        rigidBody.velocity += velocity;
    }

    void SetRandomColor()
    {
        meshRenderer.material.color = colors[Random.Range(0, colors.Length)];
    }

    void OnCollisionEnter(Collision other)
    {
        rigidBody.useGravity = true;

        //TODO: replace component with IEnemy interface
        if (other.gameObject.TryGetComponent<DragonAI>(out var dragon))
            dragon.TakeDamage(damage);

        GameEvents.onBulletDestroy.Invoke();
        Destroy(gameObject);
    }
}