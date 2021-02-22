using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public bool inverted = false;
    public Toggle toggle;

    public void Start()
    {
        if (PlayerPrefs.GetInt("inverted") == 1)
            toggle.isOn = true;
        else
            toggle.isOn = false;
    }

    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene", 0));
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("inverted", Convert.ToInt32(inverted));
    }

    public void Toggle()
    {
        inverted = !inverted;
    }
}
