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
      // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 从摄像机发射到鼠标位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 进行射线投射
            if (Physics.Raycast(ray, out hit))
            {
                // 检查射线是否击中了这个物体
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

                    // 或者，如果你想完全销毁这个物体，取消注释下面这行代码
                    Destroy(gameObject);

                }
            }
        }

    }


    private void OnMouseDown()
    {
        

    }
}
