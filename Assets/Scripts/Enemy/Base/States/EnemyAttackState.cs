using UnityEngine;
using Apocalypse.StatesMachine;
using Apocalypse.CodeBase;
using System.Collections;

namespace Apocalypse.Enemy.States
{
    public class EnemyAttackState : State
    {
        private EnemyModel _enemy;

        private ICoroutineRunner _coroutineRunner;

        private Coroutine AttackingCoroutine;

        public EnemyAttackState(StateMachine stateMachine, EnemyModel enemy, ICoroutineRunner coroutineRunner)
            : base(stateMachine)
        {
            _enemy = enemy;
            _coroutineRunner = coroutineRunner;
        }

        public override void Enter()
        {
            StartAttack();
            base.Enter();
        }

        public override void Exit()
        {
            if(AttackingCoroutine != null) 
                _coroutineRunner.StopCoroutine(AttackingCoroutine);
            
            AttackingCoroutine = null;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }

        private void StartAttack()
        {
            AttackingCoroutine = _coroutineRunner.StartCoroutine(Attacking());
        }

        private IEnumerator Attacking()
        {
            while (true)
            {
                if (_enemy.EnemyView.GetDistanceToPlayer() > 4)
                {
                    stateMachine.SetState<EnemyChaseState>();
                    yield break;
                }

                Debug.Log("Attack!");
                yield return new WaitForSeconds(0.35f);
            }
        }
    }
}
