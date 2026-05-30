using UnityEngine;

public class BuffTest : MonoBehaviour
{
    [SerializeField] private BuffPanel buffPanel;

    [Header("异常状态图标")]
    [SerializeField] private Sprite ignitedIcon;
    [SerializeField] private Sprite chilledIcon;
    [SerializeField] private Sprite shockedIcon;

    private void Update()
    {
        // 异常状态 - Z、X、C键
        if (Input.GetKeyDown(KeyCode.Z))
        {
            buffPanel.AddBuff(BuffData.CreateAbnormalBuff("ignited", ignitedIcon));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            buffPanel.AddBuff(BuffData.CreateAbnormalBuff("chilled", chilledIcon));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            buffPanel.AddBuff(BuffData.CreateAbnormalBuff("shocked", shockedIcon));
        }

        // 清除所有buff - V键
        if (Input.GetKeyDown(KeyCode.V))
        {
            buffPanel.ClearAll();
        }
    }
}
