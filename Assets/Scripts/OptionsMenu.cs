using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

// 옵션 메뉴 패널의 컴포넌트

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;  // 오디오 믹서(게임의 모든 오디오를 관리)

    public void Close() // 종료 버튼
    {
        gameObject.SetActive(false);  // 옵션 패널을 비활성화
    }

    public void Graphics(int quality)  // 그래픽 드롭다운
    {
        QualitySettings.SetQualityLevel(quality);  // 그래픽 퀄리티를 설정(quality는 0부터 1씩 커지는 정수, 숫자가 높을수록 퀄리티도 높음!)
    }

    public void Volume(float vol)  // 볼륨 슬라이더
    {
        mixer.SetFloat("vol", vol);  // 슬라이더의 위치를 반영하여 오디오 볼륨을 설정
    }
}
