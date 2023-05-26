namespace Gameplay.Pause
{
    public interface IPauseable
    {
        IPauseCommander PauseCommander { get; }
        bool IsGamePaused { get; }

        void SetPauseState(bool state);
        void SubscribeToPauser(IPauseCommander pauseCommander);
    }
}