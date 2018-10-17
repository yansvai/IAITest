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


    // Use this for initialization
    void Start ()
    {
        //Turn On Sphere On Map
        Sphere.enabled=true;
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
        EnemyDieEvent(gameObject);//Run Event In Agent
        Destroy(gameObject);
    }
}
