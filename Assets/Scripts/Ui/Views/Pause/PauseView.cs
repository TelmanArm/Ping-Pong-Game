using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Pause
{
    public class PauseView : BasePresenter
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button leaveButton;
        [SerializeField] private Button restartButton;
        private Data _data;


        public override void Show(IPresenterData data = null)
        {
            base.Show(data);
            _data = (Data)data;
            resumeButton.onClick.AddListener(ResumeGame);
            leaveButton.onClick.AddListener(LeaveGame);
            restartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            _data.MainFlow.RestartGame();
        }

        private void LeaveGame()
        {
            _data.MainFlow.LeaveGame();
        }

        private void ResumeGame()
        {
            _data.MainFlow.PauseGame(false);
        }

        public override void Close()
        {
            base.Close();
            resumeButton.onClick.RemoveListener(ResumeGame);
            leaveButton.onClick.RemoveListener(LeaveGame);
            restartButton.onClick.RemoveListener(RestartGame);
        }

        public class Data : IPresenterData
        {
            public Data(MainFlow mainFlow)
            {
                MainFlow = mainFlow;
            }

            public MainFlow MainFlow { get; }
        }
    }
}