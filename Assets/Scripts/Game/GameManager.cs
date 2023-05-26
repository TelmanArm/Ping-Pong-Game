using System;
using Gameplay;
using Service.Progress;
using Service.Save;
using Service.Score;
using Service.Skin;
using Ui;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PresenterService viewService;
        [SerializeField] private GameplayController gameplayControllerPrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private ProgressConfig progressConfig;
        [SerializeField] private BallSkinAssetsConfig ballSkinAssetsConfig;
        public GameplayController GameplayControllerPrefab => gameplayControllerPrefab;
        public Camera MainCamera => mainCamera;
        public PresenterService ViewService => viewService;
        public ScoreService ScoreService { get; private set; }
        public ProgressService ProgressService { get; private set; }

        public SkinService SkinService { get; private set; }
        public SaveService SaveService { get; private set; }
        public GameData GameData { get; set; }

        private void Start()
        {
            Application.targetFrameRate = 60;
            SaveService = new SaveService();
            GameData = SaveService.LoadData();
            if (GameData == null) GameData = GenerateDefaultData();
            ScoreService = new ScoreService(GameData);
            SkinService = new SkinService(ballSkinAssetsConfig, GameData);
            ProgressService = new ProgressService(progressConfig, GameData);

            MainFlow mainFlow = new(this);
            mainFlow.ShowMainView();
        }

        private GameData GenerateDefaultData()
        {
            GameData gameData = new GameData();
            gameData.highScore = 0;
            gameData.level = 1;
            gameData.SelectedBall = ballSkinAssetsConfig.BallSkins[0].key;
            return gameData;
        }

        private void OnApplicationQuit()
        {
            SaveService.Save(GameData);
        }
        
#if UNITY_EDITOR
    
        [MenuItem("Custom Tools/Remove Game Data")]
        private static void RemoveGameDataEditor()
        {
            SaveService service = new SaveService();
            service.DeleteFile();
        }
    
#endif
    }
}