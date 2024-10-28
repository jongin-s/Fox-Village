using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public int sceneToLoad;
    public GameObject Screen;
    public Slider LoadingBarFill;

    IntButton intButton;  // 상호작용 버튼의 컴포넌트인 스크립트
    public GameObject dynamic;  // 상호작용 버튼 그 자체

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dynamic.SetActive(true);  // NPC와 가까워지면 상호작용 버튼이 나타남
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && intButton.click)
        {
            LoadScene();
            intButton.click = false;
            Debug.Log("Teleport");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dynamic.SetActive(false);  // NPC와 멀어지면 상호작용 버튼이 다시 숨겨짐
        }
    }

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);

        intButton = GameObject.Find("Canvas").GetComponent<IntButton>();
        dynamic.SetActive(false);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;
        Time.timeScale = 1f;

        Screen.SetActive(true);
        LoadingBarFill.gameObject.SetActive(true);

        while (!op.isDone)
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);

            LoadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
