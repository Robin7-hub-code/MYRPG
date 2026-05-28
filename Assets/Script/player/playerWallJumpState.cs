using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.4f;
        player.SetVelocity(5 * -player.faceDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
