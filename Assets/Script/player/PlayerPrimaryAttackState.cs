using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2.0f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter>2||Time.time>lastTimeAttacked+comboWindow)
        {
            comboCounter = 0;
        }
        float attackDir=player.faceDir;
        player.SetVelocity(player.attackMoveMent[comboCounter].x * attackDir, player.attackMoveMent[comboCounter].y*rb.linearVelocityY);
        player.anim.SetInteger("ComboCounter", comboCounter);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked=Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer<0)
           player.SetZeroVelocity();
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}
