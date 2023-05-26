using System;
using System.Collections.Generic;
using Service.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views.Skin
{
    public class SkinsView : BasePresenter
    {
        [SerializeField] private SkinElement skinElementPrefab;
        [SerializeField] private Transform skinViewParent;
        [SerializeField] private Button backButton;
        private Data _data;

        private List<SkinElement> _skinElements;

        public override void Show(IPresenterData data = null)
        {
            base.Show(data);
            _data = (Data)data;
            _skinElements = new List<SkinElement>();

            backButton.onClick.AddListener(ShowMainView);
            SetSkins();
        }

        private void SetSkins()
        {
            BallSkinData currentSkin = _data.SkinService.GetCurrentSkinData();
            foreach (Tuple<BallSkinData, bool> skinData in _data.SkinService.GetSkinsData())
            {
                SkinElement skinElement = Instantiate(skinElementPrefab, skinViewParent);
                skinElement.gameObject.SetActive(true);
                skinElement.SetSkin( skinData.Item1, skinData.Item2);
                skinElement.OnSelectSkin += SetSkin;
                _skinElements.Add(skinElement);
                skinElement.SetSelected(skinData.Item1.key.Equals(currentSkin.key));
            }
        }

        private void ShowMainView()
        {
            _data.MainFlow.ShowMainView();
        }

        private void SetSkin(string key)
        {
            _data.SkinService.SetSkin(key);
            foreach (SkinElement skinElement in _skinElements)
            {
                skinElement.SetSelected(skinElement.SkinData.key.Equals(key));
            }
        }

        public override void Close()
        {
            base.Close();
            backButton.onClick.RemoveListener(ShowMainView);
            foreach (SkinElement skinElement in _skinElements)
            {
                skinElement.OnSelectSkin -= SetSkin;
                Destroy(skinElement.gameObject);
            }
        }

        public class Data : IPresenterData
        {
            public Data(MainFlow mainFlow, SkinService skinService)
            {
                MainFlow = mainFlow;
                SkinService = skinService;
            }

            public MainFlow MainFlow { get; }
            public SkinService SkinService { get; }
        }
    }
}