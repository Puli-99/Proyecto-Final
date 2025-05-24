using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    void Awake()
    { 
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Move();
        }
    }
    void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit);
        if (isHit)
        {
            agent.destination = hit.point;
        }
    }    
}