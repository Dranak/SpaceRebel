using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    public BorderType BorderType;
    // Start is called before the first frame update

    private void Start()
    {
        //If there isn't anny block, i'll create one from the borderStart
        if (BorderType == BorderType.Start && Level.Instance.Pooller.Pool.Where(p => p.activeInHierarchy).Count() < 1)
        {
            Level.Instance.LoadBlock();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        MapDensityGenerator generator = other.GetComponent<MapDensityGenerator>();
        if (generator && BorderType == BorderType.Start)
        {
            Level.Instance.LoadBlock();
        }
        else if (generator && BorderType == BorderType.End)
        {
            generator.ClearObstacles();
            Level.Instance.Pooller.ReturnToPool(generator.gameObject);
        }
    }

}
