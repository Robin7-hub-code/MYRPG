using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        enemy=GetComponent<Enemy>();    
    }


    // Update is called once per frame
    protected override void TakeDamage(int _dama)
    {
        base.TakeDamage(_dama);
        enemy.DamageEf();
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
