using System;
using UnityEngine;

namespace Service.Skin
{
    [CreateAssetMenu(fileName = "Ball Skins", menuName = "Config/Ball Skins", order = 0)]
    public class BallSkinAssetsConfig : ScriptableObject
    {
        [SerializeField] private BallSkinData[] ballSkins;

        public BallSkinData[] BallSkins => ballSkins;

        public BallSkinData GetMaterialByKey(string key)
        {
            foreach (BallSkinData ballSkin in ballSkins)
            {
                if (ballSkin.key == key)
                {
                    return ballSkin;
                }
            }

            throw new Exception($"No config with key: {key} found");
        }
    }

    [Serializable]
    public class BallSkinData
    {
        public string key;
        public int unlockLevel;
        public Color color;
    }
}