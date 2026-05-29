using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item", menuName = "Data/equipment")]
public class ItemData_Equipment :ItemData
{
    public EquipmentType equipmentType;

    [Header("主要状态属性")]
    public int strength;//力量 ，每增加一点力量增加一点暴击伤害，增加一点基础伤害
    public int agility;// 敏捷，每增加一点敏捷，增加一点暴击概率和闪避率
    public int intelligence;//智力，增加一点魔法伤害，增加一点魔法抗性
    public int vitality;//增加一到五点生命值
    [Header("伤害和暴击属性")]
    public int damage;
    public int critChance;
    public int critPower; //默认150%

    [Header("防御属性")]

    public int health;
    public int evasion;//闪避
    public int armer;//护盾
    public int magicResistance;


    [Header("魔法伤害")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("工艺需求")]
    public List<InventoryItem> craftMat;

    public void AddModifiers()
    {
        PlayerStats playerstats=PlayerManager.instance.player.GetComponent<PlayerStats>();


        playerstats.strength.AddModifier(strength);
        playerstats.agility.AddModifier(agility);
        playerstats.intelligence.AddModifier(intelligence);
        playerstats.vitality.AddModifier(vitality);


        playerstats.damage.AddModifier(damage);
        playerstats.critChance.AddModifier(critChance);
        playerstats.critPower.AddModifier(critPower);

        playerstats.maxHealth.AddModifier(health);
        playerstats.armer.AddModifier(armer);
        playerstats.evasion.AddModifier(evasion);
        playerstats.magicResistance.AddModifier(magicResistance);

        playerstats.fireDamage.AddModifier(fireDamage);
        playerstats.iceDamage.AddModifier(iceDamage);
        playerstats.lightingDamage.AddModifier(lightingDamage);

    }

    public void RemoveModifiers()
    {
        PlayerStats playerstats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerstats.strength.RemoveModifier(strength);
        playerstats.agility.RemoveModifier(agility);
        playerstats.intelligence.RemoveModifier(intelligence);
        playerstats.vitality.RemoveModifier(vitality);


        playerstats.damage.RemoveModifier(damage);
        playerstats.critChance.RemoveModifier(critChance);
        playerstats.critPower.RemoveModifier(critPower);

        playerstats.maxHealth.RemoveModifier(health);
        playerstats.armer.RemoveModifier(armer);
        playerstats.evasion.RemoveModifier(evasion);
        playerstats.magicResistance.RemoveModifier(magicResistance);

        playerstats.fireDamage.RemoveModifier(fireDamage);
        playerstats.iceDamage.RemoveModifier(iceDamage);
        playerstats.lightingDamage.RemoveModifier(lightingDamage);
    }

       
}
