using System;
using UnityEngine;

namespace Game
{
    public class ShipHealth : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDamage;
        public event Action OnDead;

        [field: SerializeField]
        public ShipControllerSO Config { get; private set; }
        public int CurrentHealth { get; private set; }


        private void Awake()
        {
            ResetHealth();
        }

        public void Hit(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, Config.Health);

            OnHealthChanged?.Invoke(CurrentHealth);
            if (CurrentHealth > 0)
            {
                OnDamage?.Invoke();
            }
            else
            {
                Dead();
            }
        }
        public void ResetHealth()
        {
            CurrentHealth = Config.Health;
        }

        protected virtual void Dead()
        {
            OnDead?.Invoke();
            gameObject.SetActive(false);
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }
    }
}
