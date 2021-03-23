using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerShoot _playerShoot;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _boxCollider2D;

    [SerializeField]
    private GameObject _explosionObject;

    private int _amountOfLives;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsData.enemyLaserShot) || collision.CompareTag(TagsData.enemy))
        {
            LoseHeath();

            if (collision.CompareTag(TagsData.enemyLaserShot))
                ScoreEvents.PlayerHitByEnemiesLaser(collision.gameObject.GetComponent<LaserShot>().indexOfShooter);
        }
    }

    private void OnEnable()
    {
        EnemyCollisionTriggerEvents.PlayerContact += LoseHeath;
        EnemyCollisionTriggerEvents.DownWallEnter += PlayerDies;
        PlayerWinEvent.PlayerWin += StopAllActions;
    }
    private void OnDisable()
    {
        EnemyCollisionTriggerEvents.PlayerContact -= LoseHeath;
        EnemyCollisionTriggerEvents.DownWallEnter -= PlayerDies;
        PlayerWinEvent.PlayerWin -= StopAllActions;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init(int amountOfLives = 3, int amountOfCachedLaserShots = 10)
    {
        this._amountOfLives = amountOfLives;

        _playerShoot.amountOfCachedLaserShots = amountOfCachedLaserShots;
        _playerShoot.InstantiateLaserShots(amountOfCachedLaserShots);

        _explosionObject.SetActive(false);
    }

    private void LoseHeath()
    {
        if (_amountOfLives > 1)
        {
            _amountOfLives--;

            PlayerLoseLifeEvent.PlayerLoseLife();
        }
        else
        {
            PlayerLoseLifeEvent.PlayerLoseLife();

            PlayerDies();
        }
    }

    public void EnablePlayerComponents(bool enable)
    {
        _playerMovement.enabled = enable;
        _playerShoot.enabled = enable;
        _spriteRenderer.enabled = enable;
        _boxCollider2D.enabled = enable;
    }
    private void PlayerDies()
    {
        _explosionObject.SetActive(false);

        EnableShip(false);

        Invoke("StopAllActions", 0.5f);

        PlayerDeathEvent.PlayerDeath();
    }

    public void StopAllActions()
    {
        _playerMovement.SetMovementActive(false);
        _playerShoot.SetActiveShoot(false);

        EnablePlayerComponents(false);
    }
    private void EnableShip(bool enable)
    {
        _spriteRenderer.enabled = enable;
        _boxCollider2D.enabled = enable;
    }
}
