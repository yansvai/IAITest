﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering;

/// <summary>
/// Agent State
/// </summary>
public enum AgentState
{
    Patrol,
    GoToEnemy,
    Attack
}
public class Agent : MonoBehaviour
{

    //Public Vars
    public AgentState State;
    public static Action<Enemy> UpdateEnemyUi;
    public static Action<float, Vector3> UpdateAgentUi;
    public MeshRenderer Sphere;

    //Privet Vars
    [SerializeField] private Gun Gun;
    [SerializeField] private float AttackDistance = 20;
    [SerializeField] private float GoToEnemyDistance = 50;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField]private List<GameObject> _enemysAroundMe;

    private void OnEnable()
    {
       Enemy.EnemyDieEvent += EnemyDiedEvent;
    }

    private void OnDisable()
    {
        // ReSharper disable once DelegateSubtraction
        Enemy.EnemyDieEvent -= EnemyDiedEvent;
    }
    

    // Use this for initialization
    void Start ()
    {
        Sphere.enabled = true; //Sphere On Map
     
         _navMeshAgent = GetComponent<NavMeshAgent>();
    }
  
    // Update is called once per frame
    void Update ()
	{
	    float _speed = Vector3.Project(GetComponent<NavMeshAgent>().desiredVelocity, transform.forward).magnitude; //Get The Current Speed Of Nav Mesh
        UpdateAgentUi(_speed, transform.position);//Update The Ui Agent Every Frame
	    ActionStep();//Main Func
	}

    
    /// <summary>
    /// Cheack If Enemy In List Of Enemys Around Me Is Full If Yes Get The Closer Distance And Take An Act
    /// 
    /// </summary>
    private void ActionStep()
    {
        if (_enemysAroundMe == null)
            return;

        foreach (var enemy in _enemysAroundMe)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);// Get The Distance From Every Enemy In List


            if (distance < GoToEnemyDistance)//If In Range Of Sight Go To Enemys Position
            {
                SetState(AgentState.GoToEnemy,enemy);
            }
            if (distance < AttackDistance)
            {
                SetState(AgentState.Attack, enemy);
            }


        }
    }
    /// <summary>
    /// Whwn Enemy Is In Range Of Detectsion Add To The Enemy List
    /// </summary>
    /// <param name="detectedEnemy"></param>
    private void OnTriggerEnter(Collider detectedEnemy)
    {
        if(!detectedEnemy.GetComponent<Enemy>()) //Chack If Is Enemy
            return;

        Enemy enemy = detectedEnemy.GetComponent<Enemy>();
        if (!_enemysAroundMe.Contains(enemy.gameObject)) // if Is Alraedy In The list Ignor It
        {
            _enemysAroundMe.Add(enemy.gameObject);
            UpdateEnemyUi(enemy); //Update The Enemy Ui 
        }
    }
   
    /// <summary>
    /// GO To Enemy Event Handler
    /// </summary>
    /// <param name="enemyLocation"></param>
    private void GoToEnemy(GameObject enemyLocation)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination=(enemyLocation.transform.position);
    }


    /// <summary>
    /// AttacK Enemy Event
    /// </summary>
    /// <param name="enemy"></param>
    private void Attack(GameObject enemy)
    {
        _navMeshAgent.isStopped = true; //Stop Moveing When Attack
        Gun.Fire(enemy); //Fire The Gun
    }
    

    /// <summary>
    /// State Machine 
    /// </summary>
    /// <param name="enemyMode"></param>
    /// <param name="enemyGameObject"></param>
    private void SetState( AgentState enemyMode,GameObject enemyGameObject=null)
    {
        switch (enemyMode)
        {
            case AgentState.Patrol:
                State = AgentState.Patrol;
                break;

            case AgentState.Attack:
                Debug.Log("Attack");
                State = AgentState.Attack;
                Attack(enemyGameObject);
                break;

            case AgentState.GoToEnemy:
                try
                {
                   Debug.Log("Go To Location");
                   State = AgentState.GoToEnemy;
                   GoToEnemy(enemyGameObject);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
             
                break;

        }
    }

    /// <summary>
    /// Enemy Dided Event 
    /// </summary>
    /// <param name="enemy"></param>
    private void EnemyDiedEvent(GameObject enemy)
    {
        _enemysAroundMe.Remove(enemy); //Remove From List 
        SetState(AgentState.Patrol, null);
        
      
      
    }
}
