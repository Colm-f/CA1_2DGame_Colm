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
    [SerializeField] public GameObject hammerPrefab;
    [SerializeField] private GameManager gm;
    [SerializeField] public Enemy enemy;
    [SerializeField] private Goal goal;
    private Rigidbody2D rb;
    private Animator animator;
    private float inputX, inputY;
    private int direction = 1;
    private Vector2 move;
    public float speed = 0.5f;
    public int playerFlameCount = 0;
    public int playerOrbCount = 0;
    private int lives = 3;
    private bool isInvincible;
    private float invincibleTime;
    private AudioSource aSource;
    private bool playing = false;
    private bool destroy = false;
    private bool isDead = false;
    private float deathTime = 0;
    private bool down = false;
    private bool up = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    public void CollectFlame()
    {
        playerFlameCount++;
        gm.UpdateFlameScore(playerFlameCount);
    }

    public void CollectOrb() {
        playerOrbCount++;
        gm.UpdateOrbScore(playerOrbCount);
    }

    public void Heal() {
        if (!isInvincible) { 
            lives++;
            gm.UpdateLives(lives);
        }
    }

    public void CollectHammer() {
        destroy = true;
        gm.showHammer();
    }

    private void Fire()
    {
        GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.identity);
        Projectile pro = go.GetComponent<Projectile>();
        pro.Launch(new Vector2(-direction, 0), 600);
        playerFlameCount--;
        gm.UpdateFlameScore(playerFlameCount);
    }
    private void FireHammer() {
        GameObject go = Instantiate(hammerPrefab, rb.transform.position, Quaternion.identity);
        HammerProjectile pro = go.GetComponent<HammerProjectile>();
        pro.Launch(new Vector2(-direction, 0), 600);
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
        
        if (move.x != 0 && !playing) {
            //aSource.Play();
            playing = true;
        }
        if (move.x == 0 && playing) {
            //aSource.Pause();
            playing = false;
        }
        else if (move.x != 0 && playing) {
            //aSource.UnPause();
            playing = true;
        }

        if (InputSystem.actions["Attack"].IsPressed() && playerFlameCount > 0)
        {
            InputSystem.actions["Attack"].Reset();
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.F) && destroy) {
            FireHammer();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            down = true;
            animator.SetBool("down", down);
        }
        else {
            down = false;
            animator.SetBool("up", up);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            up = true;
            animator.SetBool("up", up);
        }
        else {
            up = false;
            animator.SetBool("up", up);
        }

        if (isInvincible) {
            invincibleTime += Time.deltaTime;

            if (invincibleTime > 1f) {
                isInvincible = false;
            }
        }

        if (lives <= 0)
        {
            isDead = true;
            animator.SetBool("dead", isDead);

            deathTime += Time.deltaTime;
            if (deathTime > 2f)
            {
                SceneManager.LoadScene("Death");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isInvincible) { 
                lives--;
                gm.UpdateLives(lives);
                isInvincible = true;
            }
        }
    }

    void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(inputX * speed, inputY * speed, 0f);
    }

    


}
