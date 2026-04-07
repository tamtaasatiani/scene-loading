using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ReSharper disable HeapView.CanAvoidClosure

namespace QuestSystem
{
    public class ObjectiveManager : Observer<ObjectiveManager, Objective>
    {
        private List<Objective> _activeObjectives = new List<Objective>();

        public override UniTask InitializeAsync()
        {
            var lib = library as ObjectiveLibrary;
            if (lib == null)
            {
                Debug.LogError("Provided library is not objective library");
                return UniTask.CompletedTask;
            }
            
            lib.SubscribeToObjectiveStarted(AddToActiveObjectives);
            
            base.InitializeAsync();
            _initialized = true;
            return UniTask.CompletedTask;
        }

        private void OnDisable()
        {
            var lib = library as ObjectiveLibrary;
            if (lib == null)
            {
                Debug.LogError("Provided library is not objective library");
                return;
            }
            
            lib.UnsubscribeFromObjectiveStarted(AddToActiveObjectives);
        }

        private void AddToActiveObjectives(ScriptableObject objective)
        {
            var obj = objective as Objective;
            if (obj == null)
            {
                Debug.LogError($"Provided scriptableobject is not an objective, in {this}");
                return;
            }
            
            _activeObjectives.Add(obj);
        }
        
        public Objective FindActiveByName(string objName)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.Name == objName);
            return result;
        }

        private Objective FindActiveByHash(int hashCode)
        {
            var result = _activeObjectives.FirstOrDefault(objective => objective.GetHashCode() == hashCode);
            return result;
        }
        
        //public Objective FindByName(string objName)
        //{
        //    var result = library.FindByName(objName);
        //    return result;
        //}
        
        public override async UniTask BroadcastAsync(int hashCode, Action callback = null)
        {
            if (library == null)
            {
                Debug.LogError($"No objective library provided: {library}");
                return;
            }
            
            if (!_initialized)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                await UniTask.WaitUntil(() => _initialized);
            }
            
            var objective = library.FindByHash(hashCode);
            if (objective == null)
            {
                Debug.LogWarning("No such active objective");
                return;
            }
            
            objective.CustomUpdate(objective);
        }
    }
}
