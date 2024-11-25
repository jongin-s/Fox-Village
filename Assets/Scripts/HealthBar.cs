using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 오브젝트 Player Health Bar의 컴포넌트로 삽입

public class HealthBar : MonoBehaviour
{
    public Slider slider;  // HP 바의 역할을 하는 슬라이더
    public Gradient gradient;  // 슬라이더의 그라디언트
    public Image fill;  // 슬라이더 위의 HP를 나타내는 이미지

    public void SetMaxHealth(int health)  // 최대 체력 설정
    {
        slider.maxValue = health;
        slider.value = health;  // 최댓값과 현재 값을 같게 설정

        Color colorWithAlpha = gradient.Evaluate(1f);  // 그라디언트의 오른쪽 끝의 색(초록색)
        colorWithAlpha.a = 0.627f;  // 알파 값을 160(0.627f)으로 설정
        fill.color = colorWithAlpha;  // 그 색을 HP 이미지에 적용
    }

    public void SetHealth(int health)  // 현재 체력 설정
    {
        slider.value = health;

        Color colorWithAlpha = gradient.Evaluate(slider.normalizedValue);  // 그라디언트의 0과 1 사이의 값의 색
        colorWithAlpha.a = 0.627f;
        fill.color = colorWithAlpha;
    }
}
