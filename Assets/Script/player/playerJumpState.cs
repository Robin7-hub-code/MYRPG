using UnityEngine;

public class PlayerJumpState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
