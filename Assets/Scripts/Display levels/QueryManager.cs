using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;

public class QueryManager : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] GameObject windowsParent;

    //Objects
    [SerializeField] Toggle trap;
    [SerializeField] Toggle tree;
    [SerializeField] Toggle teleport;


    //Night level?
    [SerializeField] TMP_Dropdown nightLevel;


    //Turns limits
    [SerializeField] Toggle minTour;
    [SerializeField] Text minNb;

    [SerializeField] Toggle maxTour;
    [SerializeField] Text maxNb;


    //Date limits
    [SerializeField] Toggle from;
    [SerializeField] Toggle to;

    public void CollectUIData()
    {
        string data = "";

        if (trap.isOn)     data += " Trap ";
        if(tree.isOn)      data += " Tree ";
        if (teleport.isOn) data += " Teleport ";

        if (nightLevel.value == 1)      data += " nightLevel ";
        else if (nightLevel.value == 2) data += " lightLevel ";

        if (minTour.isOn) data += " minTour = " + minNb.text;
        if (maxTour.isOn) data += " maxTour = " + maxNb.text;

        if (from.isOn) data += " from " + " date";
        if (to.isOn)   data += " to " + " date";

        Debug.Log(data);

       //CreateLevelView();
    }

    //public void CreateLevelView()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Instantiate(window, windowsParent.transform);
    //    }
    //}
}
