using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesController _enemiesController;

    [SerializeField]
    private PlayerManager _playerManager;
    
    [SerializeField]
    private PopupManager _popupManager;

    [SerializeField]
    private GameObject _fadeMask;

    [SerializeField]
    private ScoreManager _scoreManager;

    [SerializeField]
    private PlayerPrefsManager _playerPrefsManager;

    [SerializeField]
    private GameConfig _gameConfig;

    private bool _playerStateEnd = false;

    private void OnEnable()
    {
        PlayerDeathEvent.PlayerDeath += PlayerDeath;
        PlayerWinEvent.PlayerWin += PlayerWin;
    }
    private void OnDisable()
    {
        PlayerDeathEvent.PlayerDeath -= PlayerDeath;
        PlayerWinEvent.PlayerWin -= PlayerWin;
    }

    private void Start()
    {
        _playerStateEnd = false;

        _playerManager.Init(_gameConfig.amountOfLives, _gameConfig.amountOfCashedPlayersLaserShots);

        _enemiesController.enemiesContainer.amountOfCashedLaserShots = _gameConfig.amountOfCashedEnemiesLaserShots;
        _enemiesController.Init(_gameConfig.enemyShootTimerMin, _gameConfig.enemyShootTimerMax);

        _enemiesController.enemiesContainer.Init();
    }
    private void PlayerDeath()
    {
        _fadeMask.SetActive(true);

        _popupManager.OpenEndPopup(_gameConfig.failTitleText, _gameConfig.failTitleTextColor, _scoreManager.currentScore);
        
        _enemiesController.StopAll();

        if (!_playerStateEnd)
        {
            _playerPrefsManager.UpdatePlayerPrefs(_scoreManager.currentScore, _gameConfig.amountOfScores);
            _playerStateEnd = true;
        }
    }
    private void PlayerWin()
    {
        _fadeMask.SetActive(true);

        _popupManager.OpenEndPopup(_gameConfig.winTitleText, _gameConfig.winTitleTextColor, _scoreManager.currentScore);

        _enemiesController.StopAll();

        if (!_playerStateEnd)
        {
            _playerPrefsManager.UpdatePlayerPrefs(_scoreManager.currentScore, _gameConfig.amountOfScores);
            _playerStateEnd = true;
        }
    }
}
