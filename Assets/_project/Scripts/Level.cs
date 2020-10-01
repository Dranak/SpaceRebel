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
    public float Distance;
    public Pooller Pooller { get; set; }


    void Awake()
    {
        Instance = Instance ?? this;
        Pooller = new Pooller(SizePooller, MapGenerator.gameObject);
        Distance = Mathf.Abs(Vector3.Distance(SpawnStart.transform.position, End.transform.position));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadBlock()
    {
        MapDensityGenerator loadedBlock = Pooller.GetObject().GetComponent<MapDensityGenerator>();
        loadedBlock.transform.parent = transform;
        if (loadedBlock.Volume is SphereCollider)
            loadedBlock.transform.position = new Vector3(SpawnStart.transform.position.x, SpawnStart.transform.position.y, SpawnStart.transform.position.z + (loadedBlock.Volume as SphereCollider).radius);
        else
            loadedBlock.transform.position = SpawnStart.transform.position +  Vector3.forward*20;
        loadedBlock.FillVolume();
    }
}
