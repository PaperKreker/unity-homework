using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Object
    {
        [SerializeField]
        private int _initialCapacity = 0;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private T _prefab;

        private readonly Queue<T> _pool = new();

        private void Awake()
        {
            for (int i = 0; i < _initialCapacity; i++)
            {
                T poolObject = CreateObject();

                DisableObject(poolObject);
                _pool.Enqueue(poolObject);
            }
        }

        public T Spawn()
        {
            T spawnObject;

            if (!_pool.TryDequeue(out spawnObject))
            {
                spawnObject = CreateObject();
            }
            EnableObject(spawnObject);

            return spawnObject;
        }

        public void Despawn(T objectToDespawn)
        {
            DisableObject(objectToDespawn);
            _pool.Enqueue(objectToDespawn);
        }

        protected virtual T CreateObject() 
        {
            return Instantiate(_prefab, _container);
        }
        protected abstract void EnableObject(T objectToEnable);
        protected abstract void DisableObject(T objectToDisable);
    }
}
