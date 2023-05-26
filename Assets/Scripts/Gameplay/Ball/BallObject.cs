using Gameplay.Pause;
using Service.Skin;
using UnityEngine;

namespace Gameplay.Ball
{
    public class BallObject : MonoBehaviour, IPauseable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float directionDeltaAngel = 30f;

        private Vector3 _velocity;
        public IPauseCommander PauseCommander { get; private set; }
        public bool IsGamePaused { get; private set; }
        
        public void Initialize(BallSkinData skinData)
        {
            meshRenderer.material.color = skinData.color;
        }
        private void Awake()
        {
            int random = Random.Range(0, 2);

            Vector3 forwardUnitVector = random == 0 ? Vector3.forward : Vector3.back;
            float thetaMin = -directionDeltaAngel * Mathf.Deg2Rad;
            float thetaMax = directionDeltaAngel * Mathf.Deg2Rad;

            float cosMin = Mathf.Cos(thetaMin);
            float sinMin = Mathf.Sin(thetaMin);

            Vector3 u = Vector3.up;
            Vector3 v = Vector3.Cross(forwardUnitVector, u);

            float t = Random.Range(thetaMin, thetaMax);
            Vector3 velocity =
                speed * (cosMin * forwardUnitVector + sinMin * Mathf.Cos(t) * u + sinMin * Mathf.Sin(t) * v);
            rigidbody.velocity = velocity;
        }
        public void SetPauseState(bool state)
        {
            if (state)
            {
                _velocity = rigidbody.velocity;
                rigidbody.velocity = Vector3.zero;
            }
            else
            {
                rigidbody.velocity = _velocity;
            }
        }

        public void SubscribeToPauser(IPauseCommander pauseCommander)
        {
            PauseCommander = pauseCommander;
            PauseCommander.OnPauseStateChanged += SetPauseState;
        }
        
        private void OnDestroy()
        {
            PauseCommander.OnPauseStateChanged -= SetPauseState;
        }

    }
}