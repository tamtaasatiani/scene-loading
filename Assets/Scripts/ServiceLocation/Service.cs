using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ServiceLocation
{
    public class Service : MonoBehaviour, IService
    {
        protected bool _initialized = false;
        
        public virtual UniTask InitializeAsync()
        {
            DontDestroyOnLoad(this);
            return UniTask.CompletedTask;
        }
    }
}
