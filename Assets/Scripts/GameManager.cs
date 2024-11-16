using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // TMP 기능을 사용하기 위해 반드시 임포트

public class GameManager : MonoBehaviour
{
    public GameObject player;  // 플레이어의 GameObject
    public TextMeshProUGUI invTxt0;  // 푸른 보석의 UI 텍스트
    public TextMeshProUGUI invTxt1;  // 붉은 보석의 UI 텍스트
    public Image[] weaponImage;  // 무기 교체 UI에 표시될 무기 사진
    public GameObject respawn;  // 플레이어가 처음으로 맵에 등장할 때 위치, 이상은 모두 인스펙터에서 설정
    Damage damage; // 대미지 컴포넌트
    WeaponSwitch weaponSwitch;  // 무기 교체 컴포넌트, 이 두 컴포넌트는 플레이어 오브젝트에서 가져옴

    public void Start()
    {
        damage = player.GetComponent<Damage>();
        weaponSwitch = player.GetComponent<WeaponSwitch>();  // 컴포넌트를 가져옴
        Load();  // 이 스크립트가 처음으로 실행될 때 같이 실행
        Application.targetFrameRate = 24;  // 목표 FPS
    }

    public void Awake()
    {
        invTxt0 = GameObject.Find("Jewel").GetComponentInChildren<TextMeshProUGUI>();
        invTxt1 = GameObject.Find("Jewel2").GetComponentInChildren<TextMeshProUGUI>();  // 구체적인 슬롯 이름에서 자식의 컴포넌트를 가져오지 않으면 다른 오브젝트가 업데이트됨
    }

    private void Update()
    {
        weaponImage[0].color = new Color(1, 1, 1, weaponSwitch.hasWeapons[0] ? 1 : 0);
        weaponImage[1].color = new Color(1, 1, 1, weaponSwitch.hasWeapons[1] ? 1 : 0);
        weaponImage[2].color = new Color(1, 1, 1, weaponSwitch.hasWeapons[2] ? 1 : 0);
        weaponImage[3].color = new Color(1, 1, 1, weaponSwitch.hasWeapons[3] ? 1 : 0);  // 무기를 소유하고 있다면 무기 사진을 표시, 그렇지 않다면 투명화
    }

    public void GetItem(TextMeshProUGUI invTxt, int count)  // 텍스트를 업데이트하는 함수, ItemCollection.cs에서 사용
    {
        invTxt.text = $"{count}";  // $"{count}": 문자열 내에서 변수를 그대로 출력하는 문법
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("RespawnX", player.transform.position.x);
        PlayerPrefs.SetFloat("RespawnY", player.transform.position.y);
        PlayerPrefs.SetFloat("RespawnZ", player.transform.position.z);  // 현재 맵의 리스폰 위치를 저장

        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);  // 현재 씬 번호를 저장

        PlayerPrefs.SetInt("HP", damage.curHealth);  // HP를 저장

        PlayerPrefs.SetInt("Weapon1", weaponSwitch.hasWeapons[0] ? 1 : 0);
        PlayerPrefs.SetInt("Weapon2", weaponSwitch.hasWeapons[1] ? 1 : 0);
        PlayerPrefs.SetInt("Weapon3", weaponSwitch.hasWeapons[2] ? 1 : 0);
        PlayerPrefs.SetInt("Weapon4", weaponSwitch.hasWeapons[3] ? 1 : 0);  // 무기 소유 여부를 저장, 불리언은 저장할 수 없으므로 대신 1과 0의 정수로 저장

        PlayerPrefs.Save();  // 최종 저장

        Debug.Log("Save Successful" + " " + PlayerPrefs.GetInt("Scene"));  // 디버그용 로그
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("Scene"))
            return;  // 최초로 실행했을 때는 키가 없으므로 즉시 종료, 아무것도 실행되지 않음

        float x = PlayerPrefs.GetFloat("RespawnX");
        float y = PlayerPrefs.GetFloat("RespawnY");
        float z = PlayerPrefs.GetFloat("RespawnZ");  // 저장한 위치 벡터를 불러옴, 씬 번호는 LoadingScreen.cs에서 관리

        if (PlayerPrefs.GetInt("Scene") == SceneManager.GetActiveScene().buildIndex)  // 리스폰 씬이 저장한 씬이 아니라면 (마지막으로 다른 씬에서 저장했다면)
        {
            player.transform.position = new Vector3(x, y, z);  // 저장한 위치로 플레이어 위치를 지정 (현재 씬의 리스폰 위치로)
        }
        else
        {
            player.transform.position = respawn.transform.position;  // 맞다면 그대로 리스폰
        }

        damage.curHealth = PlayerPrefs.GetInt("HP");  // HP를 설정

        weaponSwitch.hasWeapons[0] = PlayerPrefs.GetInt("Weapon1") != 0;
        weaponSwitch.hasWeapons[1] = PlayerPrefs.GetInt("Weapon2") != 0;
        weaponSwitch.hasWeapons[2] = PlayerPrefs.GetInt("Weapon3") != 0;
        weaponSwitch.hasWeapons[3] = PlayerPrefs.GetInt("Weapon4") != 0;  // 무기 소유 여부가 0이라면 불리언도 0, 1이라면 불리언도 1

        Debug.Log("Load Successful" + " " + PlayerPrefs.GetInt("Scene"));  // 디버그용 로그
    }

    private void OnApplicationQuit()
    {
        Save();  // 애플리케이션(게임)을 강제종료할 때 자동으로 저장
    }
}
