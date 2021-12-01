using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitMaster.Enemies;

namespace HitMaster.Game
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets instance;
        public static GameAssets Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GameAssets>("GameAssets");
                }
                return instance;
            }
        }

        public Enemy _pfEnemy;
        public Bullet _pfBullet;
    }
}
