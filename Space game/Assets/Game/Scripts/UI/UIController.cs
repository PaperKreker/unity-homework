using Modules.UI;
using UnityEngine;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private EnemyOrchestrator _enemyOrchestrator;

        [SerializeField]
        private ShipHealth _shipHealth;

        [Header("UI views")]
        [SerializeField] 
        private GameOverView _gameOverView;

        [SerializeField] 
        private HealthView _healthView;

        [SerializeField] 
        private ScoreView _scoreView;

        private void Awake()
        {
            _scoreView.SetValue(0);
        }

        private void OnEnable()
        {
            _enemyOrchestrator.OnEnemyDestroyed += SetScore;
            _shipHealth.OnHealthChanged += SetHealth;
            _shipHealth.OnDead += _gameOverView.Show;
        }
        private void OnDisable()
        {
            _enemyOrchestrator.OnEnemyDestroyed -= SetScore;
            _shipHealth.OnHealthChanged -= SetHealth;
            _shipHealth.OnDead -= _gameOverView.Show;
        }

        private void SetHealth(int health)
        {
            _healthView.SetHealth(health, _shipHealth.Config.Health);
        }

        private void SetScore(EnemyShip _, int score)
        {
            _scoreView.SetValue(score);
        }
    }
}