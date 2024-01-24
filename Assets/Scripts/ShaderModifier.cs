using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderModifier : MonoBehaviour
{
    public SourceScript sourceScript;

    private float lastValue = -1f; // 存储上次检查的值以检测变化

    void Update()
    {
        if (sourceScript != null && sourceScript.sourceValue != lastValue)
        {
            // 当sourceValue变化时更新着色器的全局值
            float shaderValue = Mathf.Clamp01(sourceScript.sourceValue * 0.5f); // 将0-1范围映射到0-0.5
            Shader.SetGlobalFloat("ExplodeProgress", shaderValue);

            lastValue = sourceScript.sourceValue; // 更新最后的值
        }
    }
}