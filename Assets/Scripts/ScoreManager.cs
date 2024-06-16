using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Transform scoreParent;
    [SerializeField] private GameObject scoreboardParent;
    public GameObject nameInputPanel;
    [SerializeField] private TMP_InputField nameInputField;

    private List<PlayerScore> highScores = new List<PlayerScore>();

    private void Start()
    {
        
        LoadScores();
        DisplayScores();
        nameInputPanel.SetActive(false);
    }

    public void AddNewScore(float score)
    {
        nameInputPanel.SetActive(true);
        nameInputField.onEndEdit.AddListener(delegate { SubmitName(score); });
    }

    private void SubmitName(float score)
    {
        string playerName = nameInputField.text;
        PlayerScore newScore = new PlayerScore { Name = playerName, Score = score };
        highScores.Add(newScore);
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }
        SaveScores();
        DisplayScores();
        nameInputPanel.SetActive(false);
    }

    private void DisplayScores()
    {
        foreach (Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject scoreObject = Instantiate(scorePrefab, scoreParent);
            TMP_Text[] texts = scoreObject.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (i + 1).ToString(); // Place number
            texts[1].text = highScores[i].Name; // Name
            
            string minutes = ((int) highScores[i].Score / 60).ToString("00");
            string seconds = (highScores[i].Score % 60).ToString("00.00");

            texts[2].text = minutes + ":" + seconds; // Score
        }
    }

    private void LoadScores()
    {
        highScores.Clear();
        for (int i = 0; i < 10; i++)
        {
            string nameKey = "HighScoreName" + i;
            string scoreKey = "HighScoreValue" + i;
            if (PlayerPrefs.HasKey(nameKey) && PlayerPrefs.HasKey(scoreKey))
            {
                PlayerScore score = new PlayerScore
                {
                    Name = PlayerPrefs.GetString(nameKey),
                    Score = PlayerPrefs.GetFloat(scoreKey)
                };
                highScores.Add(score);
            }
            highScores.Sort((a, b) => a.Score.CompareTo(b.Score));
        }
    }

    private void SaveScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetString("HighScoreName" + i, highScores[i].Name);
            PlayerPrefs.SetFloat("HighScoreValue" + i, highScores[i].Score);
        }
    }

    public void ShowScoreboard()
    {
        scoreboardParent.SetActive(!scoreboardParent.activeSelf);
    }
}

[System.Serializable]
public class PlayerScore
{
    public string Name;
    public float Score;
}
