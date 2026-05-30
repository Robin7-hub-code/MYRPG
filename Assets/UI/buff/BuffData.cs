using UnityEngine;

public enum BuffType
{
    Element,
    ElementReaction,
    AbnormalStatus,
    Buff
}

[System.Serializable]
public class BuffData
{
    public string buffId;
    public string buffName;
    public Sprite icon;
    public float duration = -1f;
    public int maxStack = 1;
    public int currentStack = 1;
    public BuffType buffType;

    [System.NonSerialized] public float remainTime;

    public static BuffData CreateElementBuff(string elementName, Sprite icon, float duration = 8f)
    {
        return new BuffData
        {
            buffId = $"element_{elementName}",
            buffName = elementName,
            icon = icon,
            duration = duration,
            buffType = BuffType.Element
        };
    }

    public static BuffData CreateReactionBuff(string reactionName, Sprite icon, float duration = 4f)
    {
        return new BuffData
        {
            buffId = $"reaction_{reactionName}",
            buffName = reactionName,
            icon = icon,
            duration = duration,
            buffType = BuffType.ElementReaction
        };
    }

    public static BuffData CreateAbnormalBuff(string statusName, Sprite icon, float duration = 6f)
    {
        return new BuffData
        {
            buffId = $"abnormal_{statusName}",
            buffName = statusName,
            icon = icon,
            duration = duration,
            buffType = BuffType.AbnormalStatus
        };
    }
}
