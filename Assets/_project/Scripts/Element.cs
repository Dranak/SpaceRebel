using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Element : MonoBehaviour
{
    public int SizePooller = 5;
    public int ScaleMax = 3;
    private float _distanceFromStart;
    public float EnergyGain = -0.1f;

    private void Start()
    {
        //_distanceFromStart = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        //transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        //float _distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        //float percentage = (_distance / _distanceFromStart);
        //transform.localScale = Vector3.one * Mathf.Lerp(0, ScaleMax, percentage);
    }



    private void OnEnable()
    {
      
        //_distanceFromStart = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        //transform.localScale = Vector3.zero;
    }

    private void OnDisable()
    {

    }
}