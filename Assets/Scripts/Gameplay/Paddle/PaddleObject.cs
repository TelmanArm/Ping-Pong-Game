using Gameplay.Input;
using UnityEngine;

namespace Gameplay.Paddle
{
    public class PaddleObject : BasePaddleObject
    {
        private InputHandler _inputHandler;
        private Camera _mainCamera;

        public void Initialize(InputHandler inputHandler, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _inputHandler = inputHandler;
            _inputHandler.OnPinterHorizontalMoved += MovePaddle;
        }

        protected override void OnDestroy()
        {
            _inputHandler.OnPinterHorizontalMoved -= MovePaddle;
            base.OnDestroy();
        }
        
        protected override void MovePaddle(float pos)
        {
            base.MovePaddle(_mainCamera.ScreenToWorldPoint(new Vector3(pos, 0)).x);
        }
    }
}