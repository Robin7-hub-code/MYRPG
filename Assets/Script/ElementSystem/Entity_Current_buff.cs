using System.Security.Cryptography;
using UnityEngine;
public enum Base_ele
{
    Fire,
    Water,
    Stone,
    Electric,
    Wood,
    Zero
}
public enum Core_ele
{
    Light,
    Dark,
    Drug,
    None
}
public class Entity_Current_buff :MonoBehaviour
{
    [Header("角色附着元素")]
    [SerializeField]public bool fire;
    [SerializeField] public bool water;
    [SerializeField] public bool stone;
    [SerializeField] public bool electric;
    [SerializeField] public bool wood;
    [Header("角色抗性")]
    [SerializeField]public bool antifire;
    [SerializeField] public bool antiwater;
    [SerializeField] public bool antistone;
    [SerializeField] public bool antielectric;
    [SerializeField] public bool antiwood;
    [Header("角色触发的反应")]
    [SerializeField] public bool posui;
    [SerializeField] public bool ronghua;
    [SerializeField] public bool fushi;
    [SerializeField] public bool tunshi;
    [SerializeField] public bool zhengfa;
    [Header("角色护盾")]
    [SerializeField] public float bond_health;
    [Header("反应的数值")]
    [SerializeField] private float cheatPercentage;
    [SerializeField] private float zhengfaHurt;
    [SerializeField] private float fushiHurt;
    [SerializeField] private float fushiDuration;


    private Base_ele CurrentBaseElement;
    private Core_ele CurrentCoreElement = Core_ele.None;

    private Reaction_base react;//存储角色当前发生的反应

    private Element_reaction reaction;
    Entity_buff_checker checker = null;


    public void Start()
    {
        checker = Entity_buff_checker.instance;
    }
    #region 获得，设置 当前核心元素
    public void SetCurrentCore_element(Core_ele _CurrentCoreElement)
    {
        CurrentCoreElement = _CurrentCoreElement;
    }
    public Core_ele GetCurrent_Core()
    {
        return CurrentCoreElement;
    }
    #endregion
    #region 获得 设置 当前基本元素
    private void SetCurrentElement(Base_ele ele)
    {
        CurrentBaseElement = ele;
    }
    public Base_ele GetCurrent_Base()
    {
        return CurrentBaseElement;
    }
    #endregion
    public void Update()
    {
        if(react!=null)
         react.Update();
    }
    public void addElemnt(Base_ele base_ele)
    {
        switch (base_ele)
        {
            case Base_ele.Fire:
                fire = true;
                break;
            case Base_ele.Water: 
                water = true;
                break;
            case Base_ele.Stone: 
                stone = true;
                break;
            case Base_ele.Wood:
                wood = true; 
                break;
            case Base_ele.Electric:
                electric = true;
                break;
            default:
                break;

        }
        //检查角色当前状态是否会发生元素反应
        reaction = checker.check_if_Boom(this);
        //当前发生了反应
        if (reaction != Element_reaction.None_react)
        {
            processReaction(reaction);
        }
    }
    
    private void processReaction(Element_reaction reaction)
    {
        switch(reaction)
        {
            case Element_reaction.None_react:
                break;
            case Element_reaction.Fushi:
                fushi = true;
                react = new Reaction_Fushi(fushiHurt,fushiDuration);
                break;
            case Element_reaction.Posui:
                posui = true;
                react =new Reaction_Posui();
                break;
             case Element_reaction.Ronghua:
                ronghua = true;
                react =new Reaction_Ronghua();
                break;
            case Element_reaction.Tunshi:
                tunshi = true;
                react =new Reaction_Tunshi(cheatPercentage);
                break;
            case Element_reaction.Zhengfa:
                zhengfa = true;
                react =new Reaction_Zhengfa(zhengfaHurt);
                break;
            default :
                break;
        }
        react.processReact(this);
    }
}
