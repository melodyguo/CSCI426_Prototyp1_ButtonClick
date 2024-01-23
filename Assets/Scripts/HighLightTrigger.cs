using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightTrigger : MonoBehaviour
{

    private GameObject lastHighlighted = null;

    // Update is called once per frame
    void Update()
    {
        // 创建从摄像机到鼠标位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 进行射线投射
        if (Physics.Raycast(ray, out hit))
        {
            // 如果射线与物体碰撞，检查它是否是我们关心的物体
            if (hit.collider != null)
            {
                // 开启当前物体的 Outline
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;

                    // 禁用上一个物体的 Outline
                    if (lastHighlighted != null && lastHighlighted != hit.collider.gameObject)
                    {
                        Outline lastOutline = lastHighlighted.GetComponent<Outline>();
                        if (lastOutline != null)
                        {
                            lastOutline.enabled = false;
                        }
                    }

                    // 更新最后高亮显示的物体
                    lastHighlighted = hit.collider.gameObject;
                }
            }
        }
        else
        {
            // 如果没有物体被悬停，禁用上一个物体的 Outline
            if (lastHighlighted != null)
            {
                Outline lastOutline = lastHighlighted.GetComponent<Outline>();
                if (lastOutline != null)
                {
                    lastOutline.enabled = false;
                }
                lastHighlighted = null;
            }
        }
    }
}
