using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private EnemiesContainerManager _enemiesContainer;
    public EnemiesContainerManager enemiesContainer => _enemiesContainer; 

    private List<EnemyShootingShip> _enemyShootShips;

    private float shootTimerCounter = 0f;
    private float shootTimerDiff = 0f;
    private float shootTimerMin = 4f;
    private float shootTimerMax = 7f;
    private float shootTimerRandomMax = 2f;

    private float difficultyValue = 0.0f;
    private float difficultyBaseValue = 0.01f;

    private void OnEnable()
    {
        EnemyDiesEvents.EnemyDies += IncreaseDifficulty;
    }
    private void OnDisable()
    {
        EnemyDiesEvents.EnemyDies -= IncreaseDifficulty;
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyShootShips = _enemiesContainer.enemyShootShips;

        shootTimerDiff = shootTimerMax;
    }

    public void Init(float shootTimerMin, float shootTimerMax)
    {
        this.shootTimerMin = shootTimerMin;
        this.shootTimerMax = shootTimerMax;
    }
    // Update is called once per frame
    void Update()
    {
        shootTimerCounter += Time.deltaTime;

        if (shootTimerCounter >= shootTimerDiff - difficultyValue)
        {
            RandomEnemyShoot();

            shootTimerDiff = Random.Range(shootTimerMin, shootTimerRandomMax);

            shootTimerCounter = 0;
        }
    }

    public void RandomEnemyShoot()
    {
        EnemyShootingShip enemyShootShip = _enemyShootShips[Random.Range(0, _enemyShootShips.Count)];

        if (enemyShootShip.gameObject.activeInHierarchy && enemyShootShip.enemyShoot.canShoot)
        {
            enemyShootShip.enemyShoot.ShootLaser(SpaceShipsTypes.Enemy, _enemyShootShips.IndexOf(enemyShootShip));
        }
    }

    private void IncreaseDifficulty()
    {
        difficultyValue += difficultyBaseValue;
    }
    public void StopAll()
    {
        _enemiesContainer.ShipsEnableToMove(false);
    }
}