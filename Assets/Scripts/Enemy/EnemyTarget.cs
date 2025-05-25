using UnityEngine;
using War.io.Shooting;

namespace War.io.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacter _player;

        private readonly Collider[] _colliders = new Collider[20];

        private readonly Collider[] _collidersWeapon = new Collider[10];

        private readonly Collider[] _collidersEnemies = new Collider[10];

        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.CharactersMask, _colliders);
            var weaponCount = FindAllTargets(LayerUtils.PickUpsMask, _collidersWeapon);
            var playerCount = FindAllTargets(LayerUtils.CharactersMask, _collidersEnemies);
            bool DefaultWeapon = _agentTransform.gameObject.GetComponent<ShootingController>().GetWeaponType() == "Pistol(Clone)";
            if (DefaultWeapon && weaponCount > 0)
            {
                for (int i = 0; i < weaponCount; i++)
                {
                    var go = _collidersWeapon[i].gameObject;
                    var distance = DistanceFromAgentTo(go);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
                return;
            }

            if (!DefaultWeapon && playerCount > 1)
            {
                for (int i = 0; i < playerCount; i++)
                {

                    var go = _collidersEnemies[i].gameObject;
                    if (go == _agentTransform.gameObject) continue;


                    var distance = DistanceFromAgentTo(go);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
                return;
            }

            for (int i=0; i<count; i++)
            {

                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;


                var distance = DistanceFromAgentTo(go);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    Closest = go;
                }
            }

            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
            {
                Closest = _player.gameObject;
            }
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
            {
                return DistanceFromAgentTo(Closest);
            }
            return 0;
        }
        private int FindAllTargets(int layerMask, Collider[] _colliders)
        {
            var size = Physics.OverlapSphereNonAlloc(_agentTransform.position, _viewRadius, _colliders, layerMask);
            return size;
        }
        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;


    }
}
