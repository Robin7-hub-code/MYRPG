using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Crystal_Controller : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    private float crystalExistTimer;
    private bool canMove;
    private bool canExplode;
    private float moveSpeed;
    private Transform closetEnemy;
    public float defaultExplodeDistance = 10;
    public void SetupCrystal(float _crystalDuration,bool _canExplode,bool _canMove,float _moveSpeed,Transform _closetEnemy)
    {
        crystalExistTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        closetEnemy = _closetEnemy;
    }

    private void Update()
    {
        crystalExistTimer-= Time.deltaTime; 
        if(crystalExistTimer<0)
        {
            FinishCrystal();
        }

        if (canMove)
        {
            if(closetEnemy!=null)
            {
                transform.position = Vector2.MoveTowards(transform.position, closetEnemy.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, closetEnemy.position) < 1)
                {
                  FinishCrystal();
                  canMove = false;
                 }

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(defaultExplodeDistance* PlayerManager.instance.player.faceDir, 0), moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, new Vector3(defaultExplodeDistance * PlayerManager.instance.player.faceDir, 0)) < 1)
                {
                    FinishCrystal();
                    canMove = false;
                }
            }
        }
           
    }
    
    public void FinishCrystal()
    {
        if (canExplode)
        {
            anim.SetTrigger("Explode");
            canMove = false;
        }
        else
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy() => Destroy(gameObject); 

    private void AnimationTriggerEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
               
                PlayerManager.instance.player.stats.DoMagicEf(hit.GetComponent<Enemy>().stats);
                PlayerManager.instance.player.stats.DoMagicalDamage(hit.GetComponent<Enemy>().stats);
            }
        }
    }

}
