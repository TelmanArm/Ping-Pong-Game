using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace Service.Skin
{
    public class SkinService
    {
        private BallSkinAssetsConfig _ballSkinAssetsConfig;
        private GameData _gameData;

        public SkinService(BallSkinAssetsConfig ballSkinAssetsConfig, GameData gameData)
        {
            _ballSkinAssetsConfig = ballSkinAssetsConfig;
            _gameData = gameData;
        }

        public List<Tuple<BallSkinData, bool>> GetSkinsData()
        {
            List<Tuple<BallSkinData, bool>> skins = new List<Tuple<BallSkinData, bool>>();
            foreach (BallSkinData skin in _ballSkinAssetsConfig.BallSkins)
            {
                bool isUnlock = skin.unlockLevel > _gameData.level;
                skins.Add(new Tuple<BallSkinData, bool>(skin, isUnlock));
            }
            return skins;
        }

        public BallSkinData GetCurrentSkinData()
        {
            return _ballSkinAssetsConfig.GetMaterialByKey(_gameData.SelectedBall);
        }
        
        public void SetSkin(string key)
        {
            _gameData.SelectedBall = key;
        }
        
    }
}