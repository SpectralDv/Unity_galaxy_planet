using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject : MonoBehaviour
{
    public GameObject planet;
    private string state = "show";

    void Start()
    {
        //planet = transform.gameObject;
        transform.position = new Vector3(0,0,0);
        this.HidePlanet();
    }

    void Update()
    {
        this.StatePlanet();
    }

    private void StatePlanet()
    {
        //if(GlobalState.SharedInstance.state==eGLOBALSTATE.planet.ToString())
        if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.planet.ToString())
        {
            this.ShowPlanet();
        }
        //if(GlobalState.SharedInstance.state!=eGALAXYSTATE.planet.ToString())
        if(GlobalStateCamera.SharedInstance.state!=eGALAXYSTATE.planet.ToString())
        {
            this.HidePlanet();
        }
    }
    private void ShowPlanet()
    {
        if(state == "hide")
        {
            planet.SetActive(true);
            state = "show";
            //Debug.Log("Planet show");
        }
    }
    private void HidePlanet()
    {
        if(state == "show")
        {
            planet.SetActive(false);
            state = "hide";
            //Debug.Log("Planet hide");
        }
    }
}
