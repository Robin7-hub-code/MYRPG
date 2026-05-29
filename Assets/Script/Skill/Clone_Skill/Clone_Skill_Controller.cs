using JetBrains.Annotations;
using Mono.Cecil.Cil;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private float colorLossSpeed;
    private float cloneTimer;
    private SpriteRenderer sr;
    private Animator anim;
    private Transform closet;
    [SerializeField] Transform attackCheck;
    [SerializeField] float attackCheckRadius;
    private float chaceToDupliacte;
    private bool canDuplicateClone;
    private int faceDir=1;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer-=Time.deltaTime;
   
        if (cloneTimer < 0)
        {
          
            sr.color=new Color(1,1,1,sr.color.a-Time.deltaTime*colorLossSpeed);
        }
        if (sr.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void SetUpClone(Transform _newTransform,float _cloneDuration,float _colorLossSpeed,bool _canAttack,Vector3 _offset,Transform closet,bool _canDuplicateClone,float _chaceToDupliacte)
    {
        if(_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 4));
        }
        transform.position=_newTransform.position+_offset;
        cloneTimer = _cloneDuration;
        this.closet = closet;
        colorLossSpeed = _colorLossSpeed;
        canDuplicateClone= _canDuplicateClone;
        chaceToDupliacte = _chaceToDupliacte;
        Debug.Log("i m set clone");
        FaceClosestTarget(); 
    }
    private void AnimationTrigger()
    {
        cloneTimer = -0.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().DamageEf();
                PlayerManager.instance.player.stats.DoDamage(hit.GetComponent<Enemy>().stats);
            }
            if (canDuplicateClone)
            {
                if (Random.Range(0, 100) < chaceToDupliacte)
                    SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(.5f * faceDir, 0, 0));
            }
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    private void FaceClosestTarget()
    {
        if(closet==null)
        {
            Debug.Log("没发现敌人");
            if(faceDir!=PlayerManager.instance.player.faceDir)
            {
                transform.Rotate(0, 180, 0);
                faceDir = -faceDir;
            }
        }

        if (closet != null)
        {
            if(transform.position.x>closet.position.x)
            {
                Debug.Log("克隆体位置"+transform.position.x);
                Debug.Log("最近敌人位置"+closet.position.x);
                faceDir = -1;
                Debug.Log("i m flipped");
                transform.Rotate(0, 180, 0);
            }
        }

    }
}
