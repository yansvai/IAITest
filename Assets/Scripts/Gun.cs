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

    public void Fire(GameObject enemy)
    {
       
        //Rotate Gun Towards Enemy
        Vector3 targetDir = enemy.transform.position - transform.position;
        float step = 3 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(targetDir.x, transform.rotation.y, targetDir.z),step, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        var rounds = Rounds;//reset rounds

        //fire Bullets
        if (rounds > 0)
        {
            rounds--;
           
            var temp = Instantiate(Bullet, spawn.transform.position, spawn.transform.rotation) as GameObject;
            temp.transform.Rotate(Vector3.left * 90);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
            Destroy(temp, 1);
        }
    }
}
