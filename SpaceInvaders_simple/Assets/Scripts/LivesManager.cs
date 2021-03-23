using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> _lives;

    private int _amountOfLives;

    private void OnEnable()
    {
        PlayerLoseLifeEvent.PlayerLoseLife += LoseLife;
        PlayerDeathEvent.PlayerDeath += PlayersDeath;
    }

    private void OnDisable()
    {
        PlayerLoseLifeEvent.PlayerLoseLife -= LoseLife;        
        PlayerDeathEvent.PlayerDeath -= PlayersDeath;
    }

    void Start()
    {
        ResetLives();
    }

    public void ResetLives()
    {
        foreach (Image life in _lives)
        {
            life.gameObject.SetActive(true);
        }

        _amountOfLives = _lives.Count;
    }

    private void LoseLife()
    {
        if (_amountOfLives > 0)
        {
            _lives[_amountOfLives - 1].gameObject.SetActive(false);

            _amountOfLives--;
        }
    }

    private void PlayersDeath()
    {
        foreach (Image life in _lives)
        {
            life.gameObject.SetActive(false);
        }
    }
}
