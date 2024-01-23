using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
                    // 使物体消失：禁用它
                    gameObject.SetActive(false);

                    // 或者，如果你想完全销毁这个物体，取消注释下面这行代码
                    // Destroy(gameObject);
                }
            }
        }
    }
}
