using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    public GameObject Screen;
    public Slider LoadingBarFill;
    public GameObject respawn;

    private void Awake()
    {
        transform.position = respawn.transform.position;
    }

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y <= 0)
        {
            transform.position = respawn.transform.position;
        }
    }
}
