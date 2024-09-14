using Pathfinding;
using System;
using UnityEngine;

namespace Apocalypse.Enemy
{
    public class EnemyMovement
    {
        private AIPath _aiPath;
        private AIDestinationSetter _destinationSetter;

        public bool CanMove 
        { 
            get 
            { 
                return _aiPath.canMove;
            } 
            set
            {
                _aiPath.canMove = value;
            }
        }

        public EnemyMovement(AIPath aiPath, AIDestinationSetter destinationSetter)
        {
            _aiPath = aiPath;
            _destinationSetter = destinationSetter;
        }

        public void SetTargetToChase(Transform target)
        {
            _destinationSetter.target = target;
        }

        public void SubscribeTargetReachedCallback(Action callback) => _aiPath.OnTargetReach += callback;

        public void UnsubscribeTargetReachedCallback(Action callback) => _aiPath.OnTargetReach -= callback;
    }
}