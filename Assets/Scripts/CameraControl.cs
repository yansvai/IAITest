using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Camera Control And Change View Control
/// </summary>
public class CameraControl : MonoBehaviour
{
    //public Vars
    public Camera MainCamera;


    //Private Vars
    [SerializeField] private Transform _firstpivot;
    [SerializeField] private Transform _insidecarpivot;
    [SerializeField] private SmoothFollow _smoothFollow;
     private bool _isPivotChanged=false;




    // Use this for initialization
	void Start ()
	{
	    _smoothFollow = GetComponentInChildren<SmoothFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //change camera view with toggled button
    public void FlyCamera()
    {
        Transform t;
        _isPivotChanged = !_isPivotChanged;
        if (_isPivotChanged)
        {
            t = _insidecarpivot;
            _smoothFollow.enabled = false;
        }
        else
        {
            t = _firstpivot;
            _smoothFollow.enabled = true;
        }
  
        StartCoroutine(Fly(t));

    }

    private IEnumerator Fly(Transform t)
    {
        while (_isPivotChanged)
        {
            MainCamera.transform.position = t.position;
            MainCamera.transform.rotation = t.rotation;
           yield return null;
        }

    }

  
}
