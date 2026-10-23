using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField]public GameObject projectilePrefab;
    private Rigidbody2D rb;
    private Animator animator;
    private float inputX, inputY;
    private int direction = 1;
    private Vector2 move;
    public float speed = 0.5f;
    public int playerFlameCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Fire()
    {
        GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.identity);
        Projectile pro = go.GetComponent<Projectile>();
        pro.Launch(new Vector2(-direction, 0), 600);
        playerFlameCount--;
    }

    // Update is called once per frame
    void Update()
    {
        move = InputSystem.actions["move"].ReadValue<Vector2>();
        if (move.x != 0)
        {
            direction = move.x < 0 ? 1 : -1;
            animator.SetInteger("direction", direction);
        }
        animator.SetFloat("move", move.x);

        if (InputSystem.actions["Attack"].IsPressed() && playerFlameCount > 0)
        {
            InputSystem.actions["Attack"].Reset();
            Fire();
        }

    }

    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(inputX * speed, inputY * speed, 0f);
    }

    public void CollectFlame() {
        playerFlameCount++;
    }

    
}
