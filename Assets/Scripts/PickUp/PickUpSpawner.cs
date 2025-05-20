using UnityEditor;
using UnityEngine;

namespace War.io.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        /*        [SerializeField]
                private float _spawnIntervalSeconds = 10f;*/

        [SerializeField]
        private float _spawnIntervalMinSeconds = 2f;
        [SerializeField]
        private float _spawnIntervalMaxSeconds = 10f;
        [SerializeField]
        private float _spawnIntervalSeconds;

        private float _currentSpawnTimerSeconds;


        private int _currentCount;
        protected void Start()
        {
            _spawnIntervalSeconds = GetRandomInterval(_spawnIntervalMinSeconds, _spawnIntervalMaxSeconds);
        }
        protected void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;

                if (_currentSpawnTimerSeconds > _spawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;
                    _spawnIntervalSeconds = GetRandomInterval(_spawnIntervalMinSeconds, _spawnIntervalMaxSeconds);
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickup = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickup.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp (PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
        protected float GetRandomInterval(float min, float max)
        {
            float value = Random.Range(min, max);
            return value;
        }


    }
}