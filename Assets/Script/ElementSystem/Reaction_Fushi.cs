using UnityEngine;

public class Reaction_Fushi :Reaction_base
{
     private float hurt;
     private float duration;
    public Reaction_Fushi(float hurt, float duration)
    {
        this.hurt = hurt;
        this.duration = duration;
    }

    public override void processReact(Entity_Current_buff entity)
    {
        base.processReact(entity);
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("I m in Fushi Reaction");
    }
}
