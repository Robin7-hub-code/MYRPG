using UnityEngine;

public class SkeletonStunState : EnemyState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemySkeleton enemy;

    public SkeletonStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTimer=enemy.stunTime;
        rb.linearVelocity = new Vector2(-enemy.faceDir * enemy.stunDir.x, enemy.stunDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelColorBlink",0);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }
}
