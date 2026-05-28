
using System.Resources;
using UnityEngine;

public class EnemySkeleton : Enemy
{

    #region 状态
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }

    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunState stunState { get; private set; }

    public SkeletonDeathState deathState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunState = new SkeletonStunState(this, stateMachine, "Stun", this);
        deathState = new SkeletonDeathState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.O))
        {
            stateMachine.ChangeState(stunState);
        }
    }
    public override bool IsStunned()
    {
        if (base.IsStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}
