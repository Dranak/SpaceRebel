using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "Planet", menuName = "Planet", order = 0)]
public class Planet : ScriptableObject
{
    public QuestDifficulty Difficulty;
    public Material PlanetPicture;
    public GameObject PlanetModel;
  
}