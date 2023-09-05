using System.Collections.Generic;

[System.Serializable]
public class RecordResponse
{
    //public string username;
    public string date;
    public List<Game> games;
}

[System.Serializable]
public class Game
{
    public string game;
    public int level;
    public int score;
}