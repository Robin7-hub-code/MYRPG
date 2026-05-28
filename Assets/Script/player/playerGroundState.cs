using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackHoleState);
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)&&HasNoSword())
        {
            stateMachine.ChangeState(player.aimSwordState);
            return;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
            return;
        }
        if(!player.isGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected()) 
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        
    }
    private bool HasNoSword()
    {
        if(!player.sword)//判断是否将剑丢出，丢出视为角色拥有了一把剑
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skiill_Controller>().ReturnSword();
        return false;
    }
}
