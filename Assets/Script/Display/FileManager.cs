using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

#region RecordTag
public class RecordTag
{
    public const string Records = "Records";
    public const string Record = "Record";
    public const string RecordName = "RecordName";
    public const string Scene = "Scene";
    public const string SaveDate = "SaveDate";
}
#endregion

#region RecordData
public class RecordData
{
    public string RecordName;
    public string Scene;
    public string SaveDate;

    public RecordData()
    {

    }

    public RecordData(string recordName, string scene, string saveDate)
    {
        this.RecordName = recordName;
        this.Scene = scene;
        this.SaveDate = saveDate;
    }
}
#endregion

#region ScoreTag
/// <summary>
/// XML's Element Tag
/// </summary>
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
#endregion

#region SettingTag
public class SettingTag
{
    public const string SettingsTag = "Settings";
    public const string SetTag = "Setting";
    public const string MachineName = "MachineName";
    public const string Quality = "Quality";
    public const string Resolution = "Resolution";
    public const string FullScreen = "FullScreen";
    public const string Key = "Key";
}
#endregion

#region SettingData Class
public class SettingData
{
    public string MachineName;
    public string Quality;
    public string Resolution;
    public string FullScreen;
    public string Key;

    public SettingData()
    {

    }

    public SettingData(string machineName, string quality, string resolution, string fullscreen, string key)
    {
        this.MachineName = machineName;
        this.Quality = quality;
        this.Resolution = resolution;
        this.FullScreen = fullscreen;
        this.Key = key;
    }
}
#endregion

#region ScoreData class
/// <summary>
/// Score Data Structure
/// </summary>
public class ScoreData
{
    // Save ID and it's only one.
    public string ID;
    // Save user's name
    public string UserName;
    // Save game's score
    public string Score;
    // Save the time which spent in the scene
    public string GameTime;
    // Save the count of copper which got
    public string Coppers;
    // Save the count of silver which got
    public string Silvers;
    // Save the count of gold which got
    public string Golds;
    // Save the final rank
    public string Rank;
    // Save the day what user played
    public string PlayDate;
    // Save the scene
    public string Scene;

    public ScoreData() { }

    /// <summary>
    /// ScoreData constructer
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="username">UserName</param>
    /// <param name="score">Score</param>
    /// <param name="time">GameTime</param>
    /// <param name="coppers">Cooppers</param>
    /// <param name="silvers">Silvers</param>
    /// <param name="golds">Golds</param>
    /// <param name="rank">Rank</param>
    /// <param name="date">PlayDate</param>
    /// <param name="scene">Scene</param>
    public ScoreData(string id, string username, string score, string time, string coppers, string silvers, string golds, string rank, string date, string scene)
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
#endregion

#region FileManager class

/// <summary>
/// File manager, support read and write score data and config to XML file format.
/// </summary>
public class FileManager
{
    // Save exception message
    public Exception Ex { get; private set; }
    // Store all username's score
    public List<ScoreData> scoreList;
    private SettingData settingData;
    private List<RecordData> recordList;
    // Filename for reading
    private string fileName;


    public FileManager()
    {
        this.scoreList = new List<ScoreData>();
        this.recordList = new List<RecordData>();
        this.fileName = string.Empty;
        this.Ex = null;
    }

    public void RecordsReader(string filename)
    {
        this.VerifyFileExist(filename);
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreComments = true;
        settings.IgnoreWhitespace = true;
        settings.ValidationType = ValidationType.None;
        XmlReader reader = XmlTextReader.Create(filename, settings);
        while (reader.Read())
        {
            RecordData record = null;
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    string tagName = reader.LocalName;
                    if (tagName.Equals(RecordTag.Record))
                    {
                        record = new RecordData(reader[RecordTag.RecordName],
                                                reader[RecordTag.Scene],
                                                reader[RecordTag.SaveDate]);
                        this.recordList.Add(record);
                    }
                    break;
                default:
                    break;
            }
        }
        reader.Close();
    }

