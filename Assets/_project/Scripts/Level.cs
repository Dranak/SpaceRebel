using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; set; }
    public Queue<MapDensityGenerator> LoadedBlock { get; set; }
    public List<MapDensityGenerator> ListBlocks;
    public Dictionary<string, Pooller> PoolersBlocks { get; set; }
    public int SizePooller;
    public GameObject Pool;
    public float Distance;
    public Vector3 spawnOrigin;
    public int blockToSpawn = 10;
   

    void Awake()
    {
        Instance = Instance ?? this;
        PoolersBlocks = new Dictionary<string, Pooller>();
        foreach (MapDensityGenerator block in ListBlocks)
        {
            PoolersBlocks.Add(block.name.Split('-')[0],new Pooller(SizePooller, block.gameObject));
        }
        
        LoadedBlock = new Queue<MapDensityGenerator>();
        //Distance = Mathf.Abs(Vector3.Distance(SpawnStart.transform.position, End.transform.position));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadBlock()
    {
        string randomBlockName = ListBlocks[Random.Range(0, ListBlocks.Count)].name.Split('-')[0];
        MapDensityGenerator loadedBlock = PoolersBlocks[randomBlockName].GetObject().GetComponent<MapDensityGenerator>();
        LoadedBlock.Enqueue(loadedBlock);
        loadedBlock.transform.parent = transform;
        loadedBlock.transform.position = GameManager.Instance.Player.transform.forward*loadedBlock.Volume.bounds.size.z*LoadedBlock.Count;
        loadedBlock.FillVolume();
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }


    public void StartSpawnBlock()
    {

        for(int i=0; i < blockToSpawn;++i)
        {
            LoadBlock();
        }

    }

}
