using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _scoreTexts;

    [SerializeField]
    private PlayerPrefsManager _playerPrefsManager;

    [SerializeField]
    private GameConfig _gameConfig;

    // Start is called before the first frame update
    void Start()
    {
        _playerPrefsManager.Init(_gameConfig.amountOfScores);
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        List<int> scoreboardScores = _playerPrefsManager.ReadPlayerPrefs(_gameConfig.amountOfScores);

        for (int indexOfScore = 1; indexOfScore <= _scoreTexts.Count; indexOfScore++)
        {
            _scoreTexts[indexOfScore - 1].text = "Player " + indexOfScore + ": " + scoreboardScores[indexOfScore - 1].ToString();
        }
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
