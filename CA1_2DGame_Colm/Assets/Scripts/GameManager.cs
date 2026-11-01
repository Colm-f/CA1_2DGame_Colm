using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text FireballsCollected;
    public TMP_Text OrbsCollected;
    [SerializeField] Player player;
    [SerializeField] Image[] images;

    void Start()
    {

    }

    void Update() 
    {
        
    }
    public void UpdateFlameScore(int playerFlameCount)
    {
        FireballsCollected.text = playerFlameCount.ToString();
    }

    public void UpdateOrbScore(int playerOrbCount) {
        OrbsCollected.text = playerOrbCount.ToString();
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