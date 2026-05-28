using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.linearVelocityY==0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSliderState);
        }
        if (xInput!=0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f*xInput, rb.linearVelocityY);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
