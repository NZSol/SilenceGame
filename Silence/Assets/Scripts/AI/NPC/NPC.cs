using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    GameObject[] patrolPoint;
    [SerializeField]
    GameObject PlayerChar;

    NavMeshAgent agent = null;
    FiniteStateMachine machine;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        machine = GetComponent<FiniteStateMachine>();
    }

    public void Start()
    {
            
    }

    public void Update()
    {
            
    }

    public GameObject[] patrolPoints
    {
        get
        {
            return patrolPoint;
        }
    }

    public GameObject player
    {
        get
        {
            return PlayerChar;
        }
    }

}