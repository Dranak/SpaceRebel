using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; set; }
    public MapDensityGenerator MapGenerator;
    public int SizePooller;
    public LevelBorder SpawnStart;
    public LevelBorder End;
    public GameObject Pool;
    public Pooller Pooller { get; set; }


    void Awake()
    {
        Instance = Instance ?? this;
        Pooller = new Pooller(SizePooller, MapGenerator.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadBlock()
    {
        MapDensityGenerator loadedBlock = Pooller.GetObject().GetComponent<MapDensityGenerator>();
        loadedBlock.transform.parent =null;
        loadedBlock.transform.SetParent(transform);
        loadedBlock.transform.position = SpawnStart.transform.position;

    }
}
