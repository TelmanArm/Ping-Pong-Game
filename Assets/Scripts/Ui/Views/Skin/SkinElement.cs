using System;
using Service.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Skin
{
    public class SkinElement : MonoBehaviour
    {
        public event Action<string> OnSelectSkin;
        [SerializeField] private Image skinImage;
        [SerializeField] private Image selectedImage;
        [SerializeField] private Image lockedImage;
        [SerializeField] private Button selectButton;
        private bool _isLocked;

        public BallSkinData SkinData { get; private set; }

        private void OnDestroy()
        {
            selectButton.onClick.RemoveAllListeners();
        }
        
        public void SetSkin(BallSkinData skinData, bool isLocked)
        {
            _isLocked = isLocked;
            SkinData = skinData;
            skinImage.color = skinData.color;
            lockedImage.gameObject.SetActive(_isLocked);
            selectButton.onClick.AddListener(() =>
            {
                if (_isLocked)
                {
                    return;
                }

                OnSelectSkin?.Invoke(SkinData.key);
            });
        }

        public void SetSelected(bool isSelected)
        {
            selectedImage.gameObject.SetActive(isSelected);
        }
    }
}