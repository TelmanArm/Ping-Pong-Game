using Game;
using Service.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Main
{
    public class MainView : BasePresenter
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button skinViewButton;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI levelText;

        private Data _data;

        public override void Show(IPresenterData data = null)
        {
            base.Show(data);
            _data = (Data) data;
            SetHighScore(_data.ScoreService.HighScore);

            startButton.onClick.AddListener(StartGame);
            skinViewButton.onClick.AddListener(ShowSkinView);
            _data.ScoreService.OnHighScoreChanged += SetHighScore;
            levelText.text = $"Level {_data.GameData.level}";
        }

        private void ShowSkinView()
        {
            _data.MainFlow.ShowSkinView();
        }


        private void SetHighScore(int score)
        {
            highScoreText.text = $"High Score: {score}";
        }

        private void StartGame()
        {
            _data.MainFlow.StartGame();
        }

        public override void Close()
        {
            base.Close();
            startButton.onClick.RemoveListener(StartGame);
            skinViewButton.onClick.RemoveListener(ShowSkinView);
            _data.ScoreService.OnHighScoreChanged -= SetHighScore;
        }

        public class Data : IPresenterData
        {
            public Data(MainFlow mainFlow, ScoreService scoreService, GameData gameData)
            {
                MainFlow = mainFlow;
                ScoreService = scoreService;
                GameData = gameData;
            }

            public MainFlow MainFlow { get; }
            public ScoreService ScoreService { get; }
            public GameData GameData { get; }
        }
    }
}