using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("Players Parameters")]
    public int amountOfLives = 3;
    public int amountOfCashedPlayersLaserShots = 10;
    
    [Header("EnemyShootShip Parameters")]
    public int amountOfCashedEnemiesLaserShots = 5;
    public float enemyShootTimerMin = 4f;
    public float enemyShootTimerMax = 7f;

    [Header("Scoreboard Paremeters")]
    public int amountOfScores = 10;

    [Header("Win Popup Parameters")]
    public string winTitleText = "Victory!!!";
    [SerializeField]
    public Color winTitleTextColor;

    [Header("Lose Popup Parameters")]
    public string failTitleText = "Game Over";
    [SerializeField]
    public Color failTitleTextColor;

    private void OnValidate()
    {        
    }
}
