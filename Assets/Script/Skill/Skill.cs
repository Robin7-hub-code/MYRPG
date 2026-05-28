using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Skill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] protected float coolDown;
    [SerializeField]protected float coolDownTimer;
    protected Player player;
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
    {
        if(coolDownTimer<0)
        {
            coolDownTimer = coolDown;
            UseSkill();
            return true;
        }
        return false;
    }
    public virtual void UseSkill()
    {

    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        float closestDis = Mathf.Infinity;
        Transform closet=null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float dis = Vector2.Distance(_checkTransform.position, hit.transform.position);
                if (dis < closestDis)
                {
                    closestDis = dis;
                    closet = hit.transform;
                }
            }
        }
        return closet;
    }
}
