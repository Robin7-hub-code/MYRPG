using UnityEngine;

public class Reaction_Ronghua : Reaction_base
{
    public Reaction_Ronghua()
    {

    }
    public override void processReact(Entity_Current_buff entity)
    {
        base.processReact(entity);
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("I m in Ronghua Reaction");
    }
}
