using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ServiceLocation
{
    public interface IService
    {
        UniTask InitializeAsync();
    }
}
