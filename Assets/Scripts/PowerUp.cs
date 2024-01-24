using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Player player;
    public int powerUpType;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }


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
                    if (player != null)
                    {
                        if (powerUpType == 0)
                        {
                            player.ApplyAddPowerup(1);
                        }
                        if (powerUpType == 1)
                        {
                            player.ApplyPowerPowerup(1);
                        }
                        if (powerUpType == 2)
                        {
                            player.ApplyMultiplyPowerup(1);
                        }
                    }
                    else
                    {
                        Debug.Log("noplayer");
                    }

                    // ���ߣ����������ȫ����������壬ȡ��ע���������д���
                    Destroy(gameObject);

                }
            }
        }

    }


    private void OnMouseDown()
    {
        

    }
}
