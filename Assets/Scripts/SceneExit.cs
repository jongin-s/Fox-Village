using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 포털 오브젝트의 컴포넌트

public class SceneExit : MonoBehaviour
{
    public int sceneToLoad;  // 로딩할 씬의 번호
    public GameObject Screen;  // 로딩 화면
    public Slider LoadingBarFill;  // 로딩 바

    IntButton intButton;  // 상호작용 버튼의 컴포넌트인 스크립트
    public GameObject dynamic;  // 상호작용 버튼 그 자체

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")  // 플레이어가 포털에 접근했을 때
        {
            dynamic.SetActive(true);  // 상호작용 버튼이 나타남
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && intButton.click)  // 플레이어가 포털에 접근해 있고 상호작용 버튼을 클릭했을 때
        {
            LoadScene();  // 씬 로딩
            intButton.click = false;  // 상호작용 버튼의 클릭 상태를 false로 변경
            Debug.Log("Teleport");  // 디버그용 로그
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")  // 플레이어가 포털과 벌어지면
        {
            dynamic.SetActive(false);  // 상호작용 버튼이 다시 숨겨짐
        }
    }

    private void Start()
    {
        Screen.SetActive(false);
        LoadingBarFill.gameObject.SetActive(false);
        dynamic.SetActive(false);  // 로딩 화면, 로딩 바, 상호작용 버튼을 비활성화

        intButton = GameObject.Find("Canvas").GetComponent<IntButton>();  // 상호작용 버튼 컴포넌트를 가져옴
    }

    public void LoadScene()  // 씬을 로딩하는 함수
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));  // 로딩할 씬의 번호를 인수로 받는 코루틴
    }

    IEnumerator LoadSceneAsync(int sceneID)  // 실제 씬을 로딩하는 코루틴
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneID);
        op.allowSceneActivation = true;
        Time.timeScale = 1f;  // SceneManager 기능으로 실제로 씬을 로드

        Screen.SetActive(true);
        LoadingBarFill.gameObject.SetActive(true);  // 로딩 화면과 로딩 바를 활성화

        while (!op.isDone)  // 로딩하는 동안
        {
            float progressValue = Mathf.Clamp01(op.progress / 0.9f);  // 로딩 정도를 0과 1 사이의 값으로 치환

            LoadingBarFill.value = progressValue;  // 로딩 정도를 로딩 바에 표시

            yield return null;
        }
    }
}
