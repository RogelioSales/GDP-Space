﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 10f;
    [SerializeField]
    private float bounce = 4f;
    private float xMovement;
    private float zMovement;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;	
	}
	// Update is called once per frame
	void Update ()
    {             
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        xMovement = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        zMovement = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.Translate(xMovement, bounce, zMovement);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (gameObject.CompareTag("Wall"))
    //    {
    //        SceneManager.LoadScene("Lv2");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Wall"))
        {
            SceneManager.LoadScene("Lv2");
        }
    }
}
