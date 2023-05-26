using Game;
using Service.Skin;
using UnityEngine;

namespace Service.Progress
{
    public class ProgressService
    {
        private readonly ProgressConfig _progressConfig;
        private GameData _gameData;
        public ProgressService(ProgressConfig progressConfig, GameData gameData)
        {
            _progressConfig = progressConfig;
            _gameData = gameData;
        }
        
        public void GolCompleted()
        {
            _gameData.level++;
        }

        public int GetCurrentLevelGol()
        {
            int gol = _progressConfig.FirstLevelProgress + _progressConfig.AddProgressEachLevel * _gameData.level;
            return gol;
        }
        
    }
}