using System;
using Gameplay.Ball;
using Gameplay.Pause;
using UnityEngine;

namespace Gameplay.Paddle
{
    public class BasePaddleObject : MonoBehaviour, IPauseable
    {
        [SerializeField] protected MeshRenderer mesh;
        private float _maxXPosition;
        private float _minXPosition;
        
        public event Action OnBallCollision;
        public IPauseCommander PauseCommander { get; private set; }
        public bool IsGamePaused { get; private set; }

        public void SetPauseState(bool state)
        {
            IsGamePaused = state;
        }

        public void SubscribeToPauser(IPauseCommander pauseCommander)
        {
            PauseCommander = pauseCommander;
            PauseCommander.OnPauseStateChanged += SetPauseState;
        }
        
        public virtual void Initialize(float maxXPosition, float minXPosition)
        {
            _minXPosition = minXPosition + mesh.bounds.size.x / 2;
            _maxXPosition = maxXPosition - mesh.bounds.size.x / 2;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out BallObject ball))
            {
                OnBallCollision?.Invoke();
            }
        }

        protected virtual void MovePaddle(float xPos)
        {
            if (IsGamePaused)
            {
                return;
            }
            float xPosition = Mathf.Clamp(xPos, _minXPosition, _maxXPosition);
            transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        }
        protected virtual void OnDestroy()
        {
            PauseCommander.OnPauseStateChanged -= SetPauseState;
        }
    }
}