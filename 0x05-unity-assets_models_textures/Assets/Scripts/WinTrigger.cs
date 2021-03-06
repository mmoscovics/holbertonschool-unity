﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;
    public Text text;
    public GameObject player;

    void Start()
    {
        timer = player.GetComponent<Timer>();
    }

    void OnTriggerEnter(Collider other)
    {
        timer.enabled = false;
        text.fontSize = 60;
        text.color = Color.green;
    }
}
