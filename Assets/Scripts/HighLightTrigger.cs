using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightTrigger : MonoBehaviour
{

    private GameObject lastHighlighted = null;

    // Update is called once per frame
    void Update()
    {
        // ����������������λ�õ�����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ��������Ͷ��
        if (Physics.Raycast(ray, out hit))
        {
            // ���������������ײ��������Ƿ������ǹ��ĵ�����
            if (hit.collider != null)
            {
                // ������ǰ����� Outline
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;

                    // ������һ������� Outline
                    if (lastHighlighted != null && lastHighlighted != hit.collider.gameObject)
                    {
                        Outline lastOutline = lastHighlighted.GetComponent<Outline>();
                        if (lastOutline != null)
                        {
                            lastOutline.enabled = false;
                        }
                    }

                    // ������������ʾ������
                    lastHighlighted = hit.collider.gameObject;
                }
            }
        }
        else
        {
            // ���û�����屻��ͣ��������һ������� Outline
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
