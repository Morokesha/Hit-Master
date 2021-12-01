using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitMaster.Game
{
    public class InputSystem : MonoBehaviour
    {
        public event Action TochedScreen;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TochedScreen?.Invoke();
            }
        }

        public Vector2 GetMousePositionOnScreen()
        {
            return Input.mousePosition;
        }
    }
}

