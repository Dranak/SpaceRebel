using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : MonoBehaviour
{

    public float Speed;
    public float TurnSpeed;
    private float _chronoRotate = 0f;
    public StateMove State { get; set; } = StateMove.Straight;
    private float _sign = 0;
    Rigidbody _rigidbody = null;
    Vector3 ScreenPos;
    private float _deltaX = 0f;
    private float _deltaY = 0f;

    [Header("Bound")]
    //Les valeurs max absolue qu'ateindra le controller.
    public float rotationMax = 45;
    public Vector3 Boundaries { get; set; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

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
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        ScreenPos.x = Mathf.Clamp(ScreenPos.x, 0, Camera.main.pixelWidth);
        ScreenPos.y = Mathf.Clamp(ScreenPos.y, 0, Camera.main.pixelHeight);
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(ScreenPos).x, Camera.main.ScreenToWorldPoint(ScreenPos).y, 0);

    }


    void MoveSpaceship()
    {
        transform.Translate(Vector3.right * (_deltaX * Speed * Time.deltaTime), Space.World);
        transform.Translate(Vector3.up * (_deltaY * Speed * Time.deltaTime), Space.World);
    }

    void RotationShip()
    {
        transform.rotation = Quaternion.Euler(rotationMax * -_deltaY, 0, rotationMax * -_deltaX);
    }



}

