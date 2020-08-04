using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooller
{

    public GameObject Prefab;

    public Queue<GameObject> Pool { get; set; } = new Queue<GameObject>();
    private int  MaxOverride;

    public int PoolStartSize = 5;


    public Pooller(int size, GameObject prefab)
    {
        PoolStartSize = size;
        MaxOverride = size * 2;
        Prefab = prefab;
        for (int i = 0; i < PoolStartSize; i++)
        {
            GameObject critter = GameObject.Instantiate(Prefab,Vector3.zero, Quaternion.identity, Level.Instance.Pool.transform);
            critter.name = prefab.name + "-" + i;
             
            Pool.Enqueue(critter);
            critter.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        if (Pool.Count > 0)
        {
            GameObject _tempObject = Pool.Dequeue();
            _tempObject.SetActive(true);
            return _tempObject;
        }
        else if (Pool.Count < MaxOverride)
        {
            GameObject _tempObject = GameObject.Instantiate(Prefab);
            return _tempObject;
        }

        return null;
    }

    public void ReturnToPool(GameObject unpool)
    {
        Pool.Enqueue(unpool);
        unpool.transform.position = Vector3.zero;
        unpool.transform.parent = null;
        unpool.transform.SetParent(Level.Instance.Pool.transform);
        unpool.SetActive(false);
    }
}