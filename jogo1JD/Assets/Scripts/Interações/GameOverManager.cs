using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private DataSO dataSO;
    public void RestartGame()
    {
        dataSO.Vida = 100;
        if (dataSO.Value1 > 0)
        {
            dataSO.Value = dataSO.Value1;
        }
        else
        {
            dataSO.Value = 0;
        }
        SceneManager.LoadScene(dataSO.Level); 
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