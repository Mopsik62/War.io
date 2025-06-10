using System.Collections;
using UnityEngine;
using War.io.Enemy;

namespace War.io.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _maxRadiansDelta = 10f;

        [SerializeField]
        private float _runAwayBoost = 0.1f;


        [SerializeField]
        private float _haste = 2f;
        public Vector3 MovementDirection { get;  set; }

        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;
        private EnemyCharacter _character;

        protected void Awake()
        {
            TryGetComponent(out _character);
            _characterController =GetComponent<CharacterController>();
        }   

        protected void Update()
        {
            Translate();

            if(_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
        }

        private void Translate()
        {
            Vector3 delta;
            float curSpeed = _speed;
            if (_character != null)
            {
                if (_character.RunAwayBool)
                curSpeed += _runAwayBoost;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                 delta = MovementDirection * curSpeed * _haste * Time.deltaTime;
            }
            else
            {
                 delta = MovementDirection * curSpeed * Time.deltaTime;
            }

            Debug.Log(MovementDirection);

            _characterController.Move(delta);
        }
        
        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

           if (sqrMagnitude> SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform .rotation = newRotation;
            }
        }
        public void Accelerate(float multiplier, float duration)
        {
            StartCoroutine(AccelerateCoroutine(multiplier, duration));
        }

        private IEnumerator AccelerateCoroutine(float multiplier, float duration)
        {
            float originalSpeed = _speed;
            _speed *= multiplier;

            yield return new WaitForSeconds(duration);

            _speed = originalSpeed;
        }

    }
}