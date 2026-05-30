using UnityEngine;


public class PlayerStats : CharacterStats
{
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
            base.Start();
        player = GetComponent<Player>();
    }

    protected override void TakeDamage(int _dama)
    {
        base.TakeDamage(_dama);
        player.DamageEf();
    }

    protected override void Die() 
    {
            base.Die();
           player.Die();
        //player.rb.bodyType= RigidbodyType2D.Kinematic;
    }

    
}
