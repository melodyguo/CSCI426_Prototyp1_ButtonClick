using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public GameObject buttonCap; // Assign this in the inspector or find it dynamically
    public GameObject defaultButton;
    public float moveDistance = 1.0f; // Distance to move buttonCap
    public float shakeDistance = 0.3f;

    public ScreenShake screenShake;

    private Vector3 originalPosition;
    private Vector3 buttonOriginalPosition;
    private Vector3 targetPosition;
    private Coroutine moveCoroutine;

    void Start()
    {
        // 使用 localPosition 获取相对于父对象的初始位置
        originalPosition = buttonCap.transform.localPosition;
        buttonOriginalPosition = defaultButton.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 重置 defaultButton 的位置到其初始相对位置
            defaultButton.transform.localPosition = buttonOriginalPosition;
        }
        targetPosition = originalPosition - Vector3.up * moveDistance;
        // 检测按键
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            screenShake.TriggerShake();
            // 对 defaultButton 应用相对移动
            defaultButton.transform.localPosition = new Vector3(defaultButton.transform.localPosition.x, defaultButton.transform.localPosition.y - shakeDistance, defaultButton.transform.localPosition.z);
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // 使用 localPosition 作为移动目标
            moveCoroutine = StartCoroutine(MoveOverSeconds(buttonCap, targetPosition, 0.03f));
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // 使用 localPosition 返回到原始位置
            moveCoroutine = StartCoroutine(MoveOverSeconds(buttonCap, originalPosition, 0.1f));
        }
    }

    IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.localPosition = end;
    }

    public GameObject[] prefabs; // 存放预制体的数组
    private int buttonCount = 0; // 按钮计数器

    // 调用这个方法以增加计数并生成预制体
    public void IncrementAndSpawn()
    {
        buttonCount++; // 增加按钮计数

        if (prefabs.Length > 0)
        {
            // 从预制体数组中随机选择一个
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
            // 在当前游戏对象的位置生成预制体
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }

}