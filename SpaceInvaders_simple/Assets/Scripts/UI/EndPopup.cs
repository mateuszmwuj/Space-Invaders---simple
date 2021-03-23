using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _titleText;
    
    [SerializeField]
    private TextMeshProUGUI _scoreAmountText;

    private void OnEnable()
    {
        ScoreEvents.ScoreSync += ScoreSync;
    }
    private void OnDisable()
    {
        ScoreEvents.ScoreSync -= ScoreSync;        
    }

    public void Init(string titleText, Color titleTextColor, int score)
    {
        this._titleText.text = titleText;
        this._titleText.color = titleTextColor;

        this._scoreAmountText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void ScoreSync(int score)
    {
        this._scoreAmountText.text = score.ToString();
    }
}
