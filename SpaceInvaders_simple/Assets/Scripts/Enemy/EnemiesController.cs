using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private EnemiesContainerManager _enemiesContainer;
    public EnemiesContainerManager enemiesContainer { get { return _enemiesContainer; } }

    private List<EnemyShootShip> enemyShootShips;

    protected float shootTimerCounter = 0f;
    protected float shootTimerDiff = 0f;
    protected float shootTimerMin = 4f;
    protected float shootTimerMax = 7f;
    protected float shootTimerRandomMax = 2f;

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
        enemyShootShips = _enemiesContainer.enemyShootShips;

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
        //foreach (EnemyShootShip enemyShootShip in enemyShootShips)
        EnemyShootShip enemyShootShip = enemyShootShips[Random.Range(0, enemyShootShips.Count)];
        if (enemyShootShip.gameObject.activeInHierarchy && enemyShootShip.enemyShoot.canShoot)
        {
            enemyShootShip.enemyShoot.ShootLaser(SpaceShipsTypes.Enemy, enemyShootShips.IndexOf(enemyShootShip));
            //break;
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