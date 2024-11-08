using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

public void SetMaxHealth(int health)
{
    slider.maxValue = health;
    slider.value = health;

    // 알파 값을 160 (0.627f)으로 설정
    Color colorWithAlpha = gradient.Evaluate(1f);
    colorWithAlpha.a = 0.627f; // 알파 값 설정
    fill.color = colorWithAlpha;
}

public void SetHealth(int health)
{
    slider.value = health;

    // 알파 값을 160 (0.627f)으로 설정
    Color colorWithAlpha = gradient.Evaluate(slider.normalizedValue);
    colorWithAlpha.a = 0.627f; // 알파 값 설정
    fill.color = colorWithAlpha;
}

}
