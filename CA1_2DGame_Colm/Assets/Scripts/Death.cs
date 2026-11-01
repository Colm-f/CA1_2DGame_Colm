using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
