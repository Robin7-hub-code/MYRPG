using Unity.VisualScripting;
using UnityEngine;

public class Reaction_Zhengfa : Reaction_base
{
    private float hurt;
    public Reaction_Zhengfa(float hurt)
    {
        this.hurt = hurt;
    }

    public override void processReact(Entity_Current_buff entity)
    {
        base.processReact(entity);
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("I m in Zhengfa Reaction");
    }
}
