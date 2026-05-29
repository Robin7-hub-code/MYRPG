using System;
using System.Collections;
using UnityEngine;
[Serializable]
public class InventoryItem {


    public ItemData data;
    public int stack;
    public InventoryItem(ItemData _data)
    {
        data = _data;
        AddStack();
    }
    public void AddStack()=>stack++;

    public void RemoveStack()=>stack--;

}