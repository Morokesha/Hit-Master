using HitMaster.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster.Game
{
    public class BulletsPool : MonoBehaviour
    {
        [SerializeField] private int _capacityPool;

        private List<Bullet> _bulletsPool = new List<Bullet>();
        private void Awake()
        {
            for (int i = 0; i < _capacityPool; i++)
            {
                Bullet bullet = Instantiate(GameAssets.Instance._pfBullet);
                bullet.transform.parent = gameObject.transform;
                bullet.gameObject.SetActive(false);
                _bulletsPool.Add(bullet);
            }    
        }

        private Bullet GetBulletInPool()
        {
            for (int i = 0; i < _capacityPool; i++)
            {
                if (!_bulletsPool[i].gameObject.activeInHierarchy)
                {
                    return _bulletsPool[i];
                }
            }
            return null;
        }

        public Bullet GetBulletActve(Vector3 activePosition)
        {
            Bullet bullet = GetBulletInPool();
            bullet.gameObject.transform.position = activePosition;
            bullet.gameObject.SetActive(true);

            return bullet;
        }
    }
}

