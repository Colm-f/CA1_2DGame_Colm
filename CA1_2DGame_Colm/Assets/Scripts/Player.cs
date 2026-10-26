using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] private GameManager gm;
    private Rigidbody2D rb;
    private Animator animator;
    private float inputX, inputY;
    private int direction = 1;
    private Vector2 move;
    public float speed = 0.5f;
    public int playerFlameCount = 0;
    private int lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    public void CollectFlame()
    {
        playerFlameCount++;
        gm.UpdateScore(playerFlameCount);
    }

    private void Fire()
    {
        GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.identity);
        Projectile pro = go.GetComponent<Projectile>();
        pro.Launch(new Vector2(-direction, 0), 600);
        playerFlameCount--;
        gm.UpdateScore(playerFlameCount);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            lives--;
            gm.UpdateLives(lives);
            //if (lives == 0)
            //{
            //    SceneManager.LoadScene("Menu");
            //}
        }

        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(inputX * speed, inputY * speed, 0f);
    }

    


}
