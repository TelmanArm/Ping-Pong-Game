using UnityEngine;

namespace Gameplay.Paddle
{
    public class AIPaddleObject : BasePaddleObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float detectDistance;
        private Transform _ballTransform;

        private void Update()
        {
            if (_ballTransform == null)
            {
                return;
            }

            float distance = Mathf.Abs(_ballTransform.position.z - transform.position.z);
            if (distance > detectDistance)
            {
                return;
            }

            float direction = _ballTransform.position.x - transform.position.x > 0 ? 1 : -1;
            float deltaTime = direction * moveSpeed * Time.deltaTime *
                              Mathf.Abs(_ballTransform.position.x - transform.position.x);
            MovePaddle(deltaTime + transform.position.x);
        }

        public void SetBall(Transform ball)
        {
            _ballTransform = ball;
        }
    }
}