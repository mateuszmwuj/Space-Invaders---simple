using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button statsButton;

    public void OpenGameplayScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    
    public void OpenStatsScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
