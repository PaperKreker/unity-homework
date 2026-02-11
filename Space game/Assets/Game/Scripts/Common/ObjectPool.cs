using Game;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int _initialCapacity = 0;
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private MonoBehaviour _prefab;

        private readonly Queue<MonoBehaviour> _pool = new();

        private void Awake()
        {
            for (int i = 0; i < _initialCapacity; i++)
            {
                MonoBehaviour poolObject = Instantiate(_prefab, _container);

                poolObject.gameObject.SetActive(false);
                _pool.Enqueue(poolObject);
            }
        }

        public MonoBehaviour Spawn()
        {
            if (_pool.TryDequeue(out MonoBehaviour spawnObject))
            {
                spawnObject.gameObject.SetActive(true);
                return spawnObject;
            }

            return Instantiate(_prefab, _container);
        }

        public void Despawn(MonoBehaviour objectToDespawn)
        {
            objectToDespawn.gameObject.SetActive(false);
            _pool.Enqueue(objectToDespawn);
        }
    }
}
