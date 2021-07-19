using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{

    /*
        1. Constrain Camera snap position to player position
        2. 5 child objects. 4 are cardinal directions, 5 is Camera Parent
        3. make camera use quaternion.lookat(Player)
        4. Snap CamHolder to position, use Animation curves to ease in and out of movement


    */
    [SerializeField]
    GameObject Player = null;   //Get Player for Constraining

    GameObject CamHolder = null;
    GameObject GameCam = null;

    [SerializeField]
    Transform NorthPoint = null, EastPoint = null, SouthPoint = null, WestPoint = null;  //Get Cardinal points for camHolder to snap to

    List<Transform> CardinalPoints = new List<Transform>();     //Turn cardinalPoints into List


    void Start()
    {
        CamHolder = gameObject;
        GameCam = GetComponentInChildren<Camera>().gameObject;

        //Assign Cardinal Points to the list 
        #region cardinalpoint assignment 
        CardinalPoints.Add(NorthPoint);
        CardinalPoints.Add(SouthPoint);
        CardinalPoints.Add(EastPoint);
        CardinalPoints.Add(WestPoint);
        #endregion   

    }

    // Update is called once per frame
    void Update()
    {
        GameCam.transform.LookAt(Player.transform.position);

        SetCamPos();
    }


    void SetCamPos()
    {

    }
}
