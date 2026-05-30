using System.Collections.Generic;
using UnityEngine;

public class Sword_Skiill_Controller : MonoBehaviour
{
    private float returnSpeed;
    private float disapearDistance;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D cd;
    private bool canRotate = true;
    private bool isReturning = false;
    private Player player;
    private float numOfPierce;
    private bool isPierce;

    private float destroyTime;

    private float bounceSpeed;
    private bool isBouncing;
    private int numOfBounce;
    private List<Transform> enemyTarget = new List<Transform>();
    private int targetIndex;

    //时空间冻结
    private float freezeTimeDuration;

    [Header("旋转剑相关")]
    private float spinMoveSpeed;
    private float maxDis;
    private float spinDuration;
    private float spinTimer;
    private bool isStopped;
    private bool isSpin;

    private float spinDir;

    private float hitTimer;
    private float hitCoolDown;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        player = PlayerManager.instance.player;
    }
    public  void DestroyMe()
    {
        Destroy(gameObject);
    }
    public void SetupBounce(bool _isBouncing,int _numOfBouncing,float _bounceSpeed)
    {
        isBouncing = _isBouncing;
        numOfBounce = _numOfBouncing;
        bounceSpeed= _bounceSpeed;
    }
    public void SetUpPierce(bool _isPierce,int _numOfPerce)
    {
        numOfPierce=_numOfPerce;
        isPierce = _isPierce;
    }
    public void SetUpSpin(bool _isSpin,float _maxDis,float _spinDuration,float _hitCoolDown,float _spinMoveSpeed)
    {
        isSpin=_isSpin;
        maxDis=_maxDis;
        spinDuration=_spinDuration;
        hitCoolDown=_hitCoolDown;
        spinMoveSpeed=_spinMoveSpeed;
    }
    public void SetUpSword(Vector2 _dir, float _gravity,float _freezeTimeDuration,float _returnSpeed,float _disapearDistance,float destroyTime)
    {
        returnSpeed = _returnSpeed;
        disapearDistance= _disapearDistance;
        freezeTimeDuration=_freezeTimeDuration;
        rb.linearVelocity = _dir;
        rb.gravityScale = _gravity;
        if (numOfPierce <= 0)
        {
            anim.SetBool("Rotation", true);
        }

        spinDir = Mathf.Clamp(rb.linearVelocityX, -1, 1);
        Invoke("DestroyMe", destroyTime);
    }
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        cd.enabled = true;
        isReturning = true;

    }
    private void Update()
    {
        if (canRotate)
        {
            transform.right = rb.linearVelocity;
        }
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < disapearDistance)
            {
                player.ClearSword();
                isReturning = false;
            }
        }
        BounceLogic();
        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSpin)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxDis && !isStopped)
            {
                isStopped = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                spinTimer = spinDuration;
            }
            if (isStopped)
            {
                spinTimer -= Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDir, transform.position.y), spinMoveSpeed * Time.deltaTime);
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpin = false;
                }
                hitTimer -= Time.deltaTime;
               
                if (hitTimer < 0)
                {
                    hitTimer = hitCoolDown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                        {
                            SwordSkillDamage(hit.GetComponent<Enemy>());
                        }
                    }
                }
            }

        }
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].transform.position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].transform.position) < 0.1f)
            {
                SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
                targetIndex++;
                numOfBounce--;
                if (numOfBounce == 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }
    private void SwordSkillDamage(Enemy enemy)
    {
        enemy.DamageEf();
        PlayerManager.instance.player.stats.DoDamage(enemy.stats);
        if(player.skill.sword.timeStopUnlocked)
          enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
        {
            return;
        }
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
           SwordSkillDamage((Enemy)enemy);
        }
       
        SetUpTargetForBounce(collision);
        StuckInto(collision);
    }

    private void SetUpTargetForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if (numOfPierce > 0&&collision.GetComponent<Enemy>()!=null)
        {
           numOfPierce--;
            return;
        }
        if(isSpin)
        {
            return;
        }
        canRotate = false;
        cd.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log(isBouncing);
        if (isBouncing && enemyTarget.Count > 0)
        {
            return;
        }
        transform.parent = collision.transform;
        anim.SetBool("Rotation", false);
    }
}
