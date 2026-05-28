using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("被攻击相关")]
    [SerializeField] protected Vector2 knockbackDir;
    private bool isKnocked;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }

    public SpriteRenderer sr { get; private set; }

    public CharacterStats stats;

    public CapsuleCollider2D cd { get; private set; }
    public int faceDir { get; private set; } = 1;
    protected bool facingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx=GetComponentInChildren<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {

    }
    #region 速度
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
        {
            return;
        }
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public void SetZeroVelocity()
    {
        if (isKnocked)
        {
            return;
        }
        rb.linearVelocity = new Vector2(0, 0);
    }
    #endregion


    #region 碰撞检测
    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * faceDir, wallCheckDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * faceDir, wallCheck.position.y, 0));
    }
    #endregion
    #region 翻转控制
    public virtual void Flip()
    {
        faceDir = faceDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
    public virtual void DamageEf()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
    }
    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.linearVelocityX = knockbackDir.x * -faceDir;
        rb.linearVelocityY = knockbackDir.y;
        yield return new WaitForSeconds(0.07f);
        isKnocked = false;
    }
    public void MakeTransparent(bool transparent)
    {
        if (transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }
    public virtual void Die()
    {

    }
}
