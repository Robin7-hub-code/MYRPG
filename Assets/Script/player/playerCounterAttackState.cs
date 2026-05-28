using System.Collections;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        stateTimer=player.counterAttackTime;
        player.anim.SetBool("SucessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enemy>()!=null)
            {
                if (hit.GetComponent<Enemy>().IsStunned())
                {
                     stateTimer = 10;
                     player.anim.SetBool("SucessfulCounterAttack", true);
                    player.skill.clone.CreateCloneOnCounterAttack(hit.transform);
                }

            }
        }
        if(stateTimer<0||triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    
}
