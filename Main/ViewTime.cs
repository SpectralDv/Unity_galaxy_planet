using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalTime
{
    private static GlobalTime instance = null;
    public static GlobalTime SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalTime();
            }
            return instance;
        }
    }
    public float time;
}

public class ViewTime : MonoBehaviour
{
    private float time;

    void Update()
    {
        time += Time.deltaTime;
        GlobalTime.SharedInstance.time = time;
    }

}
