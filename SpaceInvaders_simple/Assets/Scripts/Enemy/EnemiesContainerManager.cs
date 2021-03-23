using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainerManager : MonoBehaviour
{
    private float _horizontalContainerSpeed;

    private bool _moveLeft = true;
    private bool _moveRight = false;
    private bool _moveDown = false;

    private int _enemiesRow = 5;
    private int _enemiesColumn = 5;

    private float _difficultyValueSide = 0.0f;
    private float _difficultyValueDown = 0.0f;
    private float _difficultyBaseSideValue = 0.05f;
    private float _difficultyBaseDownValue = 0.005f;

    public int amountOfCachedLaserShots = 0;

    [SerializeField]
    private List<EnemyShootingShip> _enemyShootShips = new List<EnemyShootingShip>();
    public List<EnemyShootingShip> enemyShootShips => _enemyShootShips;

    [SerializeField]
    private List<EnemyBasicShip> _enemyShips = new List<EnemyBasicShip>();
    public List<EnemyBasicShip> enemyShips => _enemyShips;

    private bool _enableToMove = true;
    public bool enableToMove => _enableToMove;

    public int amountOfActiveShips = 0;

    private void OnEnable()
    {
        EnemyMovementTriggerEvents.LeftWallEnter += MoveRightHandler;
        EnemyMovementTriggerEvents.RightWallEnter += MoveLeftHandler;
        EnemyDiesEvents.EnemyDies += EnemyDies;
        ScoreEvents.PlayerHitByEnemiesLaser += PlayerHitByEnemiesLaser;
        PlayerWinEvent.PlayerWin += StopAllShips;
        PlayerDeathEvent.PlayerDeath += StopAllShips;
    }
    private void OnDisable()
    {
        EnemyMovementTriggerEvents.LeftWallEnter -= MoveRightHandler;
        EnemyMovementTriggerEvents.RightWallEnter -= MoveLeftHandler;
        EnemyDiesEvents.EnemyDies -= EnemyDies;
        ScoreEvents.PlayerHitByEnemiesLaser -= PlayerHitByEnemiesLaser;
        PlayerWinEvent.PlayerWin -= StopAllShips;
        PlayerDeathEvent.PlayerDeath -= StopAllShips;
    }
    // Start is called before the first frame update
    void Start()
    {
        _horizontalContainerSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enableToMove)
        {
            MoveContainerLeft();
            MoveContainerRight();
        }
    }

    public void Init()
    {
        amountOfActiveShips = _enemyShips.Count;

        foreach (EnemyShootingShip enemyShootShip in enemyShootShips)
        {
            if (enemyShootShip.gameObject.activeInHierarchy != true)
            {
                enemyShootShip.gameObject.SetActive(true);
            }
            enemyShootShip.enemyShoot.Init(amountOfCachedLaserShots, enemyShootShips.IndexOf(enemyShootShip));
        }
    }

    private void MoveContainerLeft()
    {
        if (_moveLeft)
        {
            transform.localPosition -= new Vector3(Time.deltaTime * (_horizontalContainerSpeed + _difficultyValueSide), 0, 0);

            if (_moveDown)
            {
                MoveContainerDown();

                _moveDown = false;
            }
        }
    }
    private void MoveContainerRight()
    {
        if (_moveRight)
        {
            transform.localPosition += new Vector3(Time.deltaTime * (_horizontalContainerSpeed + _difficultyValueSide), 0, 0);

            if (_moveDown)
            {
                MoveContainerDown();

                _moveDown = false;
            }
        }
    }

    private void MoveContainerDown()
    {
        transform.localPosition -= new Vector3(0, 0.1f + _difficultyValueDown, 0);
    }

    private void MoveLeftHandler()
    {
        _moveLeft = true;
        _moveRight = false;

        _moveDown = true;
    }
    private void MoveRightHandler()
    {
        _moveLeft = false;
        _moveRight = true;

        _moveDown = true;
    }

    public void ShipsEnableToMove(bool enable)
    {
        _enableToMove = enable;
    }

    private void EnemyDies()
    {
        amountOfActiveShips--;

        if (amountOfActiveShips <= 0)
        {
            PlayerWinEvent.PlayerWin();
        }
        else
        {
            _difficultyValueSide += _difficultyBaseSideValue;
            _difficultyValueDown += _difficultyBaseDownValue;
        }
    }

    private int CalculateAmountOfEnemiesInColumn(EnemyBasicShip enemyBasicShip)
    {
        int indexOfShip = enemyShips.IndexOf(enemyBasicShip);

        int shipRow = 0;
        int counter = 0;

        if (indexOfShip > 0)
        {
            if (indexOfShip < _enemiesRow)
            {
                shipRow = indexOfShip;
            }
            else
            {
                int modulo = indexOfShip / _enemiesRow;
                shipRow = modulo * _enemiesRow;
            }

            for (int numberOfEnemies = 0; numberOfEnemies < _enemiesColumn; numberOfEnemies++)
            {
                if (enemyShips[shipRow + shipRow * numberOfEnemies].gameObject.activeInHierarchy)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    private int GetNumberOfActiveEnemies()
    {
        int counter = 0;

        foreach (EnemyBasicShip enemyShip in _enemyShips)
        {
            if (enemyShip.gameObject.activeInHierarchy)
            {
                counter++;
            }
        }
        return counter;
    }

    private void PlayerHitByEnemiesLaser(int indexOfEnemy)
    {
        int counter = 0;

        if (indexOfEnemy > 0)
        {
            for (int numberOfEnemies = 0; numberOfEnemies < _enemiesColumn; numberOfEnemies++)
            {
                if (enemyShips[indexOfEnemy + indexOfEnemy * numberOfEnemies].gameObject.activeInHierarchy)
                {
                    counter++;
                }
            }
        }

        ScoreEvents.ScoreEnemyAmount(counter);
    }

    private void StopAllShips()
    {
        foreach (EnemyBasicShip enemyShip in enemyShips)
        {
            if (enemyShip.gameObject.activeInHierarchy == true)
            {
                enemyShip.SetComponentsStateTo(false);

                _enableToMove = false;
            }
        }
    }
}
