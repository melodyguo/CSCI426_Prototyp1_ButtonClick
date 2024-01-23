using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    [SerializeField] Image background;

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.value / slider.maxValue);
        background.color = new Color(fill.color.r, fill.color.g, fill.color.b, 0.0f);
    }

    public float GetHealth()
    {
        return slider.value;
    }

    public void ResetToMax(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1);
        background.color = new Color(fill.color.r, fill.color.g, fill.color.b, 0.0f);
    }
}
