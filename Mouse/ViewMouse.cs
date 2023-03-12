using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ModelMouse
{
    public StateMouseLKB sMouseLKB;
    public StateMouseScroll sMouseScroll;
    public ModelMouse()
    {
        sMouseLKB = new StateMouseLKB();
        sMouseScroll = new StateMouseScroll();
        GlobalStateMouseLKB.SharedInstance.AddState(sMouseLKB);
        GlobalStateMouseScroll.SharedInstance.AddState(sMouseScroll);
    }
}


public class ViewMouse : MonoBehaviour
{
    private ModelMouse mMouse;

    public GameObject PanelDebug;
    private Text textDebug;

    void Start()
    {
        mMouse = new ModelMouse();

        textDebug = PanelDebug.transform.Find("TextDebug").GetComponent<Text>();
    }

    void Update()
    {
        StateMouseLKB();
    }

    //состояние ЛКМ
    private void StateMouseLKB()
    {
        switch (GlobalStateMouseLKB.SharedInstance.state)
        {
            case "idle":
                if(Input.GetMouseButtonDown(0))
                {
                    GlobalStateMouseLKB.SharedInstance.UpdateState("EVENTPRESS");
                    textDebug.text = "ViewMouse: "+GlobalStateMouseLKB.SharedInstance.state;
                    //Debug.Log("press");
                }
                break;
            case "press":
                if(Input.GetMouseButton(0))
                {
                    GlobalStateMouseLKB.SharedInstance.UpdateState("EVENTHOLD");
                    textDebug.text = "ViewMouse: "+GlobalStateMouseLKB.SharedInstance.state;
                    //Debug.Log("hold");
                }
                break;
            case "hold":
                if(!Input.GetMouseButton(0))
                {
                    GlobalStateMouseLKB.SharedInstance.UpdateState("EVENTRELEASE");
                    textDebug.text = "ViewMouse: "+GlobalStateMouseLKB.SharedInstance.state;
                    //Debug.Log("release");
                }
                break;
            case "release":
                if(!Input.GetMouseButton(0)) 
                {
                    GlobalStateMouseLKB.SharedInstance.UpdateState("EVENTIDLE");
                    textDebug.text = "ViewMouse: "+GlobalStateMouseLKB.SharedInstance.state;
                    //Debug.Log("idle");
                }
                break;
            default:
                break;
        }
    }
}
