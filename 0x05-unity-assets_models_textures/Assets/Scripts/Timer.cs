using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;

    private float time;
    private float min, sec;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        min = Mathf.FloorToInt(time / 60f);
        sec = time - min * 60;
        timer.text = string.Format("{0:0}:{1:00.00}", min, sec);
    }
}
