using UnityEngine;

namespace Ui
{
    public class BasePresenter : MonoBehaviour
    {
        public virtual void Show(IPresenterData data = null)
        {
        }

        public virtual void Close()
        {
        }
    }
}