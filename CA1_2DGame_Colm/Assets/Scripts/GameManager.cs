using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text FireballsCollected;
    [SerializeField] Player player;
    [SerializeField] Image[] images;

    private void Start()
    {
        FireballsCollected.text = player.playerFlameCount.ToString();
    }
    public void UpdateScore(int playerFlameCount)
    {
        FireballsCollected.text = playerFlameCount.ToString();
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
            case 4:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        }

    }
}