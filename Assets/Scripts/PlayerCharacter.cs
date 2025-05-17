using UnityEngine;

namespace War.io
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerCharacter : MonoBehaviour
    {
        private CharacterMovementController _characterMovementController;
        private IMovementDirectionSource _movementDirectionSource;


        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
        }

        // Update is called once per frame
        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.Direction = direction;
        }
    }
}
