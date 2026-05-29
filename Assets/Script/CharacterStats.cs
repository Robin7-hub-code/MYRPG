using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private bool isDead=false;
    private EntityFX fx;
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
    public Stats magicResistance;


    [Header("魔法伤害")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;



    //角色自己的异常状态
    public bool isIgnited;//时间伤害
    public bool isChilled;//造成的伤害降低为原来的80%
    public bool isShocked;//敌人闪避的概率增加20%
    public int ignitDamage;//收到的火烧伤害

    //燃烧时间和燃烧时间间隔
    private float ignitTimer;
    private float chilledTimer;
    private float shockedTimer;

    [Header("异常状态时间")]
    [SerializeField] private float ailmentDuration=4;
    [Header("冰冻减速百分比")]
    [SerializeField] private float slowPercentage;


    private float iginiteDamageCoolDown=0.3f;
    private float igniteDamageTimer;



    public int currentHealth;

    public System.Action onHealthChanged;


   protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        critPower.SetDefValue(150);
        fx=GetComponentInChildren<EntityFX>();
    }
    protected virtual void Update()
    {
        ignitTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;
        if (ignitTimer < 0)
        {
            isIgnited = false;
        }

        if (chilledTimer < 0)
        {
            isChilled = false;
        }
        if (shockedTimer < 0)
        {
            isShocked = false;
        }
        ApplyIgniteDamage();
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0 && isIgnited)
        {
            igniteDamageTimer = iginiteDamageCoolDown;
           
            DecreaseHealth(ignitDamage);
            if (currentHealth < 0&&!isDead)
            {
                Die();
            }
        }
    }

    public void SetupIgniteDamage(int _Damage) => ignitDamage = _Damage;
    public void DoDamage(CharacterStats _target)
    {
        bool flowControl = CanAvoidAttack(_target);
        if (!flowControl)
        {
            return;
        }
        DoMagicEf(_target);
        DoPhysicalDamage(_target);
        DoMagicalDamage(_target);
        

    }

    private  bool CanAvoidAttack(CharacterStats _target)
    {
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();
        if (isShocked)
        {
            totalEvasion += 20;
        }
        if (Random.Range(0, 100) < totalEvasion)
        {
           
            return false;
        }

        return true;
    }
    public virtual void DoPhysicalDamage(CharacterStats _targets)
    {
        int totolDamage = damage.GetValue() + strength.GetValue();

        //处理暴击逻辑
        if (CanCrit())
        {
            totolDamage = CalculateCriticalDamage(totolDamage);
        }
        //处理护盾逻辑
        totolDamage -= _targets.armer.GetValue();
        totolDamage = Mathf.Clamp(totolDamage, 0, int.MaxValue);
        _targets.TakeDamage(totolDamage);
        
    }
    public virtual void DoMagicalDamage(CharacterStats  _targets)
    {
        int _fireDamage=fireDamage.GetValue();
        int _iceDamage=iceDamage.GetValue();
        int _lightingDamage=lightingDamage.GetValue();

        int totolDamage=_fireDamage+_iceDamage+_lightingDamage+intelligence.GetValue();
        totolDamage-=_targets.magicResistance.GetValue()+(_targets.intelligence.GetValue()*3);
        totolDamage=Mathf.Clamp(totolDamage,0,int.MaxValue);

        if(isChilled)
        {
            totolDamage = Mathf.RoundToInt(totolDamage * 0.8f);
        }

        _targets.TakeDamage(totolDamage);
    }

    public virtual void DoMagicEf(CharacterStats _targets)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        if(Mathf.Max(_fireDamage,_iceDamage,_lightingDamage)<=0)
        {
            return;
        }

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill=_iceDamage>_fireDamage&&_iceDamage > _lightingDamage;
        bool canApplyShock=_lightingDamage>_fireDamage && _lightingDamage>_iceDamage;
        int igni=canApplyIgnite?1:0;
        int chill=canApplyChill?1:0;
        int shock=canApplyShock?1:0;
        if((igni+chill+shock)==1)
        {
            _targets.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
            if (canApplyIgnite)
            {
                _targets.SetupIgniteDamage(fireDamage.GetValue());
            }

            return;
        }
        
      

        while(!canApplyShock&&!canApplyIgnite&&!canApplyChill)
        {
            if(Random.value<0.5f&&_fireDamage>0)
            {
                canApplyIgnite=true;
                if (canApplyIgnite)
                {
                    _targets.SetupIgniteDamage(fireDamage.GetValue());
                }
                _targets.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if(Random.value<0.5f&&_iceDamage>0)
            {
                canApplyChill=true;
                _targets.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if(Random.value<0.5f&&_lightingDamage>0)
            {
                canApplyShock=true;
                _targets.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }
       

    }

    public void ApplyAilments(bool _ignite,bool _chill,bool _shock)
    {
        if(isIgnited||isChilled||isShocked)
        {
            return;
        }

        if(_ignite)
        {
            ignitTimer = ailmentDuration;
           

            fx.IgniteFxFor(ailmentDuration);
        }
        if(_chill)
        {
            chilledTimer = ailmentDuration;
           
            fx.ChillFxFor(ailmentDuration);
            GetComponent<Entity>().SlowEntity(slowPercentage,ailmentDuration);
        }
        if(_shock)
        {
            shockedTimer = ailmentDuration;
           
            fx.ShockFxFor(ailmentDuration);
        }

        isIgnited = _ignite;
        isChilled= _chill;
        isShocked = _shock;
    }

    protected virtual void TakeDamage(int _damage)
    {
        DecreaseHealth(_damage);
        currentHealth-=_damage;
        

        if (currentHealth < 0)
        {
        
            Die();
        }
    }    
    protected virtual void DecreaseHealth(int _Damage)
    {
        currentHealth -= _Damage;
        if(onHealthChanged!=null)
        {
            onHealthChanged();
        }
    }


    protected virtual void Die()
    {
        isDead = true;
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
    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue()+vitality.GetValue()*5;
    }
}
