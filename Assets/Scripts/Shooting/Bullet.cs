using UnityEngine;

namespace War.io.Shooting
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;
        private float _flySpeed;
        private float _maxFlyDistance;
        private float _currentFlyDistance;

        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed)
        {
            _direction = direction;
            _flySpeed = flySpeed;
            _maxFlyDistance = maxFlyDistance;
        }

        // Update is called once per frame
        protected void Update()
        {
            var delta = _flySpeed * Time.deltaTime;
            _currentFlyDistance += delta;
            transform.Translate(_direction * delta);

            if (_currentFlyDistance >= _maxFlyDistance)
                Destroy(gameObject);
        }
    }
}
