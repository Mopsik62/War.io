using UnityEditor;
using UnityEngine;
using War.io.Enemy;
using War.io.Camera;

namespace War.io.PickUp
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter _player;

        [SerializeField]
        private EnemyCharacter _enemy;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private float _spawnIntervalSeconds = 5f;

        private float _currentSpawnTimerSeconds;


        //private int _currentCount;
        protected void Start()
        {
            //_spawnIntervalSeconds = GetRandomInterval(_spawnIntervalMinSeconds, _spawnIntervalMaxSeconds);
        }
        protected void Update()
        {
            var playerExists = FindObjectOfType<PlayerCharacter>() != null;

            _currentSpawnTimerSeconds += Time.deltaTime;

                if (_currentSpawnTimerSeconds > _spawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;
                if (playerExists)
                {
                    Instantiate(_enemy, randomPosition, Quaternion.identity);
                }
                else
                {
                    if (Random.value < 0.5f)
                    {
                      Instantiate(_enemy, randomPosition, Quaternion.identity);
                    }
                    else
                    {
                       var spawnedPlayer = Instantiate(_player, randomPosition, Quaternion.identity);
                       UnityEngine.Camera.main.GetComponent<CameraController>()?.SetPlayer(spawnedPlayer);
                    }
                }

            }
            
        }


        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }



    }
}