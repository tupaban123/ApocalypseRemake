using Apocalypse.CodeBase;
using Apocalypse.Entity;
using Apocalypse.Player;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Apocalypse.Enemy
{
    public class EnemyView : MonoBehaviour, ICoroutineRunner, IDamageable
    {
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private AIDestinationSetter _destinationSetter;

        private EnemyModel _enemyModel;

        [Inject] private PlayerView _player;

        public Transform Transform { get; private set; }

        public EntityType EntityType => EntityType.Enemy;

        private void Start()
        {
            _aiPath ??= GetComponent<AIPath>();
            _destinationSetter ??= GetComponent<AIDestinationSetter>();

            Transform = transform;

            Initialize();
        }

        public void Initialize()
        {
            _enemyModel = new EnemyModel(this, _aiPath, _destinationSetter, _player);
        }

        private void Update()
        {
            _enemyModel.Update();
        }

        public void Damage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public float GetDistanceToPlayer() => Vector3.Distance(Transform.position, _player.Transform.position);
    }
}