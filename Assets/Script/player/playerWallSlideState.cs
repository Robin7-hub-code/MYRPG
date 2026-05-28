using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
        }
        if (xInput != 0 && player.faceDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (yInput < 0)
        {
     
            rb.linearVelocityY = rb.linearVelocityY;
        }
        else
        {
            rb.linearVelocityY = 0.9f * rb.linearVelocityY;
        }
        if(!player.isWallDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
