using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class MapDensityGenerator : MonoBehaviour
{


    public int ElementSpacing = 3; // The spacing between element placements. Basically grid size.
    [MinMaxSlider(50, 100)]
    public Vector2Int RangeNumberOfElement ;
    public int NumberOfElement { get; set; }
    public Collider Volume;
    public List<Element> Elements;
    public Dictionary<string, Pooller> Poolers;
    [MinMaxSlider(1, 10)]
    public Vector2Int RangeOffsetX;
    [MinMaxSlider(1, 10)]
    public Vector2Int RangeOffsetY;
    [MinMaxSlider(1, 10)]
    public Vector2Int RangeOffsetZ;

    public float Speed;

    public void Awake()
    {
        Poolers = new Dictionary<string, Pooller>();
        Elements.ForEach(el => Poolers.Add(el.name, new Pooller(el.SizePooller, el.gameObject)));
    }
   
    private void Update()
    {
        if (isActiveAndEnabled)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * -Speed * Time.deltaTime);
    }

    public void ClearObstacles()
    {
        List<Element> gos = new List<Element>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            gos.Add(transform.GetChild(i).GetComponent<Element>());
        }
        string temp =gos[0].name;
        string temp2 = gos[0].name.Split('-')[0];
        gos.ForEach(go => Poolers[temp2].ReturnToPool(go.gameObject));
    }

    public void FillVolume()
    {
        List<Vector3> points = GetPoints();
        foreach(Vector3 point in points )
        {
            int index = Random.Range(0, Elements.Count);
            Element element = Elements[index];
            GameObject tempObject = Poolers[element.name].GetObject();
            // Add random elements to element placement.
            Vector3 position = point;
            Vector3 rotation = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
          
            tempObject.transform.parent = transform;
            tempObject.transform.position = position;
            tempObject.transform.eulerAngles = rotation;
        }
    }

    public void SetUpElement()
    {
        for (int x = (int)Volume.bounds.min.x; x < (int)Volume.bounds.max.x; x += (int)Random.Range(RangeOffsetX.x, RangeOffsetX.y))
        {
            for (int y = (int)Volume.bounds.min.y; y < (int)Volume.bounds.max.y; y += (int)Random.Range(RangeOffsetY.x, RangeOffsetY.y))
            {
                for (int z = (int)Volume.bounds.min.z; z < (int)Volume.bounds.max.z; z += (int)Random.Range(RangeOffsetZ.x, RangeOffsetZ.y))
                {  
                    int index = Random.Range(0, Elements.Count);
                    // Get the current element.
                    Element element = Elements[index];
                    // Check if the element can be placed.
                   
                        GameObject tempObject = Poolers[element.name].GetObject();
                        // Add random elements to element placement.
                        Vector3 position = new Vector3(x, y, z);
                        Vector3 rotation = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
                        //Vector3 scale = Vector3.one * Random.Range(0.75f, 1.25f);
                        tempObject.transform.parent = transform;
                        tempObject.transform.position = position;
                        tempObject.transform.eulerAngles = rotation;
                        break;

                    
                }
            }
        }
    }

    List<Vector3> GetPoints()
    {
        List<Vector3> points = new List<Vector3>();
        Physics.SyncTransforms();
        NumberOfElement = Random.Range(RangeNumberOfElement.x, RangeNumberOfElement.x);
        if(Volume is  BoxCollider)
        {
            for (int index = 0; index < NumberOfElement; ++index)
            {
                points.Add(new Vector3(Random.Range(Volume.bounds.min.x, Volume.bounds.max.x), Random.Range(Volume.bounds.min.y, Volume.bounds.max.y), Random.Range(Volume.bounds.min.z, Volume.bounds.max.z)));
            }
        }
        else if( Volume is CapsuleCollider)
        {
            for (int index = 0; index < NumberOfElement; ++index)
            {
                points.Add(new Vector3(Random.insideUnitCircle.x * (Volume as CapsuleCollider).radius, Random.insideUnitCircle.y * (Volume as CapsuleCollider).radius, Random.Range(Level.Instance.SpawnStart.transform.position.z, Level.Instance.SpawnStart.transform.position.z+(Volume as CapsuleCollider).height)));
            }
        }
        else if (Volume is SphereCollider)
        {
            Debug.Log("BITE");
            for (int index = 0; index < NumberOfElement; ++index)
            {
                Vector3 newPoint = Random.insideUnitSphere * (Volume as SphereCollider).radius;
                //if (points.Contains(newPoint))
                //{
                //    newPoint.x += Random.Range(RangeOffsetX.x, RangeOffsetX.y);
                //    newPoint.y += Random.Range(RangeOffsetY.x, RangeOffsetY.y);
                //    newPoint.z += Random.Range(RangeOffsetZ.x, RangeOffsetZ.y);
                //}
                points.Add(newPoint);
            }
        }
        return points;
    }

   




}


