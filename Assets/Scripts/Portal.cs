using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스 추가

public class Portal : MonoBehaviour
{
    public GameObject Screen;
    public Slider LoadingBarFill;

    // 특정 트리거에 들어올 때 호출
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 Particle System 영역에 들어왔는지 확인
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
            // 씬 2로 전환
            SceneManager.LoadScene(2);
        }
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

