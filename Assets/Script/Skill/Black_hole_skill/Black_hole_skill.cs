using UnityEngine;

public class Black_hole_skill : Skill
{
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxSize;
    [SerializeField]private float  growSpeed;
    [SerializeField] private float shrinkSpeed;
    [SerializeField]private float  cloneAttackCoolDown = 0.3f;
    [SerializeField]private int    numOfAttack = 4;
    [SerializeField] private float blackholeDuration;

    Black_hole_skill_controller new_controller;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newHole=Instantiate(blackholePrefab,player.transform.position,Quaternion.identity);
       new_controller=newHole.GetComponent<Black_hole_skill_controller>();
        new_controller.SetupBlackHole(maxSize, growSpeed, shrinkSpeed, numOfAttack, cloneAttackCoolDown,blackholeDuration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    public bool BlackHoleFinished()
    {
        if(!new_controller)
        {
            return false;
        }
        if(new_controller.playerCanExitState)
        {
            new_controller = null;
            return true;
        }
        return false;
    }
}
