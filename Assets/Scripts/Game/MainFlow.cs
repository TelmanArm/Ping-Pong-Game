using Game;
using Gameplay;
using Ui.Views.Game;
using Ui.Views.Main;
using Ui.Views.Pause;
using Ui.Views.Skin;
using UnityEngine;

public class MainFlow
{
    private readonly GameManager _facade;
    private GameplayController _gameplayController;

    public MainFlow(GameManager facade)
    {
        _facade = facade;
    }

    public void ShowMainView()
    {
        _facade.ViewService.Show<MainView>(new MainView.Data(this, _facade.ScoreService));
    }

    public void StartGame()
    {
        _facade.ViewService.CloseLast();
        _gameplayController = Object.Instantiate(_facade.GameplayControllerPrefab);
        _gameplayController.Initialize(_facade.MainCamera, _facade.ScoreService,
            _facade.SkinService.GetCurrentSkinData());
        _facade.ViewService.Show<GameplayView>(new GameplayView.Data(this, _facade.ScoreService, _facade.ProgressService, _facade.GameData));
    }

    public void PauseGame(bool pauseState)
    {
        if (pauseState)
        {
            _facade.ViewService.Show<PauseView>(new PauseView.Data(this));
        }
        else
        {
            _facade.ViewService.Show<GameplayView>(new GameplayView.Data(this, _facade.ScoreService, _facade.ProgressService,_facade.GameData));
        }

        _gameplayController.SetPauseState(pauseState);
    }

    public void RestartGame()
    {
        _facade.ScoreService.ResetScore();
        Object.Destroy(_gameplayController.gameObject);
        StartGame();
    }

    public void LeaveGame()
    {
        _facade.ScoreService.ResetScore();
        Object.Destroy(_gameplayController.gameObject);
        _facade.ViewService.CloseLast();
        _facade.ViewService.Show<MainView>(new MainView.Data(this, _facade.ScoreService));
    }

    public void ShowSkinView()
    {
        _facade.ViewService.Show<SkinsView>(new SkinsView.Data(this, _facade.SkinService));
    }
}