using System;
using Gameplay.Ball;
using Gameplay.Input;
using Gameplay.Paddle;
using Gameplay.Pause;
using Gameplay.Zone;
using Service.Score;
using Service.Skin;
using UnityEngine;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour, IPauseCommander
    {
        public event Action<bool> OnPauseStateChanged;

        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private PaddlesController paddlesController;
        [SerializeField] private BallSpawner ballSpawner;
        [SerializeField] private OutZone topOutZone;
        [SerializeField] private OutZone bottomOutZone;
        [SerializeField] private int addScoreValue;

        private ScoreService _scoreService;
        private Camera _mainCamera;
        
        public void SetPauseState(bool state)
        {
            OnPauseStateChanged?.Invoke(state);
        }

        public void Initialize(Camera mainCamera, ScoreService scoreService, BallSkinData skinData)
        {
            _scoreService = scoreService;
            _mainCamera = mainCamera;
            paddlesController.Initialize(inputHandler, ballSpawner, _mainCamera, this);
            ballSpawner.Initialize(skinData);
            ballSpawner.SpawnNewBall(this);

            topOutZone.OnCollision += OutZoneEntered;
            bottomOutZone.OnCollision += OutZoneEntered;
            paddlesController.OnPaddleHit += PaddleHit;
        }

        private void PaddleHit()
        {
            _scoreService.AddScore(addScoreValue);
        }

        private void OutZoneEntered(GameObject zoneEnterObject)
        {
            BallObject ballObject = zoneEnterObject.GetComponent<BallObject>();
            if (ballObject == null)
            {
                return;
            }

            _scoreService.ResetScore();
            ballSpawner.SpawnNewBall(this);
        }
        
        private void OnDestroy()
        {
            topOutZone.OnCollision -= OutZoneEntered;
            bottomOutZone.OnCollision -= OutZoneEntered;
        }
    }
}