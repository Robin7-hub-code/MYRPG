using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flyTime=0.4f;
    private bool skillUsed;
    private float degravity;
    public PlayerBlackHoleState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        degravity=player.rb.gravityScale;
        skillUsed = false;
        stateTimer=flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = degravity;
        player.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer>0)
        {
            rb.linearVelocityY = 15;
            rb.linearVelocityX = 0;
        }
        else
        {
            rb.linearVelocityY = -0.1f;
            rb.linearVelocityX = 0;

            if(!skillUsed)
            {
              
                player.skill.blackhole.CanUseSkill();
                skillUsed = true;
            }
        }
        if(player.skill.blackhole.BlackHoleFinished())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
