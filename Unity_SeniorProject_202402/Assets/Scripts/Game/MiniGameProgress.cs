using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Progress/MiniGameProgress", fileName = "MiniGameProgress" )]
public class MiniGameProgress : ScriptableObject
{
    public List<MiniGameData> miniGames = new List<MiniGameData>();
}

[System.Serializable]
public class MiniGameData
{
    public string miniGameName;
    public bool isCompleted;
}
