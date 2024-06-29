using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField] GameObject _spaceship;

    public enum GameState
    {
        Playing,
        Pause,
        GameOver,
        GameClear,
        Result
    }
    [SerializeField] public GameState _gameState;

    HealthSystem _healthSystem;
    HyperspaceCtrl _hyperspaceCtrl;
    GameEnd _gameEnd;

    private void Start()
    {
        _gameState = GameState.Playing;

        _healthSystem = _spaceship.GetComponent<HealthSystem>();
        _hyperspaceCtrl = _spaceship.GetComponent<HyperspaceCtrl>();
        _gameEnd = this.GetComponent<GameEnd>();
    }
    void Update()
    {
        // Playing
        if (_gameState == GameState.Playing)
        {
            // タイマーが0
            if (_hyperspaceCtrl._currentTimer <= 0)
            {
                _gameState = GameState.GameClear;
                StartCoroutine(_gameEnd.Core(GameState.GameClear));
            }
            // ヘルスが0
            if (_healthSystem._health <= 0)
            {
                _gameState = GameState.GameOver;
                StartCoroutine(_gameEnd.Core(GameState.GameOver));
            }
        }
    }
}
