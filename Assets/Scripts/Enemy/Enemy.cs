using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Dead;

        [SerializeField] private EnemyRagdoll _enemyRagdoll;

        private void Start()
        {
            _enemyRagdoll.Hit += OnHit;
        }

        private void OnHit()
        {
            Dead?.Invoke(this);
        }
    }
}

