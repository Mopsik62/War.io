using UnityEngine;
using War.io.Movement;
using War.io.Shooting;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;

        private IMovementDirectionSource _movementDirectionSource;

        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;



        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            _shootingController.SetWeapon(_baseWeaponPrefab, _hand);
        }

        // Update is called once per frame
        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.Direction = direction;
        }
    }
}
