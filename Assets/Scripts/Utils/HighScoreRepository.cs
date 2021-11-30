using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreRepository
{
    private const string SAVE_GAME_FILENAME = "HIGHSCORE.xml";

    private static HighScoreRepository _instance = null;

    public static HighScoreRepository Instance
    {
        get
        {
            if (_instance == null)
                _instance = new HighScoreRepository();
            return _instance;
        }
    }


    private List<HighScore> highScores;
    private readonly string fileName;

    private HighScoreRepository()
    {
        fileName = Application.persistentDataPath + SAVE_GAME_FILENAME;
        LoadHighScores();
    }


    private void LoadHighScores()
    {
        if (File.Exists(fileName))
        {
            var fileContent = File.Open(fileName, FileMode.Open);
            var xmlSerializer = new XmlSerializer(typeof(List<HighScore>));
            highScores = xmlSerializer.Deserialize(fileContent) as List<HighScore>;
            fileContent.Close();
        }

        highScores ??= new List<HighScore>();
    }

    private void SaveHighScores()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<HighScore>));
        var fileContent = File.Open(fileName, FileMode.Create);
        xmlSerializer.Serialize(fileContent, highScores);
        fileContent.Close();
    }

    public HighScore HighScore
    {
        get
        {
            if (highScores.Count == 0) return null;
            var maxHighScore = highScores.Find(h => h.score == highScores.Max(h2 => h2.score));
            return maxHighScore;
        }
    }

    public bool HasHighScore => highScores.Count > 0;

    public void AddScore(HighScore hs)
    {
        highScores.Add(hs);
        SaveHighScores();
    }
}