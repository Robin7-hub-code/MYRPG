using UnityEngine;

public class PlayerDashState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        player.skill.dash.CreateCloneOnDashStart();
    }

    public override void Exit()
    {
        base.Exit();
        player.skill.dash.CreateCloneOnDashOver();
        player.SetVelocity(0, rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();
        if (!player.isGroundDetected()&&player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSliderState);
            return;
        }
        player.SetVelocity(player.dashSpeed * player.faceDir, 0);
        if (stateTimer<0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
        
    }
}
