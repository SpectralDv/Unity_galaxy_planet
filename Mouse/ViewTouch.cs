using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//1.так как состояния мыши и состояния сенсора существуют одновременно
//во время скрола сенсора, работает движение от удержания мыши
//2.надо сделать общее состояние управления камерой 
//чтобы перебивать удержание маши скором сенсора

public class ModelTouch
{
    public StateTouch sTouch;
    public ModelTouch()
    {
        sTouch = new StateTouch();
        GlobalStateTouch.SharedInstance.AddState(sTouch);
    }
}


public class ViewTouch : MonoBehaviour
{
    private ModelTouch mTouch;

    public GameObject PanelDebug;
    private Text textDebug;

    void Start()
    {
        mTouch = new ModelTouch();

        textDebug = PanelDebug.transform.Find("TextDebug").GetComponent<Text>();
    }

    void Update()
    {
        StateTouch();
    }

    private void StateTouch()
    {
        switch (GlobalStateTouch.SharedInstance.state)
        {
            case "idle":
                if(Input.GetMouseButtonDown(0))
                {
                    GlobalStateTouch.SharedInstance.UpdateState("EVENTPRESS");
                    textDebug.text = "ViewTouch: "+GlobalStateTouch.SharedInstance.state;
                    //Debug.Log("");
                }
                if(Input.touches.Length == 2)
                {
                    GlobalStateTouch.SharedInstance.UpdateState("EVENTSCROLL");
                    textDebug.text = "ViewTouch: "+GlobalStateTouch.SharedInstance.state;
                    //Debug.Log("");
                }
                break;
            case "press":
                if(Input.touches.Length == 2)
                {
                    GlobalStateTouch.SharedInstance.UpdateState("EVENTSCROLL");
                    textDebug.text = "ViewTouch: "+GlobalStateTouch.SharedInstance.state;
                    //Debug.Log("");
                }
                if(!Input.GetMouseButton(0))
                {
                    GlobalStateTouch.SharedInstance.UpdateState("EVENTIDLE");
                    textDebug.text = "ViewTouch: "+GlobalStateTouch.SharedInstance.state;
                    //Debug.Log("");
                }
                break;
            case "scroll":
                if(!Input.GetMouseButton(0))
                {
                    GlobalStateTouch.SharedInstance.UpdateState("EVENTIDLE");
                    textDebug.text = "ViewTouch: "+GlobalStateTouch.SharedInstance.state;
                    //Debug.Log("");
                }
                break;
            default:
                break;
        }
    }
}
