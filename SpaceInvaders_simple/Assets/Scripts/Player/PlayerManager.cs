using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    protected GameObject explosionObject;

    private int amountOfLives;

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
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerShoot = gameObject.GetComponent<PlayerShoot>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

        Init();
    }

    public void Init(int amountOfLives = 3, int amountOfCashedLaserShots = 10)
    {
        this.amountOfLives = amountOfLives;

        playerShoot.amountOfCashedLaserShots = amountOfCashedLaserShots;
        playerShoot.InstantiateLaserShots(amountOfCashedLaserShots);

        explosionObject.SetActive(false);
    }

    private void LoseHeath()
    {
        if (amountOfLives > 1)
        {
            amountOfLives--;

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
        playerMovement.enabled = enable;
        playerShoot.enabled = enable;
        spriteRenderer.enabled = enable;
        boxCollider2D.enabled = enable;
    }
    private void PlayerDies()
    {
        explosionObject.SetActive(false);

        EnableShip(false);

        Invoke("StopAllActions", 0.5f);
        //animation

        PlayerDeathEvent.PlayerDeath();
    }

    public void StopAllActions()
    {
        playerMovement.SetMovementActive(false);
        playerShoot.SetActiveShoot(false);

        EnablePlayerComponents(false);
    }
    private void EnableShip(bool enable)
    {
        spriteRenderer.enabled = enable;
        boxCollider2D.enabled = enable;
    }
}
