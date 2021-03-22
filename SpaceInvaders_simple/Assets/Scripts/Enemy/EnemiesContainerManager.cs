using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainerManager : MonoBehaviour
{
    private float horizontalSpeed;
    private Vector3 startPosition;

    private bool moveLeft = true;
    private bool moveRight = false;
    private bool moveDown = false;

    private int enemiesRow = 5;
    private int enemiesColumn = 5;

    private float difficultyValueSide = 0.0f;
    private float difficultyValueDown = 0.0f;
    private float difficultyBaseSideValue = 0.05f;
    private float difficultyBaseDownValue = 0.005f;

    public int amountOfCashedLaserShots = 0;

    [SerializeField]
    private List<EnemyShootShip> _enemyShootShips = new List<EnemyShootShip>();
    public List<EnemyShootShip> enemyShootShips { get { return _enemyShootShips; } }

    [SerializeField]
    private List<EnemyBasicShip> _enemyShips = new List<EnemyBasicShip>();
    public List<EnemyBasicShip> enemyShips { get { return _enemyShips; } }

    private bool _enableToMove = true;
    public bool enableToMove { get { return _enableToMove; } }

    public int amountOfActiveShips = 0;

    private void OnEnable()
    {
        EnemyMovementTriggerEvents.LeftWallEnter += MoveRightHandler;
        EnemyMovementTriggerEvents.RightWallEnter += MoveLeftHandler;
        EnemyDiesEvents.EnemyDies += EnemyDies;
        EnemyDiesEvents.EnemyDiesWithInfo += EnemyDiesWithInfo;
        ScoreEvents.PlayerHitByEnemiesLaser += PlayerHitByEnemiesLaser;
    }
    private void OnDisable()
    {
        EnemyMovementTriggerEvents.LeftWallEnter -= MoveRightHandler;
        EnemyMovementTriggerEvents.RightWallEnter -= MoveLeftHandler;
        EnemyDiesEvents.EnemyDies -= EnemyDies;
        EnemyDiesEvents.EnemyDiesWithInfo -= EnemyDiesWithInfo;
        ScoreEvents.PlayerHitByEnemiesLaser -= PlayerHitByEnemiesLaser;
    }
    // Start is called before the first frame update
    void Start()
    {
        horizontalSpeed = 1f;
        startPosition = transform.position;
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
        //transform.position = startPosition;

        amountOfActiveShips = _enemyShips.Count;

        foreach (EnemyShootShip enemyShootShip in enemyShootShips)
        {
            if (!enemyShootShip.gameObject.activeInHierarchy)
            {
                enemyShootShip.gameObject.SetActive(true);
            }
            enemyShootShip.enemyShoot.Init(amountOfCashedLaserShots, enemyShootShips.IndexOf(enemyShootShip));
        }
        //foreach (EnemyBasicShip enemyShootShip in _enemyShips)
        //{
        //    if()
        //    _enemyShootShips.Add(enemyShootShip);
        //}
    }

    private void MoveContainerLeft()
    {
        if (moveLeft)
        {
            transform.localPosition -= new Vector3(Time.deltaTime * (horizontalSpeed + difficultyValueSide), 0, 0);

            if (moveDown)
            {
                MoveContainerDown();

                moveDown = false;
            }
        }
        //transform.localPosition = startPosition - new Vector3(Mathf.Sin(Time.time * horizontalSpeed) / 2f, 0, 0);
    }
    private void MoveContainerRight()
    {
        if (moveRight)
        {
            transform.localPosition += new Vector3(Time.deltaTime * (horizontalSpeed + difficultyValueSide), 0, 0);

            if (moveDown)
            {
                MoveContainerDown();

                moveDown = false;
            }
        }
    }

    private void MoveContainerDown()
    {
        transform.localPosition -= new Vector3(0, 0.1f + difficultyValueDown, 0);
    }

    private void MoveLeftHandler()
    {
        moveLeft = true;
        moveRight = false;

        moveDown = true;
    }
    private void MoveRightHandler()
    {
        moveLeft = false;
        moveRight = true;

        moveDown = true;
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
            difficultyValueSide += difficultyBaseSideValue;
            difficultyValueDown += difficultyBaseDownValue;
        }
    }

    private void EnemyDiesWithInfo(EnemyBasicShip enemyBasicShip)
    {
        
    }


    private int CalculateAmountOfEnemiesInColumn(EnemyBasicShip enemyBasicShip)
    {
        int indexOfShip = enemyShips.IndexOf(enemyBasicShip);

        int shipRow = 0;
        int counter = 0;

        if (indexOfShip > 0)
        {
            if (indexOfShip < enemiesRow)
            {
                shipRow = indexOfShip;
            }
            else
            {
                int modulo = indexOfShip / enemiesRow;
                shipRow = modulo * enemiesRow;
            }

            for (int numberOfEnemies = 0; numberOfEnemies < enemiesColumn; numberOfEnemies++)
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
            for (int numberOfEnemies = 0; numberOfEnemies < enemiesColumn; numberOfEnemies++)
            {
                if (enemyShips[indexOfEnemy + indexOfEnemy * numberOfEnemies].gameObject.activeInHierarchy)
                {
                    counter++;
                }
            }
        }

        ScoreEvents.ScoreEnemyAmount(counter);
    }
}
