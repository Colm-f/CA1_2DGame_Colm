using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Animator animator;
    public float speed;
    public float distance;
    private Rigidbody2D rb;
    public int strength = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(this.transform.position, player.transform.position);

        if (distance > 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var proj = collision.gameObject.GetComponent<Projectile>();
        var hammer = collision.gameObject.GetComponent<HammerProjectile>();
        if (proj != null || hammer != null)
        {
            rb.linearVelocity = new Vector2(0, 0);
            if (proj != null) { 
                Hit();
            }
        }
    }

    private void Hit()
    {
        strength--;
        if (strength <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
