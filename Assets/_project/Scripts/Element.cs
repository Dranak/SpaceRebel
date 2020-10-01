using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Element : MonoBehaviour
{
    public int SizePooller = 5;
    public int ScaleMax = 3;
    public float TimeWaitToScale = 0.5f;
    private float _distanceFromStart;
    private float _distanceWait = 0f;

    private float _chronoWaitToScale = 0f;

    private void Start()
    {
        _distanceWait = 0f;
        _distanceFromStart = 0f;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        //if (TimeToScale())
        //{
            _distanceFromStart = Vector3.Distance(Level.Instance.SpawnStart.transform.position, transform.position);
            float percentage = Mathf.Max((_distanceFromStart / Level.Instance.Distance), 0f);
            transform.localScale = Vector3.one * Mathf.Lerp(0, ScaleMax, percentage);
        //}

    }


    bool TimeToScale()
    {
        if (_chronoWaitToScale <= TimeWaitToScale)
        {
            _chronoWaitToScale += Time.deltaTime;
            return false;
        }
        else
        {
            if (_distanceWait == 0f)
            {
                _distanceWait = Vector3.Distance(Level.Instance.SpawnStart.transform.position, transform.position);
            }
            return true;
        }
    }


    private void OnEnable()
    {
        _distanceWait = 0f;
        _distanceFromStart = 0f;
        transform.localScale = Vector3.zero;
    }

    private void OnDisable()
    {

    }
}