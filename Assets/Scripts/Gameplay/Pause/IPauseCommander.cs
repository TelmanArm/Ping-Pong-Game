using System;

namespace Gameplay.Pause
{
    public interface IPauseCommander
    {
        public event Action<bool> OnPauseStateChanged;
        public void SetPauseState(bool state);
    }
}