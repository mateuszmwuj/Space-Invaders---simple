using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;
    public int currentScore { get { return _currentScore; } }

    private void OnEnable()
    {
        ScoreEvents.ScoreEnemyAmount += DecreaseScoreByColumnEnemies;
        EnemyDiesEvents.EnemyDies += AddScore;
    }
    private void OnDisable()
    {
        ScoreEvents.ScoreEnemyAmount -= DecreaseScoreByColumnEnemies;
        EnemyDiesEvents.EnemyDies -= AddScore;
    }

    private void DecreaseScoreByColumnEnemies(int enemies)
    {
        _currentScore -= enemies * 2;
        _currentScore = _currentScore <= 0 ? 0 : _currentScore;

        ScoreEvents.ScoreSync(_currentScore);
    }

    private void AddScore()
    {
        _currentScore++;

        ScoreEvents.ScoreSync(_currentScore);
    }
    public void ResetScore()
    {
        _currentScore = 0;
    }
}
