using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using DG.Tweening;
using Tabtale.TTPlugins;

public class Movement : MonoBehaviour
{
    public PlayerState playerState;

//    [SerializeField] private GameObject _canvas;

//    [SerializeField] private Transform camTransform;
    

    public enum PlayerState
    {
        Stop,
        Move
    }
    //Touch Settings
    [SerializeField] bool isTouching;
    float touchPosX;
    Vector3 direction;
    [SerializeField, ReadOnly] private float movementSpeed;
    [SerializeField, ReadOnly] private float controlSpeed;

    private void Awake()
    {
        // Initialize CLIK Plugin
        TTPCore.Setup();
        // Your code here
    }
    void Start()
    {
        playerState = PlayerState.Move;
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {

        if (playerState == PlayerState.Move)
        {
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (isTouching)
        {
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        }

        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
    }

    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

   

   
}