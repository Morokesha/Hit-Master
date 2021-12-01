using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster.Enemies
{
    public class EnemyCollision : MonoBehaviour
    {
        public event System.Action Hit;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet))
            {
                Hit?.Invoke();
                bullet.gameObject.SetActive(false);
            }
        }
    }
}

