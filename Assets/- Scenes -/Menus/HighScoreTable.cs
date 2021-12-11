using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        
        
        /*
        {
            new HighScoreEntry {score = 99999999, name = "BRNNR"},
            new HighScoreEntry {score = 76385923, name = "MGMAN"},
            new HighScoreEntry {score = 50897368, name = "ZERO"},
            new HighScoreEntry {score = 48987098, name = "SIGMA"},
            new HighScoreEntry {score = 10879367, name = "VILE"},
        };*/


        // AddHighScoreEntry(11000000, "BRNTST");
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    HighScoreEntry tmp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = tmp;
                }
            }
        }


        highScoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }


    private void AddHighScoreEntry(int score, string name)
    {
        // Create High Score Entry
        HighScoreEntry highScoreEntry = new HighScoreEntry {score = score, name = name};

        // Load High Scores
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        // Add new entry to High Scores
        highScores.highScoreEntryList.Add(highScoreEntry);

        // Save Updated high Scores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }


    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container,
        List<Transform> transformList)
    {

        float templateHeight = 114f;
        
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
                case 1:
                    rankString = "1";
                    break;
                case 2:
                    rankString = "2";
                    break;
                case 3:
                    rankString = "3";
                    break;
                case 4:
                    rankString = "4";
                    break;
                case 5:
                    rankString = "5";
                    break;

                default:
                    rankString = "0";
                    break;
        }


        entryTransform.Find("rankText").GetComponent<Text>().text = rankString;

        int score = highScoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();


        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


        if (rank == 1)
        {

            entryTransform.Find("rankText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;

        }

        /*switch (rank)
        {
            default:
                entryTransform.Find("crownsContainer").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("crownsContainer").GetComponent<Image>().color =
                    UtilsClass.GetColorFromString("FFD200");
                break;
            case 2:
                entryTransform.Find("crownsContainer").GetComponent<Image>().color =
                    UtilsClass.GetColorFromString("C6C6C6");
                break;
            case 3:
                entryTransform.Find("crownsContainer").GetComponent<Image>().color =
                    UtilsClass.GetColorFromString("B76F56");
                break;
        }*/

        transformList.Add(entryTransform);
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


    [System.Serializable]
    private class HighScoreEntry
    { 
        public int score;
        public string name;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
