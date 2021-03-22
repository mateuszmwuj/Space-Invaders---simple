using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    protected LaserShot laserShotPrefab;
    [SerializeField]
    protected GameObject spawnerPrefab;
    [SerializeField]
    protected GameObject laserShotsContainer;

    public List<LaserShot> laserShotsList = new List<LaserShot>();
    private LaserShot laserShotObject;

    protected float timerCounter = 5f;
    protected float timerMax = 2f;
    protected float timerMaxBoost = 1f;

    public int amountOfCashedLaserShots = 0;
    public int indexOfShooter = -1;

    private bool _canShoot = true;
    public bool canShoot { get { return _canShoot; } }

    private bool shootingEnabled = true;

    private void OnEnable()
    {
        PlayerDeathEvent.PlayerDeath += StopShooting;
        PlayerWinEvent.PlayerWin += StopShooting;
    }
    private void OnDisable()
    {
        PlayerDeathEvent.PlayerDeath -= StopShooting;
        PlayerWinEvent.PlayerWin -= StopShooting;
    }
    protected void Start()
    {
        InstantiateLaserShot();
    }
    // Update is called once per frame
    protected void Update()
    {
        if (shootingEnabled)
            LaserShotTimerChecker();

        if (Input.GetKey(KeyCode.R))
        {
            DestroyAllLaserShots();
        }
    }

    protected void LaserShotTimerChecker()
    {
        timerCounter += Time.deltaTime;

        if (timerCounter >= timerMax || timerCounter >= timerMaxBoost)
        {
            //InstantiateLaserShot();

            timerCounter = 0;

            _canShoot = true;
        }
    }
    protected void InstantiateLaserShot(bool nowShoot = false, int indexOfShooter = -1)
    {
        laserShotObject = Instantiate(laserShotPrefab, spawnerPrefab.transform.position, Quaternion.identity, laserShotsContainer.transform);
        
        laserShotObject.indexOfShooter = indexOfShooter;

        laserShotsList.Add(laserShotObject);

        laserShotObject.gameObject.SetActive(nowShoot);

        _canShoot = !nowShoot;
    }

    public void InstantiateLaserShots(int amount, int indexOfShooter = -1)
    {
        for (int numberOflasers = 1; numberOflasers <= amount; numberOflasers++)
        {
            InstantiateLaserShot(false, indexOfShooter);
        }
    }

    protected LaserShot GetFirstAvailableLaserShot()
    {
        LaserShot laserShotReturn = null;

        foreach (LaserShot laserShot in laserShotsList)
        {
            if (laserShot.gameObject.activeInHierarchy != true)
            {
                laserShotReturn = laserShot;
            }
        }

        return laserShotReturn;
    }
    public void ShootLaser(SpaceShipsTypes spaceShipsType, int indexOfEnemy = -1)
    {
        if (_canShoot && laserShotObject.gameObject.activeInHierarchy == false)
        {
            laserShotObject = GetFirstAvailableLaserShot();

            if (laserShotObject != null)
            {
                laserShotObject.transform.position = spawnerPrefab.transform.position;
                laserShotObject.gameObject.SetActive(true);

                SoundManager.Instance.PlayRandomShootSoundByEnum(spaceShipsType);

                _canShoot = false;
            }
            //foreach (LaserShot laserShot in laserShotsList)
            //{
            //    if (laserShot.gameObject.activeInHierarchy == true)
            //    {
            //        laserShot.gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        //InstantiateLaserShot(true);
            //    }
            //}
        }
    }

    public void SetActiveShoot(bool active)
    {
        shootingEnabled = active;
    }

    private void StopShooting()
    {
        shootingEnabled = false;
        _canShoot = false;
    }

    protected void DestroyAllLaserShots()
    {
        int counter = 0;

        while (laserShotsList.Count > 0 && counter < 500)
        {
            LaserShot laserShot = laserShotsList[0];
            laserShotsList.Remove(laserShot);
            Destroy(laserShot.gameObject);

            counter++;
        }

        if (counter >= 500)
        {
            Debug.LogError("Destroy laserShots while endless");

            counter = 0;
        }
    }
}
