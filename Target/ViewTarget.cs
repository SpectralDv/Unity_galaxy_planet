using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModelTarget
{
    public StateTarget stateTarget;
    public string state;
    public ModelTarget()
    {
        state = "empty";
        stateTarget = new StateTarget();
    }
}


public class ViewTarget : MonoBehaviour
{
    private ModelTarget mTarget;
    private GameObject targetGO;

    void Start()
    {
        
    }

    void Update()
    {
        //чтобы здесь в таргет получить объект от камеры
        //необходимо создать синглтон в который камера будет передавать объект для таргета
    }

    private void SelectTarget()
    {
        if(targetGO==null)
        {
            targetGO.transform.GetComponent<Outline>().enabled = true;
            targetGO = null;
            GlobalStateTarget.SharedInstance.UpdateState("SELECT");
        }
    }

    private void ResetTarget()
    {
        if(targetGO!=null)
        {
            targetGO.transform.GetComponent<Outline>().enabled = false;
            targetGO = null;
            GlobalStateTarget.SharedInstance.UpdateState("RESET");
        }
    }


}
