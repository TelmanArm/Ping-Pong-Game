using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Ui
{
    public class PresenterService : MonoBehaviour
    {
        [SerializeField] private string presentersPath;
        [SerializeField] private Transform parentTransform;
        private readonly LinkedList<BasePresenter> _activePresenters = new();

        public T Show<T>(IPresenterData data = null, bool closeLast = true)
            where T : BasePresenter
        {
            T alreadyOpened = TryGetPresenter<T>();
            if (alreadyOpened != null)
            {
                return alreadyOpened;
            }

            T presenter = Create<T>();

            BasePresenter lastPresenter = _activePresenters.LastOrDefault();
            if (closeLast && lastPresenter != null)
            {
                CloseLast();
            }

            presenter.Show(data);
            _activePresenters.AddLast(presenter);
            return presenter;
        }

        public T TryGetPresenter<T>() where T : BasePresenter
        {
            return _activePresenters.FirstOrDefault(p => p is T) as T;
        }

        public void Close<T>() where T : BasePresenter
        {
            BasePresenter presenter = _activePresenters.FirstOrDefault(p => p is T);
            if (presenter != null)
            {
                _activePresenters.Remove(presenter);
                presenter.Close();
                Destroy(presenter.gameObject);
            }
        }

        public void CloseLast()
        {
            BasePresenter presenter = _activePresenters.LastOrDefault();
            if (presenter != null)
            {
                _activePresenters.Remove(presenter);
                presenter.Close();
                Destroy(presenter.gameObject);
            }
        }

        private T Create<T>() where T : BasePresenter
        {
            string path = Path.Combine(presentersPath, typeof(T).Name);
            T presenterPrefabs = Resources.Load<T>(path);
            T presenter = Instantiate(presenterPrefabs, parentTransform);
            return presenter;
        }
    }
}