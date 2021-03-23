using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostArrow : MonoBehaviour
{
    [SerializeField]
    private Button _boostArrowButton;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private GameConfig _gameConfig;

    private float _timerCounter = 40.0f;
    private float _timerStartBase = 40.0f;
    private float _timerMax = 0.0f;

    private bool _startCounting = false;

    private void OnEnable()
    {
        PlayerBoostEvent.PlayerBoostEnd += StartCountingBoostTimer;
        PlayerBoostEvent.StopCounting += StopCountingBoostTimer;
    }

    private void OnDisable()
    {
        PlayerBoostEvent.PlayerBoostEnd -= StartCountingBoostTimer;        
        PlayerBoostEvent.StopCounting -= StopCountingBoostTimer;        
    }
    // Start is called before the first frame update
    void Start()
    {
        _timerCounter = _gameConfig.boostLoadingTimer;
        _timerStartBase = _gameConfig.boostLoadingTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startCounting == true)
        {
            _timerCounter -= Time.deltaTime;
            _timerText.text = ((int)_timerCounter).ToString();

            if (_timerCounter <= _timerMax)
            {
                _startCounting = false;

                ResetTimer();

                SetBoostButtonActive(true);
                SetButtonColor(_gameConfig.boostButtonBaseColor);

                SetBoostTimerActive(false);

                PlayerBoostEvent.PlayerBoostReady();
            }
        }
    }

    private void SetBoostButtonActive(bool active)
    {
        _boostArrowButton.enabled = active;
    }
    private void SetBoostTimerActive(bool active)
    {
        _timerText.gameObject.SetActive(active);

        _timerText.text = _gameConfig.boostLoadingTimer.ToString();
    }

    private void SetButtonColor(Color color)
    {
        _image.color = color;
    }
    private void ResetTimer()
    {
        _timerCounter = _timerStartBase;
    }

    private void StartCountingBoostTimer()
    {
        SetBoostButtonActive(false);
        SetButtonColor(_gameConfig.boostButtonFadeColor);

        ResetTimer();
        SetBoostTimerActive(true);

        _startCounting = true;
    }
    private void StopCountingBoostTimer()
    {
        SetBoostButtonActive(true);
        SetButtonColor(_gameConfig.boostButtonBaseColor);

        ResetTimer();
        SetBoostTimerActive(false);

        _startCounting = false;
    }

    public void BoostButtonClick()
    {
        SetBoostButtonActive(false);
        SetButtonColor(_gameConfig.boostButtonFadeColor);

        PlayerBoostEvent.PlayerBoostClick();
    }
}
