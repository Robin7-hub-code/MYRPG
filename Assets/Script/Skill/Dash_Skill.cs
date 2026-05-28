using UnityEngine;

public class Dash_Skill : Skill
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseSkill()
    {
        
        base.UseSkill();
        Debug.Log("Dashing");
    }
}
