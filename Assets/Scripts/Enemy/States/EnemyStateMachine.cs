using System.Collections.Generic;
using War.io.FSM;

namespace War.io.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, NavMesher navMesher, EnemyTarget target, EnemyCharacter enemy)
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var runAwayState = new RunAwayState(target, enemyDirectionController, enemy);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition> //1:21
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                new Transition(
                    runAwayState,
                    () => enemy.RunAway)
            }
            );

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
            }
            );

            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    runAwayState,
                    () => enemy.RunAway)
            }
            );

            AddState(state: runAwayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
            }
);

        }
    }
}
