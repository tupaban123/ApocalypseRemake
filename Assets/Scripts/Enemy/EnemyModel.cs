using Apocalypse.StatesMachine;
using Apocalypse.Enemy.States;
using Pathfinding;
using UnityEngine;

namespace Apocalypse.Enemy
{
    public class EnemyModel
    {
        private EnemyView _enemyView;
        private StateMachine _enemyStateMachine;

        private AIPath _aiPath;

        [Header("States")]
        [SerializeField] private EnemyChaseState EnemyChaseState;
        [SerializeField] private EnemyAttackState EnemyAttackState;

        public EnemyModel(EnemyView enemyView, AIPath aiPath)
        {
            _enemyView = enemyView;
            _aiPath = aiPath;

            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _enemyStateMachine = new StateMachine();

            _enemyStateMachine.AddState(EnemyChaseState);
            _enemyStateMachine.AddState(EnemyAttackState);
        }
    }
}
