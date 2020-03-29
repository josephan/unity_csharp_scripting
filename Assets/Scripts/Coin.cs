﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 1.0f;

    [SerializeField]
    private float floatSpeed = 0.5f;

    [SerializeField]
    private float movementDistance = 0.5f;

    private float startingY;
    private bool isMovingUp;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        transform.Rotate(transform.up, Random.Range(0f, 360f));
    }

    void Update()
    {
        Spin();
        Float();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pickup();
        }
    }

    void Pickup()
    {
        GameManager.Instance.NumCoins++;
        Destroy(gameObject);
    }


    private void Spin()
    {
        transform.Rotate(transform.up, 360 * rotateSpeed * Time.deltaTime);
    }

    private void Float()
    {
        float newY = transform.position.y + (isMovingUp ? 1 : -1) * 2 * movementDistance * floatSpeed * Time.deltaTime;

        if (newY > startingY + movementDistance)
        {
            newY = startingY + movementDistance;
            isMovingUp = false;
        }
        else if (newY < startingY)
        {
            newY = startingY;
            isMovingUp = true;
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
