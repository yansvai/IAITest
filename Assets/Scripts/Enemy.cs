using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float Speed;
    public Vector3 Location;
    public static Action<GameObject> EnemyDieEvent;
    public MeshRenderer Sphere;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;


    // Use this for initialization
    void Start ()
    {
        //Turn On Sphere On Map
        Sphere.enabled=true;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

	    Speed = Vector3.Project(gameObject.GetComponent<NavMeshAgent>().desiredVelocity,transform.forward).magnitude;
	    Location = transform.position;
	}
    /// <summary>
    /// Die Event
    /// </summary>
    public void Die()
    {

        _navMeshAgent.isStopped = true;
        _animator.enabled = false;
        if (EnemyDieEvent != null)
        {
            EnemyDieEvent(gameObject); //Run Event In Agent
        }
        else
        {
            Debug.LogError("EnemyDieEvent =null");
        }

        Destroy(gameObject,1.5f);


    }
}
