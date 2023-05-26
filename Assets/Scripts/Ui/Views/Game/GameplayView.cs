using Game;
using Service.Progress;
using Service.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Game
{
    public class GameplayView : BasePresenter
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private Image progressBarImage;
        private Data _data;
        private int _levelGolScore;
        private bool _isCompleted;

        public override void Show(IPresenterData data = null)
        {
            base.Show(data);
            _data = (Data) data;
            pauseButton.onClick.AddListener(PauseGame);
            SetScore(_data.ScoreService.Score);
            SetHighScore(_data.ScoreService.HighScore);
            progressBarImage.fillAmount = 0;
           
            currentLevelText.text = $"L{_data.GameData.level}";
            _isCompleted = false;
            _data.ScoreService.OnScoreChanged += SetScore;
            _data.ScoreService.OnHighScoreChanged += SetHighScore;
        }

        private void SetScore(int score)
        {
            _levelGolScore = _data.ProgressService.GetCurrentLevelGol();
            if (score == 0) return;
            if (score >= _levelGolScore & !_isCompleted)
            {
                _data.ProgressService.GolCompleted();
                _isCompleted = true;
            }

            progressBarImage.fillAmount = (float) score / _levelGolScore;

            scoreText.text = $"Score: {score}";
        }

        private void SetHighScore(int score)
        {
            highScoreText.text = $"High Score: {score}";
        }

        private void PauseGame()
        {
            _data.MainFlow.PauseGame(true);
        }

        public override void Close()
        {
            base.Close();
            pauseButton.onClick.RemoveListener(PauseGame);
            _data.ScoreService.OnScoreChanged -= SetScore;
            _data.ScoreService.OnHighScoreChanged -= SetHighScore;
        }

        public class Data : IPresenterData
        {
            public Data(MainFlow mainFlow, ScoreService scoreService, ProgressService progressService,
                GameData gameData)
            {
                MainFlow = mainFlow;
                ScoreService = scoreService;
                ProgressService = progressService;
                GameData = gameData;
            }

            public MainFlow MainFlow { get; }
            public ScoreService ScoreService { get; }
            public ProgressService ProgressService { get; }
            public GameData GameData { get; }
        }
    }
}