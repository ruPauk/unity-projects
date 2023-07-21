using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float normalSpeed = 25f;
    bool canMove = true;

    Rigidbody2D rgb2D;
    SurfaceEffector2D surfaceEffector2D;
    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            BoostPlayer();
        }
    }

    public void DisableController()
    {
        canMove = false;
    }

    void BoostPlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rgb2D.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rgb2D.AddTorque(-torqueAmount);
        }
    }
}
