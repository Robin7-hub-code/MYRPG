using UnityEngine;

public class PlayerState 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected PlayerStateMachine  stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    private string animBoolName;
    protected bool triggerCalled;
    public PlayerState(Player _player,PlayerStateMachine _stateMachine,string _aniBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _aniBoolName;
    }
    public virtual void Enter()
    {
        rb = player.rb;
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Update()
    {
       
        stateTimer-= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.linearVelocityY);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
