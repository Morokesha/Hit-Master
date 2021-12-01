using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HitMaster.Enemies;

namespace HitMaster.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;

        private List<Enemy> _enemiesList = new List<Enemy>();
        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < _spawnPositions.Length; i++)
            {
                Enemy enemy = Instantiate(GameAssets.Instance._pfEnemy, _spawnPositions[i].position, _spawnPositions[i].rotation);
                enemy.Dead += RemoveEnemy;
                _enemiesList.Add(enemy);
            }
        }

        private void RemoveEnemy(Enemy enemy)
        {
            _enemiesList.Remove(enemy);
            enemy.Dead -= RemoveEnemy;
        }

        public Enemy GetLastEnemy()
        {
            return _enemiesList.Last();
        }

        public int GetCountEnemy()
        {
            return _enemiesList.Count;
        }
    }
}

