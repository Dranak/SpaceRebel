using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    public BorderType BorderType;

    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<SpaceShipController>())
        {
            MapDensityGenerator generator = Level.Instance.LoadedBlock.Dequeue();
            generator.ClearObstacles();
            Level.Instance.PoolersBlocks[generator.name.Split('-')[0]].ReturnToPool(generator.gameObject);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    MapDensityGenerator generator = other.GetComponent<MapDensityGenerator>();
    //    if (generator && BorderType == BorderType.Start)
    //    {
    //        Level.Instance.LoadBlock();
    //    }
    //    else if (generator && BorderType == BorderType.End)
    //    {
    //        generator.ClearObstacles();
    //        Level.Instance.Pooller.ReturnToPool(generator.gameObject);
    //    }
    //}

}
