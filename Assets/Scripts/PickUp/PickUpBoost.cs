using UnityEngine;
using War.io.Movement;
using War.io.Shooting;

namespace War.io.PickUp
{
    public class PickUpBoost: PickUpItem
    {
        [SerializeField]
        private float _acceleration = 2f;

        [SerializeField]
        private float _time = 10f;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            var controller = character.GetComponent<CharacterMovementController>();
            controller.Accelerate(_acceleration, _time);
        }
    }
}