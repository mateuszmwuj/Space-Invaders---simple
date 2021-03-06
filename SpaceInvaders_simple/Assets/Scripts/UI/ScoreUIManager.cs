using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreAmount;

    // Start is called before the first frame update
    void Start()
    {
        _scoreAmount.text = "0";
    }

    private void OnEnable()
    {
        ScoreEvents.ScoreSync += SyncScore;
    }

    private void OnDisable()
    {
        ScoreEvents.ScoreSync -= SyncScore;        
    }

    private void SyncScore(int score)
    {
        _scoreAmount.text = score.ToString();
    }
}
