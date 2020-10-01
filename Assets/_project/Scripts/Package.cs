using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "Package", menuName = "Package", order = 0)]
public  class Package : ScriptableObject
{
    public QuestDifficulty Difficulty;
    [MinMaxSlider(1000, 10000)]
    public Vector2Int MoneyGain;
    public float Life;
    public float Damage;
}