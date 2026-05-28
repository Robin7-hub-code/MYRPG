using UnityEngine;

public class Reaction_Tunshi : Reaction_base
{
    private float cheatPercentage;
   public Reaction_Tunshi(float cheatPercentage)
    {
        this.cheatPercentage = cheatPercentage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void processReact(Entity_Current_buff entity)
    {
        base.processReact(entity);
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("I m in Tunshi Reaction");
    }
}
