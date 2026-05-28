using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    public override void Update()
    {
       base.Update();
        if (stateMachine.currentState != this)
        {
            return;
        }
        player.SetVelocity(xInput*player.moveSpeed, rb.linearVelocityY);
        if (xInput==0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
