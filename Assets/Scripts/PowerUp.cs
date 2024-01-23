using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // ����������䵽���λ�õ�����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ��������Ͷ��
            if (Physics.Raycast(ray, out hit))
            {
                // ��������Ƿ�������������
                if (hit.collider.gameObject == gameObject)
                {
                    // ʹ������ʧ��������
                    gameObject.SetActive(false);

                    // ���ߣ����������ȫ����������壬ȡ��ע���������д���
                    // Destroy(gameObject);
                }
            }
        }
    }
}
