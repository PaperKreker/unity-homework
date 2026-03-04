using Game;
using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    [SerializeField]
    private PositionIterator _spawnPositions;

    [SerializeField]
    private PositionIterator _attackPositions;

    private void Start()
    {
        _spawnPositions.Shuffle();
        _attackPositions.Shuffle();
    }

    public void GetPositions(out Vector2 spawnPosition, out Vector2 attackPosition)
    {
        spawnPosition = _spawnPositions.Next();
        attackPosition = _attackPositions.Next();
    }
}
