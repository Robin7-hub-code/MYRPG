using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float moveSpeed=7.0f;
    public float jumpForce;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    private float defaultDashSpeed;
    [Header("AttackDetails")]
    public Vector2 []attackMoveMent;
    public float counterAttackTime=0.1f;

    [Header("Sword Return Impact")]
    [SerializeField] public float swordReturnImpact=8.0f; 
    public float dashDir { get; private set; }

   

  

   
 
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }

    public PlayerWallSlideState wallSliderState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }

    public PlayerAimSwordState aimSwordState { get; private set; }

    public PlayerBlackHoleState blackHoleState { get; private set; }

    public PlayerDeathState deathState { get; private set; }
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }


    public override void SlowEntity(float _slowPercentage, float _slowDuration)
    {
        base.SlowEntity(_slowPercentage, _slowDuration);
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce= jumpForce * (1 - _slowPercentage);
        dashSpeed= dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed*(1 - _slowPercentage);

        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);
    }
    public override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        //设置这些状态属于哪个plyer 属于哪个状态机stateMachine
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
         dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSliderState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "PrimaryAttack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        //剑投掷
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Jump");
        deathState = new PlayerDeathState(this, stateMachine, "Die");
    }
    protected override void Start()
    {
       base.Start();
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
        skill=SkillManager.instance;
        stateMachine.Initialize(idleState);//设置初始当前currentState
    }

  

   protected override void Update()
    {
        base.Update();
      stateMachine.currentState.Update();//调用当前状态的update方法
      CheckForDashInput();
        if(Input.GetKeyDown(KeyCode.F))
        {
            skill.crystal.CanUseSkill();
        }
    }
    private void CheckForDashInput()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftShift)&&SkillManager.instance.dash.CanUseSkill())
        {
           
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = faceDir;
            }
            stateMachine.ChangeState(dashState);
        }
    }
  
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
   
    public void AssignSword(GameObject _newSword)
    {
        sword=_newSword;
    }
    public void ClearSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}
