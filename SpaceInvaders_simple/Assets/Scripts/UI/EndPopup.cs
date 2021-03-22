using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    
    [SerializeField]
    private TextMeshProUGUI scoreAmountText;

    public void Init(string titleText, Color titleTextColor, int score)
    {
        this.titleText.text = titleText;
        this.titleText.color = titleTextColor;

        this.scoreAmountText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
