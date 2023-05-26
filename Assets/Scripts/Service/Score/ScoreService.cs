using System;
using Game;
using UnityEngine;

namespace Service.Score
{
    public class ScoreService
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnHighScoreChanged;
        private int _highScore;
        public int Score { get; private set; }
        public int HighScore => _gameData.highScore;

        private GameData _gameData;

        public ScoreService(GameData gameData)
        {
            _gameData = gameData;
        }

        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke(Score);

            if (Score > _gameData.highScore)
            {
                _gameData.highScore = Score;
                OnHighScoreChanged?.Invoke(Score);
            }
        }

        public void ResetScore()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }
    }
}