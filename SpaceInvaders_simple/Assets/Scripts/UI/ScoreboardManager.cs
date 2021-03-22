using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> scoreTexts;

    [SerializeField]
    private PlayerPrefsManager playerPrefsManager;

    [SerializeField]
    private GameConfig gameConfig;

    // Start is called before the first frame update
    void Start()
    {
        playerPrefsManager.Init(gameConfig.amountOfScores);
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        List<int> scoreboardScores = playerPrefsManager.ReadPlayerPrefs(gameConfig.amountOfScores);

        for (int indexOfScore = 1; indexOfScore <= scoreTexts.Count; indexOfScore++)
        {
            scoreTexts[indexOfScore - 1].text = "Player " + indexOfScore + ": " + scoreboardScores[indexOfScore - 1].ToString();
        }
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
