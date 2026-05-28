using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats 
{
    [SerializeField]private int baseValue;
    public List<int> modifiers;

    public int GetValue()
    {
        return baseValue;
    }


    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }
    public void SetDefValue(int Value)
    {
        baseValue = Value;
    }
    public void RemoveModifier(int modifier)
    {
        modifiers.RemoveAt(modifier);
    }
}
