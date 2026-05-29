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
        int modifierDamage=0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifierDamage += modifiers[i];
        }
        return baseValue+modifierDamage;
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
        modifiers.Remove(modifier);
    }
}
