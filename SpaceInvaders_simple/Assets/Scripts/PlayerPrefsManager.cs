using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private string startPlayerPrefsName = "Score:";

    private List<int> randomValuesList = new List<int>();

    private int amountOfScores;
    // Start is called before the first frame update
    void Awake()
    {
    }

    public void Init(int amountOfScores)
    {
        this.amountOfScores = amountOfScores;
        
        InitRandomValues();

        if (!PlayerPrefs.HasKey("Score:1"))
            WriteToPlayerPrefs(null, 10);
    }

    public void WriteToPlayerPrefs(List<int> newList, int amountOfScores= 10)
    {
        if (newList == null || newList.Count == 0)
        {
            newList = randomValuesList;
        }
        for (int numberOfScores = 1; numberOfScores <= amountOfScores; numberOfScores++)
        {
            string prefsName = startPlayerPrefsName + numberOfScores;

            PlayerPrefs.SetInt(prefsName, newList[numberOfScores - 1]);
        }

        PlayerPrefs.Save();
    }

    private void InitRandomValues()
    {
        for (int numberOfScores = 1; numberOfScores <= amountOfScores; numberOfScores++)
        {
            randomValuesList.Add(Random.Range(0, 25));
        }
        randomValuesList.Sort();
        randomValuesList.Reverse();
    }

    public void UpdatePlayerPrefs(int newScore, int amountOfScores = 10)
    {
        if (PlayerPrefs.HasKey("Score:1"))
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
            string prefsName = startPlayerPrefsName + numberOfScores;

            if (PlayerPrefs.HasKey(prefsName))
            {
                tempValuesList.Add(PlayerPrefs.GetInt(prefsName));
            }
        }

        return tempValuesList;
    }
}
