using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] Player player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() { 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && player.playerOrbCount == 3) {
            SceneManager.LoadScene("Victory");
        }
    }
}
