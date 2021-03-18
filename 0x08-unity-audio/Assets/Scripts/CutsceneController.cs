using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
	public GameObject main;
	public GameObject player;
	public GameObject timer_canvas;

	void cutsceneEnd()
	{
		player.GetComponent<PlayerController>().enabled = true;
		main.SetActive(true);
		timer_canvas.SetActive(true);
		gameObject.SetActive(false);
	}
}
