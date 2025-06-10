using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using War.io.Movement;
using War.io.PickUp;
using War.io.Shooting;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        public event Action<BaseCharacter> Dead;
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        protected float _maxHealth = 2f;

        [SerializeField]
        protected Image _healthBar;

        [SerializeField]
        protected float _health = 2f;

        [SerializeField]
        private ParticleSystem _damageParticle;

        [SerializeField]
        private ParticleSystem _deathParticle;

        [SerializeField]
        private AudioSource _deathAudio;


        public bool _isDeath = false;

        private IMovementDirectionSource _movementDirectionSource;

        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;



        protected void Awake()
        {
            _health = _maxHealth;
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.MovementDirection = direction;

            if (_isDeath) return;

            var lookDirection = direction;

            if (_shootingController.HasTarget)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            }
            _characterMovementController.LookDirection = lookDirection;


            _animator.SetBool("IsMoving", direction != Vector3.zero);
            _animator.SetBool("IsShooting", _shootingController.HasTarget);


            if (_health <= 0f)
            {
                _deathParticle.Play();
                _deathAudio.Play();
                Death();
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _damageParticle.Play();
                _health -= bullet.Damage;
                _healthBar.fillAmount = _health / _maxHealth;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }

        public void Death()
        {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {

                gameObject.layer = LayerMask.NameToLayer("Dead");
                _isDeath = true;

                _animator.SetTrigger("Death");

                yield return null;

                AnimatorStateInfo state;
                do
                {
                    yield return null;
                    state = _animator.GetCurrentAnimatorStateInfo(2);
                }
                while (!state.IsName("Death"));

                yield return new WaitForSeconds(state.length);

            Dead?.Invoke(this);
            Destroy(gameObject);
        }

    }
}
