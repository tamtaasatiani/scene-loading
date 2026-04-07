using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QuestSystem;
using QuickEye.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.Util;

namespace ServiceLocation
{
    public class ServiceInstaller : MonoBehaviour
    {
        //[SerializedTypeRestriction(type = typeof(Service)), SerializeField] 
        //private UnityDictionary<SerializedType, Service> services;

        //[SerializedTypeRestriction(type = typeof(Service)), SerializeField]
        //private SerializedType t;
        
        //[SerializeField] private List<SerializedType> serviceTypes = new List<SerializedType>();
        //[SerializeField] private List<Service> services;
        
        [SerializeField] private GameManager gameManager;
        [Space]
        [SerializeField] private SceneLoader sceneLoader;
        [Space]
        [SerializeField] private ObjectiveManager objectiveManager;
        [SerializeField] private QuestManager questManager;
        
        public async UniTask InitializeAsync()
        {
            IServiceLocator.Default.TryRegisterService(gameManager);
            await gameManager.InitializeAsync();
            
            IServiceLocator.Default.TryRegisterService(sceneLoader);
            await sceneLoader.InitializeAsync();
            
            IServiceLocator.Default.TryRegisterService(objectiveManager);
            await objectiveManager.InitializeAsync();
            IServiceLocator.Default.TryRegisterService(questManager);
            await questManager.InitializeAsync();
            
            
            //foreach (var entry in services)
            //{
                /*
                var typeName = Type.GetType(entry.Key);
                if (typeName == null)
                {
                    Debug.LogError($"Cannot find type {entry.Key}");
                    return;
                }
                var castedValue = Convert.ChangeType(entry.Value, typeName);
                if (castedValue is not IService)
                {
                    Debug.LogError($"Cannot cast {entry.Key} to {typeName}");
                    return;
                }
                //IServiceLocator.Default.TryRegisterService(castedValue);
                */
            //}
        }

        private void OnDisable()
        {
            IServiceLocator.Default.TryUnregisterService(gameManager);
            
            IServiceLocator.Default.TryUnregisterService(sceneLoader);
            
            IServiceLocator.Default.TryUnregisterService(objectiveManager);
            IServiceLocator.Default.TryUnregisterService(questManager);
        }
    }
}
