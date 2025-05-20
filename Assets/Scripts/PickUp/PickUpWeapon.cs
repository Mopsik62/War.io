using UnityEngine;
using War.io.Shooting;

namespace War.io.PickUp
{
    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        private Weapon _weaponPrefab;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}