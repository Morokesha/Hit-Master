using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster.Enemies
{
    public class EnemyRagdoll: MonoBehaviour
    {
        public event System.Action Hit;

        [SerializeField] private Animator _animator;

        private Rigidbody[] _allRigidbodys;
        private bool isHit = false;

        private void Awake() => 
            Initialize();

        private void MakePhysical()
        {
            for (int i = 0; i < _allRigidbodys.Length; i++)
            {
                _animator.enabled = false;
                _allRigidbodys[i].isKinematic = false;
            }
        }

        private void Initialize()
        {
            _allRigidbodys = GetComponentsInChildren<Rigidbody>();

            foreach (var rb in _allRigidbodys)
            {
                rb.isKinematic = true;

                if (gameObject.name != rb.gameObject.name)
                {
                    EnemyCollision enemyCollision = rb.gameObject.AddComponent<EnemyCollision>();
                    enemyCollision.Hit += OnHit;
                }
            }
        }

        private void OnHit()
        {

            if (isHit == false)
            {
                isHit = true;
                MakePhysical();

                Hit?.Invoke();
            }

        }
    }
}