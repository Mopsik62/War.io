using UnityEngine;
using War.io.Enemy.States;

namespace War.io.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        private EnemyCharacter enemy;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            enemy = GetComponent<EnemyCharacter>();


            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius);
            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, enemy);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}