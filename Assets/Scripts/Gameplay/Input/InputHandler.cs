using System;
using UnityEngine;

namespace Gameplay.Input
{
    public class InputHandler : MonoBehaviour
    {
        public event Action<float> OnPinterHorizontalMoved;
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                OnPinterHorizontalMoved?.Invoke(UnityEngine.Input.mousePosition.x);
            }
        }
    }
}