using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        player.SetZeroVelocity();
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
       
        if (xInput!=0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
