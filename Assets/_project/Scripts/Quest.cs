using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;


public class Quest
{
    public QuestDifficulty Difficulty;
    public float Duration;
    public Package Package;
    public Planet Planet;
  

    public Quest(QuestDifficulty difficulty, float duration, Package package, Planet planet)
    {
        Difficulty = difficulty;
        Duration = duration;
        Package = package;
        Planet = planet;
    }
}