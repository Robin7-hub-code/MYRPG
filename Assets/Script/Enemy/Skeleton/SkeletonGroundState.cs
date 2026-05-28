using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected EnemySkeleton enemy;
    protected Transform player;
    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.alertTime;
        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();
        if(enemy.isPlayerDetected()||Vector2.Distance(enemy.transform.position,player.position)<2)
        {
            if (enemy.isPlayerDetected().distance < enemy.attackDistance && this.GetType().Name == "SkeletonIdleState")
            {
                if (stateTimer < 0)
                    stateMachine.ChangeState(enemy.battleState);
                else return;
            }
            else
            {
                stateMachine.ChangeState(enemy.battleState);
            }
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
