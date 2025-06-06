using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War.io.Enemy;

namespace War.io
{

    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;
        public PlayerCharacter Player { get; private set; } 
        public List<EnemyCharacter> Enemies { get; private set; }

        public TimerUI Timer { get; private set; }

        private void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();

            Player.Dead += OnPlayerDead;

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer.TimeEnd += PlayerLose;
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
            enemy.Dead -= OnEnemyDead;
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

