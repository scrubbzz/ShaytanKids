using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuu : MonoBehaviour
{
    int index = 0;

    public void PlayGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene(index++);

    }

    public void GoTomainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayTutoriallevel()
    {
        Debug.Log("start");
        SceneManager.LoadScene("Tutorial polish");

    }
    
}
