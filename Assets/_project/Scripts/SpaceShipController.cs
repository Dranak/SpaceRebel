using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : MonoBehaviour
{
    public event Action<float> OnSpeedUp;
    public event Action<float> OnSpeedDown;
    public event Action OnDeath;
    public event Action NextLevel;

    public int MinFov;
      public int MaxFov;
    private int _fov;

    public float MaxEnergy;
    private float _actualEnergy =0;

    public float SpeedVertical;
    public float MinForwardSpeed;
    public float _actualForwardSpeed =0f;
    public float MaxForwardSpeed;

    public float TurnSpeed;


  
    Rigidbody _rigidbody = null;
    Vector3 ScreenPos;
    private float _deltaX = 0f;
    private float _deltaY = 0f;

    [Header("Bound")]

    public float rotationMax = 45;
    public Vector3 Boundaries { get; set; }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        _fov = MinFov;
        Camera.main.fieldOfView = _fov;
        _actualForwardSpeed = MinForwardSpeed;
    }

    void Update()
    {
        _deltaX = Input.GetAxis("Horizontal");
        _deltaY = Input.GetAxis("Vertical");


        MoveSpaceship();
        RotationShip();

    }

    void LateUpdate()
    {
        //ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        //ScreenPos.x = Mathf.Clamp(ScreenPos.x, 0, Camera.main.pixelWidth);
        //ScreenPos.y = Mathf.Clamp(ScreenPos.y, 0, Camera.main.pixelHeight);
        //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(ScreenPos).x, Camera.main.ScreenToWorldPoint(ScreenPos).y, 0);

    }

    void MoveSpaceship()
    {
        transform.Translate(Vector3.forward * _actualForwardSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * (_deltaX * SpeedVertical * Time.deltaTime), Space.World);
        transform.Translate(Vector3.up * (_deltaY * SpeedVertical * Time.deltaTime), Space.World);
    }

    void RotationShip()
    {
        transform.rotation = Quaternion.Euler(rotationMax * -_deltaY, 0, rotationMax * -_deltaX);
    }

   

    private void OnTriggerEnter(Collider other)
    {
        Element element = other.GetComponent<Element>();
        if (element)
        {
            _actualEnergy += element.EnergyGain;
            float ratio = _actualEnergy ;
            Debug.Log("ratio1 " + _actualEnergy);
            UiManager.Instance.UpdateEnergy(_actualEnergy);

            if (element.EnergyGain < 0)
            {
                if (_actualEnergy < 0)
                    OnDeath.Invoke();
            }
            else
            {
                if (_actualEnergy >= MaxEnergy)
                {
                    NextLevel.Invoke();
                }
            }
            Debug.Log("ratio2 " + ratio);
            Camera.main.fieldOfView = Mathf.Lerp(MinFov, MaxFov, _actualEnergy);
            _actualForwardSpeed = Mathf.Lerp(MinForwardSpeed, MaxForwardSpeed, _actualEnergy);
            OnSpeedUp.Invoke(_actualEnergy);
        }
    }


}

