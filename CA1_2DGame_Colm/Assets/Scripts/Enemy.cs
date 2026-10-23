using UnityEngine;
using UnityEngine.Windows;

//public class Enemy : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    private Animator animator;
//    private bool isWalking = false;
//    private float elapsedTime;
//    [SerializeField] private float walkTime;
//    [SerializeField] private float idleTime;
//    private float fireTimer = 0.5f;
//    private float fireCountdown;
//    private int direction = 1;
//    [SerializeField] private GameObject projectile;
//    private bool isInvincible;
//    private float invincibleCount;
//    private int strength = 3;
//    private int enemyFlameCount = 0;
//    public float speed = 0.1f;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        //animator = GetComponent<Animator>();
//        //animator.SetInteger("direction", direction);
//        //animator.SetInteger("move", 0);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isWalking && elapsedTime < walkTime)
//        {
//            Vector2 position = rb.transform.position;
//            position.x += direction * Time.deltaTime;
//            rb.transform.position = position;
//        }
//        else if (!isWalking && elapsedTime > walkTime)
//        {
//            isWalking = true;
//            elapsedTime = 0;
//            direction *= -1;
//            //animator.SetFloat("move", direction);
//            //animator.SetInteger("direction", direction);
//        }
//        else if (isWalking && elapsedTime > walkTime)
//        {
//            isWalking = false;
//            elapsedTime = 0;
//            //animator.SetFloat("move", 0);
//        }

//        elapsedTime += Time.deltaTime;

//        if (isInvincible)
//        {
//            invincibleCount += Time.deltaTime;
//            if (invincibleCount > 1f)
//            {
//                isInvincible = false;
//            }
//        }
//    }

//    private void FixedUpdate()
//    {
//        inputX = Input.GetAxis("Horizontal");
//        inputY = Input.GetAxis("Vertical");

//        transform.Translate(inputX * speed, inputY * speed, 0f);
//        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, new Vector2(direction, 0),
//            5f, LayerMask.GetMask("Player"));

//        if (hit.collider != null)
//        {
//            if (hit.collider.GetComponent<Player>() != null && enemyFlameCount > 0)
//            {
//                Fire();
//            }
//        }

//        fireCountdown += Time.fixedDeltaTime;
//    }

//    public void Fire()
//    {
//        if (fireCountdown > fireTimer)
//        {
//            GameObject go = Instantiate(projectile, rb.transform.position, Quaternion.identity);
//            Projectile pro = go.GetComponent<Projectile>();
//            pro.Launch(new Vector2(direction, 0), 300);
//            fireCountdown = 0;
//            enemyFlameCount--;
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.GetComponent<Projectile>() != null)
//        {
//            rb.linearVelocity = new Vector2(0, 0);
//            Hit();
//        }
//    }

//    private void Hit()
//    {
//        if (!isInvincible)
//        {
//            strength--;
//            if (strength <= 0)
//            {
//                Destroy(this.gameObject);
//            }

//            isInvincible = true;
//        }

//    }

//    public void CollectFlame()
//    {
//        enemyFlameCount++;
//    }
//}

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distance;
    private int enemyFlameCount = 0;
    [SerializeField] private GameObject projectile;
    private float fireTimer = 0.5f;
    private float fireCountdown;
    private Rigidbody2D rb;
    private int direction;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;

        if (distance > 4) {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void FixedUpdate()
    {
        if (distance > 2 && enemyFlameCount > 0)
        {
            Fire();
        }

        fireCountdown += Time.fixedDeltaTime;
    }

    public void Fire()
    {
        if (fireCountdown > fireTimer)
        {
            GameObject go = Instantiate(projectile, rb.transform.position, Quaternion.identity);
            Projectile pro = go.GetComponent<Projectile>();
            pro.Launch(new Vector2(direction, 0), 300);
            fireCountdown = 0;
            enemyFlameCount--;
        }
    }

    public void CollectFlame()
    {
        enemyFlameCount++;
    }
}