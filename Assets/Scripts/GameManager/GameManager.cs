using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War.io.Enemy;
using War.io; 

namespace War.io
{

    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;


        public PlayerCharacter Player { get; private set; } 
        public List<EnemyCharacter> Enemies { get; private set; }

        public CurrentEnemiesCount EnemyCount { get; private set; }
        public TimerUI Timer { get; private set; }

        //public PointerManager pointerManager { get; private set; }

        private void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();
            EnemyCount = FindObjectOfType<CurrentEnemiesCount>();
           // pointerManager = FindObjectOfType<PointerManager>();

            EnemyCount.SetEnemies(Enemies.Count);
            Debug.Log(Enemies.Count);
            Player.Dead += OnPlayerDead;

            foreach (var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
                //pointerManager.SetPointers(enemy);
            }

            Timer.TimeEnd += PlayerLose;

            Time.timeScale = 1f;

        }


        private void OnPlayerDead(BaseCharacter sender)
        {
            Player.Dead -= OnPlayerDead;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnEnemyDead (BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
            Enemies.Remove(enemy);
            //pointerManager.RemoveEnemy(enemy);
            enemy.Dead -= OnEnemyDead;
            Debug.Log("Enemy dead");
            EnemyCount.SetEnemies(Enemies.Count);

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;

            }
        }

        private void PlayerLose()
        {
            Timer.TimeEnd -= PlayerLose;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }
    }


}

