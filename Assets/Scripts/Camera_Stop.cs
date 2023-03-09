using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Stop : MonoBehaviour
{
    [SerializeField] private Transform player; 
    private Vector3 pos;
    void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }

    void FixedUpdate()
    {
       pos = player.position;
       pos.z = -10f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}
