using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using QuestSystem;
using UnityEngine;
// ReSharper disable HeapView.CanAvoidClosure

public class Observer<TManager, TObserved> : SingletonMonoBehaviour<TManager> where TManager : MonoBehaviour where TObserved : ScriptableObject, IUpdateable<TObserved>
{
    protected bool _initialized = false;
    protected CancellationTokenSource _cancellationTokenSource;
    
    [SerializeField] protected Library<TObserved> library;

    public override void Awake()
    {
        base.Awake();
        
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            destroyCancellationToken,
            Application.exitCancellationToken
        );
    }
    
    public virtual async UniTask AddListenerAsync(int hashCode, Action<TObserved> callback)
    {
        if (library == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in empty library");
            return;
        }

        if (!_initialized)
        {
            _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            await UniTask.WaitUntil(() => _initialized);
        }
        
        var item = library.FindByHash(hashCode);
        
        if (item == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in library");
            return;
        }
        
        item.OnUpdated += callback;
    }

    public virtual async UniTask RemoveListenerAsync(int hashCode, Action<TObserved> callback)
    {
        if (library == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in empty library");
            return;
        }

        if (!_initialized)
        {
            _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            await UniTask.WaitUntil(() => _initialized);
        }
        
        var item = library.FindByHash(hashCode);
        
        if (item == null)
        {
            Debug.LogError($"Observer {typeof(TManager)} cannot find element in library");
            return;
        }
        
        item.OnUpdated -= callback;
    }

    public virtual async UniTask BroadcastAsync(int hashCode, Action callback = null)
    {
        if (!_initialized)
        {
            _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            await UniTask.WaitUntil(() => _initialized);
        }

        callback?.Invoke();
    }

    public void RemoveAllListeners()
    {
        foreach (var item in library.GetAll())
        {
            item.RemoveAllListeners();
        }
    }

    private void OnDisable()
    {
        RemoveAllListeners();
    }
}
