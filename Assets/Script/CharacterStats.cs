using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("主要状态属性")]
    public Stats strength;//力量 ，每增加一点力量增加一点暴击伤害，增加一点基础伤害
    public Stats agility;// 敏捷，每增加一点敏捷，增加一点暴击概率和闪避率
    public Stats intelligence;//智力，增加一点魔法伤害，增加一点魔法抗性
    public Stats vitality;//增加一到五点生命值
    [Header("伤害和暴击属性")]
    public Stats damage;
    public Stats critChance;
    public Stats critPower; //默认150%

    [Header("防御属性")]

    public Stats maxHealth;
    public Stats evasion;//闪避
    public Stats armer;//护盾

    [Header("Magic stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

   




    public int currentHealth;


   protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
        critPower.SetDefValue(150);
    }

    public void DoDamage(CharacterStats _target)
    {
        bool flowControl = CanAvoidAttack(_target);
        if (!flowControl)
        {
            return;
        }

        int totolDamage = damage.GetValue() + strength.GetValue();
        totolDamage-=armer.GetValue();
        totolDamage = Mathf.Clamp(totolDamage, 0, int.MaxValue);
        _target.TakeDamage(totolDamage);

        if(CanCrit())
        {
            totolDamage = CalculateCriticalDamage(totolDamage);
            
        }
    }

    private  bool CanAvoidAttack(CharacterStats _target)
    {
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
           
            return false;
        }

        return true;
    }
    public virtual void DoMagicalDamage(CharacterStats  _targets)
    {

    }

    public void ApplyAilments(bool _ignite,bool _chill,bool _shock)
    {

    }

    protected virtual void TakeDamage(int _damage)
    {
        currentHealth-=_damage;
        Debug.Log(_damage);
        if (currentHealth < 0)
        {
        
            Die();
        }
    }    
    protected virtual void Die()
    {

    }

    private bool CanCrit()
    {
        int totolCritChance=critChance.GetValue()+agility.GetValue();
        if(Random.Range(0,100)<= totolCritChance)
        {
            return true;
        }
        return false;
    }
    private int CalculateCriticalDamage(int _damage)
    {
        float totolCritPower = (critPower.GetValue() +strength.GetValue()) * 0.01f;
        float critDamage = _damage * totolCritPower;

        return Mathf.RoundToInt(critDamage);    
    }
}
