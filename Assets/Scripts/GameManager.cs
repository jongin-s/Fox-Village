using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // TMP 기능을 사용하기 위해 반드시 임포트

public class GameManager : MonoBehaviour
{
    public GameObject player;  // 플레이어의 GameObject
    public TextMeshProUGUI invTxt0;  // Inventory Panel의 각 슬롯의 텍스트
    public GameObject respawn;
    Damage damage;

    public void Start()
    {
        damage = player.GetComponent<Damage>();
        Load();  // 이 스크립트가 처음으로 실행될 때 같이 실행
        Application.targetFrameRate = 24;  // 목표 FPS
    }

    public void Awake()
    {
        invTxt0 = GameObject.Find("Jewel").GetComponentInChildren<TextMeshProUGUI>();  // 구체적인 슬롯 이름에서 자식의 컴포넌트를 가져오지 않으면 다른 "Text (TMP)" 오브젝트가 업데이트됨
    }

    public void GetItem(TextMeshProUGUI invTxt, int count)
    {
        invTxt.text = $"{count}";  // $"{count}": 문자열 내에서 변수를 그대로 출력하는 문법
    }

    public void Save()
    {
        //PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        //PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);  // 플레이어 위치를 나타내는 실수 3개를 저장

        //PlayerPrefs.SetFloat("PlayerRX", player.transform.rotation.x);
        //PlayerPrefs.SetFloat("PlayerRY", player.transform.rotation.y);
        //PlayerPrefs.SetFloat("PlayerRZ", player.transform.rotation.z);
        //PlayerPrefs.SetFloat("PlayerRW", player.transform.rotation.w);  // 플레이어 회전을 나타내는 실수 4개를 저장

        PlayerPrefs.SetFloat("RespawnX", respawn.transform.position.x);
        PlayerPrefs.SetFloat("RespawnY", respawn.transform.position.y);
        PlayerPrefs.SetFloat("RespawnZ", respawn.transform.position.z);

        PlayerPrefs.SetInt("HP", damage.curHealth);

        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);

        PlayerPrefs.Save();

        Debug.Log("Save Successful" + " " + PlayerPrefs.GetInt("Scene"));
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("Scene"))
            return;  // 최초로 실행했을 때는 키가 없으므로 즉시 종료, 아무것도 실행되지 않음

        //float x = PlayerPrefs.GetFloat("PlayerX");
        //float y = PlayerPrefs.GetFloat("PlayerY");
        //float z = PlayerPrefs.GetFloat("PlayerZ");  // 플레이어 위치를 나타내는 실수 3개를 불러옴

        //float rx = PlayerPrefs.GetFloat("PlayerRX");
        //float ry = PlayerPrefs.GetFloat("PlayerRY");
        //float rz = PlayerPrefs.GetFloat("PlayerRZ");
        //float rw = PlayerPrefs.GetFloat("PlayerRW");  // 플레이어 회전을 나타내는 실수 4개를 불러옴

        float x = PlayerPrefs.GetFloat("RespawnX");
        float y = PlayerPrefs.GetFloat("RespawnY");
        float z = PlayerPrefs.GetFloat("RespawnZ");

        int hp = PlayerPrefs.GetInt("HP");

        if (respawn.transform.position != new Vector3(x, y, z))
        {
            player.transform.position = new Vector3(x, y, z);  // 플레이어 위치를 지정
        }
        else
        {
            player.transform.position = respawn.transform.position;
        }

        //player.transform.rotation = new Quaternion(rx, ry, rz, rw);  // 플레이어 회전을 지정
        damage.curHealth = hp;

        Debug.Log("Load Successful" + " " + PlayerPrefs.GetInt("Scene"));
    }
}
