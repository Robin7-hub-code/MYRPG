using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
   
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName,_enemy)
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
        enemy.SetVelocity(enemy.moveSpeed* enemy.faceDir, rb.linearVelocityY);
        if(enemy.isWallDetected()||!enemy.isGroundDetected())
        {
           
            enemy.rb.linearVelocityX = 0;
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
