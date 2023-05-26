using System;
using Gameplay.Ball;
using Gameplay.Input;
using Gameplay.Pause;
using UnityEngine;

namespace Gameplay.Paddle
{
    public class PaddlesController : MonoBehaviour
    {
        public event Action OnPaddleHit;

        [SerializeField] private AIPaddleObject aiTopPaddle;
        [SerializeField] private PaddleObject bottomPaddle;

        [SerializeField] private Transform xMaxPoint;
        [SerializeField] private Transform xMinPoint;
        private BallSpawner _ballSpawner;
        
        public void Initialize(InputHandler inputHandler, BallSpawner ballSpawner, Camera mainCamera,
            IPauseCommander pauseCommander)
        {
            _ballSpawner = ballSpawner;
            aiTopPaddle.Initialize(xMaxPoint.position.x, xMinPoint.position.x);
            bottomPaddle.Initialize(xMaxPoint.position.x, xMinPoint.position.x);
            bottomPaddle.Initialize(inputHandler, mainCamera);

            aiTopPaddle.SubscribeToPauser(pauseCommander);
            bottomPaddle.SubscribeToPauser(pauseCommander);
            aiTopPaddle.OnBallCollision += BallCollided;
            bottomPaddle.OnBallCollision += BallCollided;
            _ballSpawner.OnBallSpawned += SetBall;
        }

        private void SetBall(BallObject ball)
        {
            aiTopPaddle.SetBall(ball.transform);
        }

        private void BallCollided()
        {
            OnPaddleHit?.Invoke();
        }
        
        private void OnDestroy()
        {
            aiTopPaddle.OnBallCollision -= BallCollided;
            bottomPaddle.OnBallCollision -= BallCollided;
            _ballSpawner.OnBallSpawned += SetBall;
        }
    }
}