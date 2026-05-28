using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy )
    {
      
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
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
            stateMachine.ChangeState(enemy.moveState);
            return;
        }
    }
}
