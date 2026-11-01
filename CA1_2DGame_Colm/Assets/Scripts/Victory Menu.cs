using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Menu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
