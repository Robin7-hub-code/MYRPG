using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private EnemySkeleton enemy=>GetComponentInParent<EnemySkeleton>();
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target=hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
                //hit.GetComponent<Player>().Damage();
            }
        }
    }
    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow()=>enemy.CloseCounterAttackWindow();
}
