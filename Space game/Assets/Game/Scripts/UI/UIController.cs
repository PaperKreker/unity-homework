using Modules.UI;
using UnityEngine;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _enemyOrchestrator;

        [SerializeField]
        private Ship _ship;

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
            _ship.OnHealthChanged += SetHealth;
            _ship.OnDead += _gameOverView.Show;
        }
        private void OnDisable()
        {
            _enemyOrchestrator.OnEnemyDestroyed -= SetScore;
            _ship.OnHealthChanged -= SetHealth;
            _ship.OnDead -= _gameOverView.Show;
        }

        private void SetHealth(int currentHealth, int maxHealth)
        {
            _healthView.SetHealth(currentHealth, maxHealth);
        }

        private void SetScore(EnemyBehaviour _, int score)
        {
            _scoreView.SetValue(score);
        }
    }
}