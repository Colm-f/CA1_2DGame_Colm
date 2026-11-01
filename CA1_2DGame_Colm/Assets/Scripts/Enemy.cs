using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public float speed;
    public float distance;
    private Rigidbody2D rb;
    public bool isInvincible = false;
    private float invincibleCount;
    public int strength = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > 1)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (isInvincible)
        {
            invincibleCount += Time.deltaTime;
            if (invincibleCount > 1f)
            {
                isInvincible = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            rb.linearVelocity = new Vector2(0, 0);
            Hit();
        }
    }

    private void Hit()
    {
        if (!isInvincible)
        {
            strength--;
            if (strength <= 0)
            {
                Destroy(this.gameObject);
            }

            isInvincible = true;
        }

    }
}
