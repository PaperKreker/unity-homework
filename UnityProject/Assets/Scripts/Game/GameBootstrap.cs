using UnityEngine;
using Zenject;

public class GameBootstrap : MonoBehaviour
{
    private GameCycle gameCycle;

    [Inject]
    private void Construct(GameCycle gameCycle)
    {
        this.gameCycle = gameCycle;
    }

    void Start()
    {
        gameCycle.StartGame();
    }
}
