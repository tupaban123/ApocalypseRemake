using Apocalypse.StatesMachine;
using Apocalypse.Enemy.States;
using Pathfinding;
using UnityEngine;
using Apocalypse.Player;

namespace Apocalypse.Enemy
{
    public class EnemyModel
    {
        private EnemyView _enemyView;
        private EnemyMovement _enemyMovement;

        private StateMachine _enemyStateMachine;
        
        private EnemyChaseState _enemyChaseState;
        private EnemyAttackState _enemyAttackState;
        
        private PlayerView _player;

        public EnemyView EnemyView => _enemyView;
        public EnemyMovement EnemyMovement => _enemyMovement;

        public EnemyModel(EnemyView enemyView, AIPath aiPath, AIDestinationSetter destinationSetter, PlayerView player)
        {
            _enemyView = enemyView;
            _enemyMovement = new EnemyMovement(aiPath, destinationSetter);

            _player = player;

            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _enemyStateMachine = new StateMachine();

            InitializeStates();

            _enemyStateMachine.AddState(_enemyChaseState);
            _enemyStateMachine.AddState(_enemyAttackState);

            _enemyStateMachine.SetState<EnemyChaseState>();
        }

        private void InitializeStates()
        {
            Transform playerTransfom = _player.Transform;

            _enemyChaseState = new EnemyChaseState(_enemyStateMachine, this, playerTransfom);
            _enemyAttackState = new EnemyAttackState(_enemyStateMachine, this, _enemyView);
        }

        public void Update()
        {
            _enemyStateMachine.Update();
        }
    }
}
