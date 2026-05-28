using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


    public class SkeletonDeathState : EnemyState
    {

        private EnemySkeleton enemy;
        public SkeletonDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            enemy = _enemy;
           
            stateTimer = 0.1f;
        }



        public override void Enter()
        {
            base.Enter();
            enemy.cd.enabled = false;
            enemy.anim.speed = 0;
            enemy.anim.SetBool(enemy.lastAnimBoolName, true);
            
        }

        
        public override void Update()
        {
            base.Update();
            if (stateTimer > 0)
            {
                rb.linearVelocity = new Vector2(0, 10);
                
            }
        }
    }
