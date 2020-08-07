using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class MapDensityGenerator : MonoBehaviour
{


    public int ElementSpacing = 3; // The spacing between element placements. Basically grid size.

    public Collider Volume;
    public List<Element> Elements;
    public Dictionary<string, Pooller> Poolers;
    [MinMaxSlider(0, 10)]
    public Vector2Int RangeOffsetX;
    [MinMaxSlider(0, 10)]
    public Vector2Int RangeOffsetY;
    [MinMaxSlider(0, 10)]
    public Vector2Int RangeOffsetZ;


    public float Speed;

    public void Awake()
    {
        Poolers = new Dictionary<string, Pooller>();
        Elements.ForEach(el => Poolers.Add(el.name, new Pooller(el.SizePooller, el.gameObject)));
    }

    //private void OnEnable()
    //{
    //    if(Poolers !=  null)
    //    SetUpElement();
    //}

    //private void OnDisable()
    //{
    //    //ClearObstacles();

    //}

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



    public void SetUpElement()
    {
        Physics.SyncTransforms();

        for (int x = (int)Volume.bounds.min.x; x < (int)Volume.bounds.max.x; x += (int)Random.Range(RangeOffsetX.x, RangeOffsetX.y))
        {
            for (int y = (int)Volume.bounds.min.y; y < (int)Volume.bounds.max.y; y += (int)Random.Range(RangeOffsetY.x, RangeOffsetY.y))
            {
                for (int z = (int)Volume.bounds.min.z; z < (int)Volume.bounds.max.z; z += (int)Random.Range(RangeOffsetZ.x, RangeOffsetZ.y))
                {
                    // For each position, loop through each element...
                    //for (int i = 0; i < Elements.Count; i++)
                    //{
                    int index = Random.Range(0, Elements.Count);
                    // Get the current element.
                    Element element = Elements[index];
                    // Check if the element can be placed.
                    if (element.CanPlace())
                    {
                        GameObject tempObject = Poolers[element.name].GetObject();
                        // Add random elements to element placement.
                        Vector3 position = new Vector3(x, y, z);
                        Vector3 rotation = new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
                        Vector3 scale = Vector3.one * Random.Range(0.75f, 1.25f);
                        tempObject.transform.parent = transform;
                        tempObject.transform.position = position;
                        tempObject.transform.eulerAngles = rotation;
                        break;

                    }
                }
            }

        }
    }
}


