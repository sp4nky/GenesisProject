using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.menu = this;
    }

    public void GoToLevel01()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLevel02()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToLevel03()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToLevelCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
