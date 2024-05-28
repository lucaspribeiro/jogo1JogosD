using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{   
    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1"); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}