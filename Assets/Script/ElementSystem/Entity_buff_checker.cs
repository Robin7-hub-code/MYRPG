using UnityEngine;
public enum Element_reaction
{
    Zhengfa,
    Fushi,
    Posui,
    Ronghua,
    Tunshi,
    None_react
}
public class Entity_buff_checker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Entity_buff_checker instance; 
    public void Awake()
    {
       instance = this;
    }
    public Element_reaction check_if_Boom(Entity_Current_buff entityBuff)
    {
        if(entityBuff.fire&&entityBuff.water)
        {
            entityBuff.fire = false;
            entityBuff.water = false;
            return Element_reaction.Zhengfa;
        }    
        if (entityBuff.fire && entityBuff.stone)
        {
            entityBuff.fire= false;
            entityBuff.stone = false;
            return Element_reaction.Posui;
        }
        if (entityBuff.stone && entityBuff.electric)
        {
            entityBuff.electric = false;
            entityBuff.stone = false;
            return Element_reaction.Ronghua;
        }
        if(entityBuff.electric&&entityBuff.wood)
        {
            entityBuff.wood = false;
            entityBuff.electric = false;
            return Element_reaction.Tunshi;
        }
        if(entityBuff.wood&&entityBuff.water)
        {
            entityBuff.water = false;
            entityBuff.wood = false;
            return Element_reaction.Fushi;
        }
        return Element_reaction.None_react;
    }
}
