using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private string _startPlayerPrefsName = "Score:";
    private string _firstPlayerPrefsScoreName = "Score:1";

    private List<int> _randomValuesList = new List<int>();

    private int _amountOfScores;

    public void Init(int amountOfScores)
    {
        _amountOfScores = amountOfScores;
        
        InitRandomValues();

        if (!PlayerPrefs.HasKey(_firstPlayerPrefsScoreName))
            WriteToPlayerPrefs(null, 10);
    }

    public void WriteToPlayerPrefs(List<int> newList, int amountOfScores= 10)
    {
        if (newList == null || newList.Count == 0)
        {
            newList = _randomValuesList;
        }
        for (int numberOfScores = 1; numberOfScores <= amountOfScores; numberOfScores++)
        {
            string prefsName = _startPlayerPrefsName + numberOfScores;

            PlayerPrefs.SetInt(prefsName, newList[numberOfScores - 1]);
        }

        PlayerPrefs.Save();
    }

    private void InitRandomValues()
    {
        for (int numberOfScores = 1; numberOfScores <= _amountOfScores; numberOfScores++)
        {
            _randomValuesList.Add(Random.Range(0, 25));
        }
        _randomValuesList.Sort();
        _randomValuesList.Reverse();
    }

    public void UpdatePlayerPrefs(int newScore, int amountOfScores = 10)
    {
        if (PlayerPrefs.HasKey(_firstPlayerPrefsScoreName))
        {
            List<int> tempValuesList = new List<int>();

            tempValuesList = ReadPlayerPrefs(amountOfScores);

            tempValuesList.Add(newScore);
            tempValuesList.Sort();
            tempValuesList.Reverse();
            tempValuesList.RemoveAt(tempValuesList.Count - 1);

            WriteToPlayerPrefs(tempValuesList, amountOfScores);
        }

        PlayerPrefs.Save(); 
    }

    public List<int> ReadPlayerPrefs(int amountOfScores)
    {
        List<int> tempValuesList = new List<int>();

        for (int numberOfScores = 1; numberOfScores <= amountOfScores; numberOfScores++)
        {
            string prefsName = _startPlayerPrefsName + numberOfScores;

            if (PlayerPrefs.HasKey(prefsName))
            {
                tempValuesList.Add(PlayerPrefs.GetInt(prefsName));
            }
        }

        return tempValuesList;
    }
}
