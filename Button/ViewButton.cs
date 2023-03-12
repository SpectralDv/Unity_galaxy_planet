using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonReturn : IButton
{
    public ButtonReturn(){}
    public void PressButton()
    {
        GlobalStateCamera.SharedInstance.UpdateState("RETURN");
    }
}

public class ButtonSelect : IButton
{
    public ButtonSelect(){}
    public void PressButton()
    {
        GlobalStateCamera.SharedInstance.UpdateState("SELECT");
    }
}

public class ButtonCancle : IButton
{
    public ButtonCancle(){}
    public void PressButton()
    {
        //GlobalStateCamera.SharedInstance.UpdateState("SELECT");
    }
}

public class ViewButton : MonoBehaviour
{
    private ButtonSelect buttonSelect;
    private ButtonReturn buttonReturn;
    private ButtonCancle buttonCancle;
    private GameObject PanelInfo;
    private Text textDebug;
    private Text textName;
    private string state;
    private string stateTarget;
    
    void Start()
    {
        buttonReturn = new ButtonReturn();
        buttonSelect = new ButtonSelect();
        buttonCancle = new ButtonCancle();
        PanelInfo = transform.Find("Main/PanelInfo").gameObject;
        textName = PanelInfo.transform.Find("TextName").GetComponent<Text>();
        stateTarget = "";
    }

    void Update()
    {
        StatePanel();
    }

    ///////////////////////////////////////////////////////
    public void ButtonSelect()
    {
        Debug.Log("ButtonSelect");
        buttonSelect.PressButton();
    }
    public void ButtonReturn()
    {
        buttonReturn.PressButton();
        //HidePanelInfo();
    }
    public void ButtonInfoCancle()
    {
        HidePanelInfo();
        stateTarget = "";
        GlobalTarget.SharedInstance.name = "";
        GlobalStateTarget.SharedInstance.UpdateState("RESET");
    }

    ///////////////////////////////////////////////////////
    private void StatePanel()
    {
        if(GlobalTarget.SharedInstance.name!="" && stateTarget=="")
        {
            stateTarget = GlobalTarget.SharedInstance.name;
            ShowPanelInfo();
        }
        //if(GlobalTargetGO.SharedInstance.targetGO==null && stateTarget!="")
        if(GlobalTarget.SharedInstance.name=="" && stateTarget!="")
        {
            stateTarget = "";
            HidePanelInfo();
        }
    }

    private void ShowPanelInfo()
    {
        if(PanelInfo.activeSelf==false)
        {
            textName.text = GlobalTarget.SharedInstance.name;
            PanelInfo.SetActive(true);
        }
    }
    private void HidePanelInfo()
    {
        if(PanelInfo.activeSelf==true)
        {
            PanelInfo.SetActive(false);
        }
    }

    /*
    private int PrintPanelDebug()
    {
        if(GlobalState.SharedInstance.state!=null)
        {textDebug.text = GlobalState.SharedInstance.state;}
        if(GlobalNameStar.SharedInstance.name!=null)
        {textDebug.text = GlobalState.SharedInstance.state+": "+GlobalNameStar.SharedInstance.name;}
        if(GlobalNamePlanet.SharedInstance.name!=null)
        {textDebug.text = GlobalState.SharedInstance.state+": "+GlobalNameStar.SharedInstance.name+": "+GlobalNamePlanet.SharedInstance.name;}
        return 0;
    }
    */
}
