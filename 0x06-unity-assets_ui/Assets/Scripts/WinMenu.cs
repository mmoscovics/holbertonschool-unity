using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Next()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
