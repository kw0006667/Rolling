using UnityEngine;
using System.Collections;

public class GameDefinition
{

    #region Public Properties

    public enum Tag
    {
        Player = 1,
        MainCamera = 2,
    };

    #endregion

    #region private const string

    private const string Player = "Player";
    private const string MainCamera = "MainCamera";

    #endregion

    /// <summary>
    /// Get application tag name that you have setted.
    /// </summary>
    /// <param name="tag">Tag</param>
    /// <returns>Tag string name</returns>
    public static string GetTagName(Tag tag)
    {
        switch (tag)
        {
            case Tag.Player:
                return GameDefinition.Player;
            case Tag.MainCamera:
                return GameDefinition.MainCamera;
            default:
                return null;
        }
    }
}
