using Apocalypse.Player;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Apocalypse.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyModel _enemyModel;
        private AIPath _aiPath;

        [Inject] private PlayerView _player;

        private void Start()
        {
            _aiPath = GetComponent<AIPath>();
            Debug.Log($"Player: {_player != null}");

        }

        /*private void Update()
        {
            _aiPath.destination = _player.Transform.position;
        }*/

        public void Initialize()
        {
            _enemyModel = new EnemyModel(this, _aiPath);
        }
    }
}