using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _playerPos;
    
    [Header("Movement Settings")] 
    [Range(0.1f,25.0f)]
    public float movementSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;

    private void Start()
    {
        movementSpeed = UnityEngine.Random.Range(2f,maxSpeed);
        byte colorIntensity = (byte)(255-(255*movementSpeed/maxSpeed));
        Debug.Log($"ms: {movementSpeed}, color intensity {colorIntensity}");
        GetComponent<Renderer>().material.color = new Color32(255,colorIntensity,colorIntensity,255);
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, movementSpeed*Time.fixedDeltaTime);
    }
}
