using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;
    //这里是特殊设置
    private float angryStateTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy= _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();
        angryStateTimer-=Time.deltaTime;
        if (enemy.isPlayerDetected())
        {
            angryStateTimer=enemy.angryTime;
           if(enemy.isPlayerDetected().distance<enemy.attackDistance)
           {
                if(CanAttack())
                {
                       stateMachine.ChangeState(enemy.attackState);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(enemy.idleState);
                    return;
                }
                   
           }
        }
        else
        {
            if(angryStateTimer<0||Vector2.Distance(player.transform.position,enemy.transform.position)>7)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }
    private bool CanAttack()
    {
        if(Time.time>=enemy.lastTimeAttack+enemy.attackCoolDown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}
