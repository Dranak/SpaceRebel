using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Starfield;
    private float _zStartStarField;
    public SpaceShipController Player;
    public Level Level;


    private float _chronoQuest;
     

    private void Awake()
    {
        Instance = Instance ?? this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Player.OnSpeedUp += Player_OnSpeedUp;
        Player.OnDeath += Player_OnDeath;
        _zStartStarField = Starfield.transform.position.z;
        Level.StartSpawnBlock();
    }

    private void Player_OnDeath()
    {
        Time.timeScale = 0f;
    }

    private void Player_OnSpeedUp(float _ratio)
    {
        Debug.Log("ratio " + _ratio);
        //Level.Pooller.Pool.ToList().ForEach(mg => mg.GetComponent<MapDensityGenerator>().SetSpeed(_ratio));
        Starfield.transform.position = new Vector3(Starfield.transform.position.x, Starfield.transform.position.y, Mathf.Lerp(_zStartStarField, 600, _ratio));
    }

    // Update is called once per frame
    void Update()
    {
        TimeRun();
    }


    void TimeRun()
    {
        _chronoQuest += Time.deltaTime;
       
        UiManager.Instance.UpdateChrono(_chronoQuest);

    }
}
