using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Shoot
{
    private float _timerBoostCounter;
    private float _timerBoostUsageMax;
    private bool _startCounting = false;

    private void OnEnable()
    {
        PlayerBoostEvent.PlayerBoostClick += StartBoostShooting;
    }
    private void OnDisable()
    {
        PlayerBoostEvent.PlayerBoostClick -= StartBoostShooting;        
    }

    protected new void Update()
    {
        base.Update();

        if (_startCounting == true)
        {
            _timerBoostCounter -= Time.deltaTime;

            if (_timerBoostCounter <= 0.0f)
            {
                _timerBoostCounter = _timerBoostUsageMax;

                _startCounting = false;
                
                this.timerMax = this.timerMaxBoost;

                PlayerBoostEvent.PlayerBoostEnd();
            }
        }

        ShootLaser(SpaceShipsTypes.Player);
    }


    public void SetShootingMaxTimer(float timerMax)
    {
        this.timerMax = timerMax;
        this.timerMaxBase = timerMax;
    }
    public void SetShootingBoostMaxTimer(float timerMaxBoost)
    {
        this.timerMaxBoost = timerMaxBoost;
    }
    
    public void SetTimerBoostUsageMax(float timerBoostUsageMax)
    {
        this._timerBoostUsageMax = timerBoostUsageMax;
        this._timerBoostCounter = timerBoostUsageMax;
    }

    private void StartBoostShooting()
    {
        _startCounting = true;

        this.timerMax = this.timerMaxBoost;
    }
}
