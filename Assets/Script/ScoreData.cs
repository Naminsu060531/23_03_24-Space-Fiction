using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{


    public string Name;

    public int Score;

    public int Time;

    public ScoreData(string name, int score, int time)
    {
        this.Name = name;
        this.Score = score;
        this.Time = time;
    }

}
