using HitMaster.Player;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HitMaster.Game
{
    public class Finish : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MovementPlayer player))
            {
                RestartLevel();
            }
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GC.Collect();
        }
    }
}

