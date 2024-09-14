using Apocalypse.StatesMachine;
using UnityEngine;

namespace Apocalypse.Enemy.States
{
    public class EnemyChaseState : State
    {
        private EnemyModel _enemy;
        private EnemyMovement _enemyMovement;

        private Transform _targetToChase;

        public EnemyChaseState(StateMachine stateMachine, EnemyModel enemy, Transform target) 
            : base(stateMachine)
        {
            _enemy = enemy;
            _enemyMovement = enemy.EnemyMovement;

            _targetToChase = target;
            _enemyMovement.SetTargetToChase(_targetToChase);
            _enemyMovement.CanMove = false;
        }

        public override void Enter()
        {
            _enemyMovement.SubscribeTargetReachedCallback(OnTargetReached);
            _enemyMovement.CanMove = true;
            base.Enter();
        }

        public override void Exit() 
        {
            _enemyMovement.UnsubscribeTargetReachedCallback(OnTargetReached);
            _enemyMovement.CanMove = false;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }

        private void OnTargetReached()
        {
            stateMachine.SetState<EnemyAttackState>();
        }
    }
}