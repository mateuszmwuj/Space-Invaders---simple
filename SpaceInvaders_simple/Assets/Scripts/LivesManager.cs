using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> lives = new List<Image>();
    private int amountOfLives;
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
        foreach (Image life in lives)
        {
            life.gameObject.SetActive(true);
        }

        amountOfLives = lives.Count;
    }

    private void LoseLife()
    {
        if (amountOfLives > 0)
        {
            lives[amountOfLives - 1].gameObject.SetActive(false);

            amountOfLives--;
        }

    }

    private void PlayersDeath()
    {
        foreach (Image life in lives)
        {
            life.gameObject.SetActive(false);
        }
    }
}
