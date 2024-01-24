using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderModifier : MonoBehaviour
{
    public Button button;
    private float healthRatio;
    public GameObject buttonBody;
    public ButtonAnimation buttonAnimation;

    private float lastValue = -1f; // 存储上次检查的值以检测变化

    void Update()
    {
        healthRatio = 1f - (button.healthBar.GetHealth() / button.maxHealth);

        if (button != null && healthRatio != lastValue)
        {
            // 当sourceValue变化时更新着色器的全局值
            float shaderValue = Mathf.Clamp01(healthRatio * 0.5f); // 将0-1范围映射到0-0.5
            Shader.SetGlobalFloat("ExplodeProgress", shaderValue);

            lastValue = healthRatio; // 更新最后的值
        }

        
    }

    public IEnumerator ChangeNumber(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            elapsedTime += Time.fixedDeltaTime;
            Shader.SetGlobalFloat("ExplodeProgress", currentValue);
            yield return null;
        }
        Debug.Log("stoped");
        buttonBody.transform.position = new Vector3(0f, 0f,0f);
        Shader.SetGlobalFloat("ExplodeProgress",0f);
        buttonAnimation.IncrementAndSpawn();
    }

    public void explodeAnimation()
    {
        StartCoroutine(ChangeNumber(0.5f, 1.5f, 1.5f));
    }

}