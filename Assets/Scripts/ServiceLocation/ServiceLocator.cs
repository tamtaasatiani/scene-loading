using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocation
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, IService> _services = new();
        
        public T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (_services.TryGetValue(type, out var service))
                return (T)service;
            
            return default;
        }

        public bool TryRegisterService<TContract, TImplementation>(TImplementation service) where TContract : class, IService where TImplementation : class, TContract
        {
            var contractType = typeof(TContract);

            if (!_services.TryAdd(contractType, service))
                return false;
            
            var implementationType = typeof(TImplementation);
            
            if (contractType == implementationType)
                return true;

            if (!_services.TryAdd(implementationType, service))
            {
                _services.Remove(contractType);
                return false;
            }
            
            return true;
        }

        public bool TryUnregisterService<TContract, TImplementation>(TImplementation service) where TContract : class, IService where TImplementation : class, TContract
        {
            var contractType = typeof(TContract);
            
            var isRegistrationExists = _services.TryGetValue(contractType, out var existing);
            
            if (!isRegistrationExists || !ReferenceEquals(existing, service))
                return false;
            
            var isContractRemoved = _services.Remove(contractType);
            var implementationType = typeof(TImplementation);

            if (isContractRemoved && implementationType == contractType)
                return true;
            
            return false;
        }
    }
}