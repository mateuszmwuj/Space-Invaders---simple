using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _statsButton;

    public void OpenGameplayScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    
    public void OpenStatsScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
