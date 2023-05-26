using System;
using Gameplay.Pause;
using Service.Skin;
using UnityEngine;

namespace Gameplay.Ball
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private BallObject ballObjectPrefab;
        [SerializeField] private Transform ballSpawnPoint;

        private BallObject _ball;
        private BallSkinData _skinData;
        public event Action<BallObject> OnBallSpawned;
        public void Initialize(BallSkinData skinData)
        {
            _skinData = skinData;
        }

        public void SpawnNewBall(IPauseCommander pauseCommander)
        {
            if (_ball != null)
            {
                Destroy(_ball.gameObject);
            }

            _ball = Instantiate(ballObjectPrefab, transform);
            _ball.transform.position = ballSpawnPoint.position;
            _ball.Initialize(_skinData);
            _ball.SubscribeToPauser(pauseCommander);
            OnBallSpawned?.Invoke(_ball);
        }
    }
}