using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    // 这一行让变量可以在 Inspector 面板上显示和拖拽
    [Header("设置")]
    [Tooltip("拖入你的玩家物体")]
    public Transform playerTarget;

    void LateUpdate()
    {
        // 如果没有设置玩家，就不执行后面的代码，防止报错
        if (playerTarget == null) return;

        // 核心逻辑：
        // 1. X轴：跟随玩家的 X
        // 2. Y轴：跟随玩家的 Y
        // 3. Z轴：保持相机原本的 Z (非常重要！2D相机不能改变Z，否则视野大小会变)

        Vector3 newPosition = new Vector3(
            playerTarget.position.x,
            playerTarget.position.y,
            transform.position.z
        );

        transform.position = newPosition;
    }
}
