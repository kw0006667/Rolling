using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ScoreData
{
    public int ID;
    public string UserName;
    public uint Score;
    public string GameTime;
    public uint Coppers;
    public uint Silvers;
    public uint Golds;
    public string Rank;
    public DateTime PlayDate;
    public string Scene;

    public ScoreData()
    {

    }

    public ScoreData(int id, string username, uint score, string time,uint coppers, uint silvers, uint golds, string rank, DateTime date, string scene)
    {
        this.ID = id;
        this.UserName = username;
        this.Score = score;
        this.GameTime = time;
        this.Coppers = coppers;
        this.Silvers = silvers;
        this.Golds = golds;
        this.Rank = rank;
        this.PlayDate = date;
        this.Scene = scene;
    }
}

public class ScoreTag
{
    public const string ScoresTag = "Scores";
    public const string UserTag = "User";
    public const string ID = "ID";
    public const string UserName = "UserName";
    public const string Score = "Score";
    public const string GameTime = "GameTime";
    public const string Coppers = "Coppers";
    public const string Silvers = "Silvers";
    public const string Golds = "Golds";
    public const string Rank = "Rank";
    public const string PlayDate = "PlayDate";
    public const string Scene = "Scene";
}
public class FileManager
{
    private List<ScoreData> scoreList;

    public FileManager()
    {
        this.scoreList = new List<ScoreData>();
        
    }

    public void ScoresReader(string filename)
    {
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreComments = true;
        settings.IgnoreWhitespace = true;
        settings.ValidationType = ValidationType.None;
        XmlReader reader = XmlTextReader.Create(filename, settings);
        while (reader.Read())
        {
            ScoreData score = null;
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    string tagName = reader.LocalName;
                    if (tagName.Equals(ScoreTag.UserTag))
                    {
                        score = new ScoreData(Convert.ToInt32(reader[ScoreTag.ID]),
                                                              reader[ScoreTag.UserName],
                                             Convert.ToUInt32(reader[ScoreTag.Score]),
                                                              reader[ScoreTag.GameTime],
                                             Convert.ToUInt32(reader[ScoreTag.Coppers]),
                                             Convert.ToUInt32(reader[ScoreTag.Silvers]),
                                             Convert.ToUInt32(reader[ScoreTag.Golds]),
                                                              reader[ScoreTag.Rank],
                                           Convert.ToDateTime(reader[ScoreTag.PlayDate]),
                                                              reader[ScoreTag.Scene]);
                        this.scoreList.Add(score);
                    }
                    break;
                default:
                    break; 
            }
        }
        reader.Close();
    }

    public List<ScoreData> GetScores()
    {
        return this.scoreList;
    }

    public List<ScoreData> GetHighScores(string scene, int count)
    {
        var query =
            from score in this.scoreList
            where score.Scene.Equals(scene)
            select score;

        List<ScoreData> scores = query.ToList<ScoreData>();
        scores.Sort(new ScoreCompare());
        if (scores.Count >= count)
            return scores.GetRange(0, count);
        else
            return scores.GetRange(0, scores.Count);
    }

    private class ScoreCompare : IComparer<ScoreData>
    {
        public int Compare(ScoreData x, ScoreData y)
        {
            return -x.Score.CompareTo(y.Score);
        }
    }
}
