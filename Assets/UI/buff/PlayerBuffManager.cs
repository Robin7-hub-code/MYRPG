using UnityEngine;

public class PlayerBuffManager : MonoBehaviour
{
    [Header("引用")]
    [SerializeField] private BuffPanel buffPanel;
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private Entity_Current_buff entityBuff;

    [Header("元素图标")]
    [SerializeField] private Sprite fireIcon;
    [SerializeField] private Sprite waterIcon;
    [SerializeField] private Sprite stoneIcon;
    [SerializeField] private Sprite electricIcon;
    [SerializeField] private Sprite woodIcon;

    [Header("元素反应图标")]
    [SerializeField] private Sprite zhengfaIcon;
    [SerializeField] private Sprite fushiIcon;
    [SerializeField] private Sprite posuiIcon;
    [SerializeField] private Sprite ronghuaIcon;
    [SerializeField] private Sprite tunshiIcon;

    [Header("异常状态图标")]
    [SerializeField] private Sprite ignitedIcon;
    [SerializeField] private Sprite chilledIcon;
    [SerializeField] private Sprite shockedIcon;

    private bool lastFire;
    private bool lastWater;
    private bool lastStone;
    private bool lastElectric;
    private bool lastWood;

    private bool lastIgnited;
    private bool lastChilled;
    private bool lastShocked;

    private bool lastZhengfa;
    private bool lastFushi;
    private bool lastPosui;
    private bool lastRonghua;
    private bool lastTunshi;

    private void Start()
    {
        if (buffPanel == null)
            buffPanel = GetComponent<BuffPanel>();

        if (characterStats == null)
            characterStats = GetComponentInParent<CharacterStats>();

        if (entityBuff == null)
            entityBuff = GetComponentInParent<Entity_Current_buff>();

        InitializeLastStates();
    }

    private void InitializeLastStates()
    {
        if (entityBuff != null)
        {
            lastFire = entityBuff.fire;
            lastWater = entityBuff.water;
            lastStone = entityBuff.stone;
            lastElectric = entityBuff.electric;
            lastWood = entityBuff.wood;
            lastZhengfa = entityBuff.zhengfa;
            lastFushi = entityBuff.fushi;
            lastPosui = entityBuff.posui;
            lastRonghua = entityBuff.ronghua;
            lastTunshi = entityBuff.tunshi;
        }

        if (characterStats != null)
        {
            lastIgnited = characterStats.isIgnited;
            lastChilled = characterStats.isChilled;
            lastShocked = characterStats.isShocked;
        }
    }

    private void Update()
    {
        CheckElementChanges();
        CheckAbnormalStatusChanges();
        CheckReactionChanges();
    }

    private void CheckElementChanges()
    {
        if (entityBuff == null || buffPanel == null) return;

        CheckElementChange("fire", entityBuff.fire, ref lastFire, fireIcon);
        CheckElementChange("water", entityBuff.water, ref lastWater, waterIcon);
        CheckElementChange("stone", entityBuff.stone, ref lastStone, stoneIcon);
        CheckElementChange("electric", entityBuff.electric, ref lastElectric, electricIcon);
        CheckElementChange("wood", entityBuff.wood, ref lastWood, woodIcon);
    }

    private void CheckElementChange(string elementName, bool current, ref bool last, Sprite icon)
    {
        if (current && !last)
        {
            buffPanel.AddBuff(BuffData.CreateElementBuff(elementName, icon));
        }
        else if (!current && last)
        {
            buffPanel.RemoveBuff($"element_{elementName}");
        }
        last = current;
    }

    private void CheckAbnormalStatusChanges()
    {
        if (characterStats == null || buffPanel == null) return;

        CheckStatusChange("ignited", characterStats.isIgnited, ref lastIgnited, ignitedIcon);
        CheckStatusChange("chilled", characterStats.isChilled, ref lastChilled, chilledIcon);
        CheckStatusChange("shocked", characterStats.isShocked, ref lastShocked, shockedIcon);
    }

    private void CheckStatusChange(string statusName, bool current, ref bool last, Sprite icon)
    {
        if (current && !last)
        {
            buffPanel.AddBuff(BuffData.CreateAbnormalBuff(statusName, icon));
        }
        else if (!current && last)
        {
            buffPanel.RemoveBuff($"abnormal_{statusName}");
        }
        last = current;
    }

    private void CheckReactionChanges()
    {
        if (entityBuff == null || buffPanel == null) return;

        CheckReactionChange("zhengfa", entityBuff.zhengfa, ref lastZhengfa, zhengfaIcon);
        CheckReactionChange("fushi", entityBuff.fushi, ref lastFushi, fushiIcon);
        CheckReactionChange("posui", entityBuff.posui, ref lastPosui, posuiIcon);
        CheckReactionChange("ronghua", entityBuff.ronghua, ref lastRonghua, ronghuaIcon);
        CheckReactionChange("tunshi", entityBuff.tunshi, ref lastTunshi, tunshiIcon);
    }

    private void CheckReactionChange(string reactionName, bool current, ref bool last, Sprite icon)
    {
        if (current && !last)
        {
            buffPanel.AddBuff(BuffData.CreateReactionBuff(reactionName, icon));
        }
        else if (!current && last)
        {
            buffPanel.RemoveBuff($"reaction_{reactionName}");
        }
        last = current;
    }

    public void AddCustomBuff(BuffData buffData)
    {
        if (buffPanel != null)
            buffPanel.AddBuff(buffData);
    }

    public void RemoveCustomBuff(string buffId)
    {
        if (buffPanel != null)
            buffPanel.RemoveBuff(buffId);
    }
}
