using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public GameObject Bullet;
    public int Rounds = 50;
    public Transform spawn;
    public int BulletSpeed=250;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	   
	}

    public void Fire()
    {
       var rounds = Rounds;
        if (rounds > 0)
        {
            rounds--;
           
            var temp = Instantiate(Bullet, spawn.transform.position, spawn.transform.rotation) as GameObject;
            temp.transform.Rotate(Vector3.left * 90);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
            Destroy(temp, 3);
        }
    }
}
