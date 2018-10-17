using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : MonoBehaviour
{
    //private Vars 
    [SerializeField] Transform UIParent;


    //Public Vars
    public GameObject UiPrefab;
     public Text MySpeed;
    public Text MyLocation;


    // Use this for initialization
    private void OnEnable()
    {
        Agent.UpdateAgentUi += UpdateAgentUi;
        Agent.UpdateEnemyUi += UpdateEnemyUI;
    }

    private void OnDisable()
    {
        Agent.UpdateAgentUi -= UpdateAgentUi;
        Agent.UpdateEnemyUi -= UpdateEnemyUI;
    }
    /// <summary>
    /// Update Enemy UI By Instatiate New UI Elemnt
    /// </summary>
    /// <param name="enemy"></param>
    public void UpdateEnemyUI(Enemy enemy)
    {
        GameObject temp = Instantiate(UiPrefab, UIParent);
        UIElement uielement = temp.GetComponent<UIElement>();
        StartCoroutine(EnemyUi(enemy, uielement));
    }
    /// <summary>
    /// Updte The Speed And Loaction For Each Enemy Every Frame
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="uielement"></param>
    /// <returns></returns>
    IEnumerator EnemyUi(Enemy enemy,UIElement uielement)
    {
        while (enemy)
        {
            uielement.Location.text = "Location: " + enemy.Location;
            uielement.Speed.text = "Speed: " + enemy.Speed;
            yield return null;
        }
        uielement.EnemyDestroyedPanel.SetActive(true); //When Enemy Dided Turn On Message
    }
 
    /// <summary>
    /// Update Agent Ui
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="location"></param>
    public void UpdateAgentUi(float speed, Vector3 location)
    {
       MySpeed.text = "Speed: "+ speed;
        MyLocation.text ="Location: " + location;
    }
}
