using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("玩家层级")]
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("移动速度")]
    [SerializeField] public float moveSpeed;
    private float defaultMoveSpeed;
    [Header("待机时间")]
    [SerializeField] public float idleTime;
    [Header("警告时间")]
    [SerializeField] public float alertTime;
    [Header("攻击相关")]
    [SerializeField] public float attackDistance;
    [Header("嘲讽时间")]
    [SerializeField] public float angryTime;
    [Header("眩晕时间和方向大小")]
    [SerializeField] public float stunTime;
    public Vector2 stunDir;
    protected bool IsInStunTime;
    [SerializeField] protected GameObject counterImage;
    [HideInInspector]public float lastTimeAttack;
    public float attackCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string lastAnimBoolName { get; private set; }
  public EnemyStateMachine stateMachine { get; private set; }

    public override void SlowEntity(float _slowPercentage, float _slowDuration)
    {
        base.SlowEntity(_slowPercentage, _slowDuration);
        moveSpeed = moveSpeed*(1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);
        Invoke(nameof(ReturnDefaultSpeed), _slowDuration);
    }

    public override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed=defaultMoveSpeed;
    }

    public virtual void AssignLastAnimation(string _ani)
    {
        lastAnimBoolName = _ani;
    }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        if(isPlayerDetected())
          Debug.Log(isPlayerDetected().collider.gameObject.name + "I see");
    }
    public virtual void FreezeTimer(bool _timeFrozen)
    {
        if(_timeFrozen)
        {
            moveSpeed = 0f;
            anim.speed = 0f;
        }
        else
        {
            moveSpeed=defaultMoveSpeed;
            anim.speed = 1f;
        }
    }

    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTimer(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTimer(false);
    }
    public virtual void AnimationFinishTrigger()=>stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * faceDir, 50, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * faceDir, transform.position.y, 0));
    }
    public virtual void OpenCounterAttackWindow()
    {
        IsInStunTime = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        IsInStunTime= false;
        counterImage.SetActive(false);
    }
    public virtual bool IsStunned()
    {
        if(IsInStunTime)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
}
