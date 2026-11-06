using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text FireballsCollected;
    public TMP_Text OrbsCollected;
    public TMP_Text HammerText;
    public TMP_Text ExitText;
    [SerializeField] Player player;
    [SerializeField] Image[] images;
    [SerializeField] Image hammer;
    private float hammerTime;
    private bool hammerOn;
    private int totalOrbs = 3;

    void Start()
    {
        hammer.enabled = false;
        HammerText.enabled = false;
        ExitText.enabled = false;
    }

    void Update() 
    {
        if (hammerOn) {
            hammerTime += Time.deltaTime;
            if (hammerTime > 4f) {
                HammerText.enabled = false;
            }
        }
    }
    public void UpdateFlameScore(int playerFlameCount)
    {
        FireballsCollected.text = playerFlameCount.ToString();
    }

    public void UpdateOrbScore(int playerOrbCount) {
        OrbsCollected.text = playerOrbCount.ToString() + " / " + totalOrbs;
        if (playerOrbCount == 3) {
            ExitText.enabled = true;
        }
    }

    public void showHammer() {
        hammer.enabled = true;
        HammerText.enabled = true;
        hammerOn = true;
    }

    public void UpdateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 1:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = true;
                break;
            case 2:
                images[0].enabled = false;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
            case 3:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        }

    }
}