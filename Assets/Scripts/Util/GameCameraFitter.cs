using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(Camera))]
    public class GameCameraFitter : MonoBehaviour
    {
        [SerializeField] private Vector2 screenSizes;
        [SerializeField] private float minSize;
        [SerializeField] private Camera gameCamera;

        private void Awake()
        {
            float screenRatio = (float)Screen.width / Screen.height;
            float targetRatio = screenSizes.x / screenSizes.y;
            float size = gameCamera.orthographicSize;

            size = targetRatio * size / screenRatio;

            if (size < minSize)
            {
                size = minSize;
            }


            gameCamera.orthographicSize = size;
        }
    }
}