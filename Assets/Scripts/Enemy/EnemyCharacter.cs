using UnityEngine;

namespace War.io.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField]
        [Range(0f, 1f)] private float lowHealthThreshold = 0.3f;
        [SerializeField]
        [Range(0f, 1f)] private float runAwayProbability = 0.7f;

        public bool RunAway()
        {
                return (_health / _maxHealth) < lowHealthThreshold && UnityEngine.Random.value<runAwayProbability;

        }

}
}