    public void RecordWrite(RecordData record)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(GameDefinition.RecordFilePath);
        XmlNodeList setNodes = doc.SelectNodes(RecordTag.Records + "/" + RecordTag.Record);
        foreach (XmlNode setNode in setNodes)
        {
            XmlElement element = (XmlElement)setNode;
            if (element.GetAttribute(RecordTag.RecordName).Equals(record.RecordName))
            {
                element.SetAttribute(RecordTag.Scene, record.Scene);
                element.SetAttribute(RecordTag.SaveDate, record.SaveDate);
            }
        }
        doc.Save(GameDefinition.RecordFilePath);
    }

    public bool RecordDelete(int recordChoice)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GameDefinition.RecordFilePath);
            XmlNodeList setNodes = doc.SelectNodes(RecordTag.Records + "/" + RecordTag.Record);
            foreach (XmlNode setNode in setNodes)
            {
                XmlElement element = (XmlElement)setNode;
                XmlAttributeCollection attributes = element.Attributes;
                if (element.GetAttribute(RecordTag.RecordName).Equals("Record " + (recordChoice + 1).ToString()))
                {
                    element.SetAttribute(RecordTag.Scene, "");
                    element.SetAttribute(RecordTag.SaveDate, "");
                    break;
                }
            }
            doc.Save(GameDefinition.RecordFilePath);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void RecordUpdate()
    {
        this.recordList = new List<RecordData>();
        this.RecordsReader(GameDefinition.RecordFilePath);
    }

    public List<RecordData> GetRecords()
    {
        return this.recordList;
    }

    /// <summary>
    /// Read scores
    /// </summary>
    /// <param name="filename">XML format file name</param>
    public void ScoresReader(string filename)
    {
        this.fileName = filename;
        this.VerifyFileExist(filename);

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
                        score = new ScoreData(reader[ScoreTag.ID],
                                              reader[ScoreTag.UserName],
                                              reader[ScoreTag.Score],
                                              reader[ScoreTag.GameTime],
                                              reader[ScoreTag.Coppers],
                                              reader[ScoreTag.Silvers],
                                              reader[ScoreTag.Golds],
                                              reader[ScoreTag.Rank],
                                              reader[ScoreTag.PlayDate],
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

    public bool ConfigReader(string filename, string machineName)
    {
        if (this.VerifyFileExist(filename))
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            settings.ValidationType = ValidationType.None;
            XmlReader reader = XmlTextReader.Create(filename, settings);
            bool isSameName = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        string tagName = reader.LocalName;
                        if (tagName.Equals(SettingTag.SetTag))
                        {
                            if (machineName.Equals(reader[SettingTag.MachineName]))
                            {
                                this.settingData = new SettingData(reader[SettingTag.MachineName],
                                                                   reader[SettingTag.Quality],
                                                                   reader[SettingTag.Resolution],
                                                                   reader[SettingTag.FullScreen],
                                                                   reader[SettingTag.Key]);
                                isSameName = true;
                            }
                            else
                                isSameName = false;
                            
                        }
                        break;
                    default:
                        break;
                }
            }            
            reader.Close();
            // if different machine , game setting will back to default value.
            if (!isSameName)
            {
                this.settingData = new SettingData(Environment.GetEnvironmentVariable("COMPUTERNAME"), "4", "0", "False", "0");
                this.ConfigWrite(this.settingData);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ConfigWrite(SettingData setting)
    {
        this.settingData = setting;
        XmlDocument doc = new XmlDocument();
        doc.Load(GameDefinition.SettingFilePath);
        XmlNode setNode = doc.SelectSingleNode(SettingTag.SettingsTag + "/" + SettingTag.SetTag);
        XmlElement element = (XmlElement)setNode;
        XmlAttributeCollection attributes = element.Attributes;
        foreach (XmlAttribute attribute in attributes)
        {
            switch (attribute.Name)
            {
                case SettingTag.MachineName:
                    attribute.Value = this.settingData.MachineName;
                    break;
                case SettingTag.Quality:
                    attribute.Value = this.settingData.Quality;
                    break;
                case SettingTag.Resolution:
                    attribute.Value = this.settingData.Resolution;
                    break;
                case SettingTag.FullScreen:
                    attribute.Value = this.settingData.FullScreen;
                    break;
                case SettingTag.Key:
                    attribute.Value = this.settingData.Key;
                    break;
                default:
                    break;
            }
        }
        doc.Save(GameDefinition.SettingFilePath);
    }

    public SettingData GetSettingData()
    {
        return this.settingData;
    }

    /// <summary>
    /// Get all scores
    /// </summary>
    /// <returns>All user's score list</returns>
    public List<ScoreData> GetScores()
    {
        return this.scoreList;
    }

    /// <summary>
    /// Refresh latest scores list (You have to call ScoresReader() function.)
    /// </summary>
    /// <returns>If true means successful, or unsuccessful</returns>
    public bool ScoresUpdate()
    {
        try
        {
            if (!this.fileName.Equals(string.Empty))
            {
                this.scoreList.Clear();
                this.ScoresReader(this.fileName);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            this.Ex = ex;
            return false;
        }
    }

    /// <summary>
    /// Get highest scores for numbers. (A sorted scores list.)
    /// </summary>
    /// <param name="scene">Which scene's record.</param>
    /// <param name="count">How much numbers.</param>
    /// <returns>How numbers of highest scores list</returns>
    public List<ScoreData> GetHighScores(string scene, int count)
    {
        var query =
            from score in this.scoreList
            where score.Scene.Equals(scene)
            select score;

        List<ScoreData> scores = query.ToList<ScoreData>();
        scores.Sort(
            (x, y) =>
            {
                return -Convert.ToUInt32(x.Score).CompareTo(Convert.ToUInt32(y.Score));
            });
        if (scores.Count >= count)
            return scores.GetRange(0, count);
        else
            return scores.GetRange(0, scores.Count);
    }

    /// <summary>
    /// Write score to file
    /// </summary>
    /// <param name="scoreData">A full score data.</param>
    /// <param name="filename">Which XML format file you want to write.</param>
    public void ScoresWrite(ScoreData scoreData, string filename)
    {
        XmlDocument document = new XmlDocument();

        this.VerifyFileExist(GameDefinition.ScoresFilePath);
        document.Load(filename);
        XmlNode node = document.SelectSingleNode(ScoreTag.ScoresTag);

        XmlElement newUser = document.CreateElement(ScoreTag.UserTag);
        newUser.SetAttribute(ScoreTag.ID, scoreData.ID);
        newUser.SetAttribute(ScoreTag.UserName, scoreData.UserName);
        newUser.SetAttribute(ScoreTag.Score, scoreData.Score);
        newUser.SetAttribute(ScoreTag.GameTime, scoreData.GameTime);
        newUser.SetAttribute(ScoreTag.Coppers, scoreData.Coppers);
        newUser.SetAttribute(ScoreTag.Silvers, scoreData.Silvers);
        newUser.SetAttribute(ScoreTag.Golds, scoreData.Golds);
        newUser.SetAttribute(ScoreTag.Rank, scoreData.Rank);
        newUser.SetAttribute(ScoreTag.PlayDate, scoreData.PlayDate);
        newUser.SetAttribute(ScoreTag.Scene, scoreData.Scene);
        node.AppendChild(newUser);
        document.Save(filename);
        this.scoreList.Add(scoreData);
    }

    public bool VerifyFileExist(string filename)
    {
        XmlDocument document = new XmlDocument();
        switch (filename)
        {
            case GameDefinition.SettingFilePath:
                if (!File.Exists(filename))
                {
                    XmlNode docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                    document.AppendChild(docNode);
                    XmlNode productsNode = document.CreateElement(SettingTag.SettingsTag);
                    document.AppendChild(productsNode);
                    document.Save(filename);
                    XmlNode nodee = document.SelectSingleNode(SettingTag.SettingsTag);
                    XmlElement newMachine = document.CreateElement(SettingTag.SetTag);
                    newMachine.SetAttribute(SettingTag.MachineName, Environment.GetEnvironmentVariable("COMPUTERNAME"));
                    newMachine.SetAttribute(SettingTag.Quality, "4");
                    newMachine.SetAttribute(SettingTag.Resolution, "0");
                    newMachine.SetAttribute(SettingTag.FullScreen, "False");
                    newMachine.SetAttribute(SettingTag.Key, "0");
                    nodee.AppendChild(newMachine);
                    document.Save(filename);
                    this.settingData = new SettingData(Environment.GetEnvironmentVariable("COMPUTERNAME"), "4", "0", "False", "0");
                    return false;
                }
                break;
            case GameDefinition.ScoresFilePath:
                if (!File.Exists(filename))
                {
                    XmlNode docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                    document.AppendChild(docNode);

                    XmlNode productsNode = document.CreateElement(ScoreTag.ScoresTag);
                    document.AppendChild(productsNode);

                    document.Save(filename);
                    return false;
                }
                break;
            case GameDefinition.RecordFilePath:
                if (!File.Exists(filename))
                {
                    XmlNode docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
                    document.AppendChild(docNode);
                    XmlNode node = document.CreateElement(RecordTag.Records);
                    document.AppendChild(node);
                    document.Save(filename);
                    XmlNode newNode = document.SelectSingleNode(RecordTag.Records);

                    for (int count = 1; count <= 5; count++)
                    {
                        XmlElement newUser = document.CreateElement(RecordTag.Record);
                        newUser.SetAttribute(RecordTag.RecordName, "Record " + count.ToString());
                        newUser.SetAttribute(RecordTag.Scene, "");
                        newUser.SetAttribute(RecordTag.SaveDate, "");
                        node.AppendChild(newUser);
                    }
                    document.Save(filename);
                }
                break;
            default:
                break;
        }
        return true;
    }
}

#endregion
