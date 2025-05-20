using UnityEngine;
using War.io.Shooting;

namespace War.io.PickUp
{
    public class PickUpWeapon : MonoBehaviour
    {
        [field: SerializeField]
        public Weapon WeaponPrefab { get; private set; }
    }
}