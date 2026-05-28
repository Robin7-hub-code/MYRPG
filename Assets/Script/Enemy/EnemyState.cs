using UnityEngine;

public class EnemyState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string animBoolName;
    protected float stateTimer;
    public EnemyState(Enemy _enemyBase,EnemyStateMachine _stateMachine,string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Update()
    {
        stateTimer-=Time.deltaTime;
    }
    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    { 
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimation(animBoolName);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
