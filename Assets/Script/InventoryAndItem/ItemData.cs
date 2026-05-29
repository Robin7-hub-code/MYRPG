using UnityEngine;
public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName="New Item",menuName ="Data/item")]

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string Name;
    public string Description;
    public Sprite icon;
}
