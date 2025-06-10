using War.io.FSM;
using UnityEngine;

namespace War.io.Enemy.States
{
    internal class DeathState : BaseState
    {

        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly EnemyCharacter _enemy;


        public DeathState( EnemyDirectionController enemyDirectionController, EnemyCharacter enemy)
        {
            _enemyDirectionController = enemyDirectionController;
            _enemy = enemy;
        }
        public override void Execute()
        {
            _enemyDirectionController.MovementDirection = Vector3.zero;
            Debug.Log("Death state " + _enemyDirectionController.MovementDirection);
        }
    }
}
