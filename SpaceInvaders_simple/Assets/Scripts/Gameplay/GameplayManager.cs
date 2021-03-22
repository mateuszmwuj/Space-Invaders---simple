using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesController enemiesController;

    [SerializeField]
    private PlayerManager playerManager;
    
    [SerializeField]
    private PopupManager popupManager;
    
    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private PlayerPrefsManager playerPrefsManager;

    [SerializeField]
    private GameConfig gameConfig;

    private bool playerStateEnd = false;

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
        playerStateEnd = false;

        playerManager.Init(gameConfig.amountOfLives, gameConfig.amountOfCashedPlayersLaserShots);

        enemiesController.enemiesContainer.amountOfCashedLaserShots = gameConfig.amountOfCashedEnemiesLaserShots;
        enemiesController.Init(gameConfig.enemyShootTimerMin, gameConfig.enemyShootTimerMax);

        enemiesController.enemiesContainer.Init();
    }
    private void PlayerDeath()
    {
        popupManager.OpenEndPopup(gameConfig.failTitleText, gameConfig.failTitleTextColor, scoreManager.currentScore);
        
        enemiesController.StopAll();

        if (!playerStateEnd)
        {
            playerPrefsManager.UpdatePlayerPrefs(scoreManager.currentScore, gameConfig.amountOfScores);
            playerStateEnd = true;
        }
    }
    private void PlayerWin()
    {
        popupManager.OpenEndPopup(gameConfig.winTitleText, gameConfig.winTitleTextColor, scoreManager.currentScore);

        enemiesController.StopAll();

        if (!playerStateEnd)
        {
            playerPrefsManager.UpdatePlayerPrefs(scoreManager.currentScore, gameConfig.amountOfScores);
            playerStateEnd = true;
        }
    }
}
