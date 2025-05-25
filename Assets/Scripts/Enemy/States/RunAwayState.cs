using War.io.FSM;
using UnityEngine;

namespace War.io.Enemy.States
{
    internal class RunAwayState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyCharacter _this;
        private readonly EnemyDirectionController _enemyDirectionController;

        private Vector3 _currentPoint;

        public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController, EnemyCharacter enemy)
        {
            _target = target;
            _this = enemy;
            _enemyDirectionController = enemyDirectionController;
        }

        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            Vector3 enemyPosition = _this.transform.position;

            Vector3 directionRunAway = (enemyPosition - targetPosition).normalized;

            Vector3 runAwayPosition = enemyPosition + directionRunAway;

            if (_currentPoint != runAwayPosition)
            {
                _currentPoint = runAwayPosition;
                _enemyDirectionController.UpdateMovementDirection(runAwayPosition);
            }
        }
    }
}
