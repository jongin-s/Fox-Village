using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject[] canvas;
    public GameObject pause;

    private void Start() { pause.SetActive(false); }

    public void TogglePause()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
        pause.SetActive(true);
    }
}
