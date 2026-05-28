using UnityEngine;

public class Reaction_Posui : Reaction_base
{
    public  Reaction_Posui()
    {

    }
    public override void processReact(Entity_Current_buff entity)
    {
        base.processReact(entity);
        entity.antifire = false;
        entity.antistone = false;
        entity.antiwater = false;
        entity.antiwood = false;
        entity.antielectric = false;

        entity.bond_health = 0;
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("I m in Posui Reaction");
    }
}
