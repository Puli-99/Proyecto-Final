using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    Testing test;
    NavMeshAgent agent;
    void Awake()
    { 
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        test = GetComponent<Testing>();
        if (GameManager.Instance.returnPosition != Vector3.zero)
        {
            transform.position = GameManager.Instance.returnPosition;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Move();
        }
        if (Input.GetKey(KeyCode.S))
        {
            agent.ResetPath();
        }
    }
    void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit);
        if (isHit)
        {
            Debug.DrawRay(hit.point, agent.destination, Color.red);
            agent.destination = hit.point;
        }        
    }    
}