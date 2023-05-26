using System;
using UnityEngine;

namespace Gameplay.Zone
{
    public class OutZone : MonoBehaviour
    {
        public event Action<GameObject> OnCollision;
        private void OnCollisionEnter(Collision collision)
        {
            OnCollision?.Invoke(collision.gameObject);
        }
    }
}