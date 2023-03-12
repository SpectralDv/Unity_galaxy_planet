using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

//1.Создать модель объекта для которого нужен класс состояний
//1.1.Создать приватную сслыку на класс состояний и добавить ее в синглтон
//1.2.
//2.Создать класс состояний 
//2.1.Класс состояний умеет только переходить в состояния
//2.2.
//3.Создать синглтон, который будет хранить приватную ссылку на класс состояний и публичную строку состояния
//3.1.Синглтон необходимо использовать для смены состояние другого объекта
//3.2.Синглтон необходимо использовать для получения состояния в других моделях или объектах

//состояние игры
public class StateGame : IState
{
    public string state{get;private set;}
    public StateGame(){}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "EVENTMAINMENU":
                state=eSTATEGAME.mainmenu.ToString(); //"mainmenu"
                break;
            case "EVENTGALAXY":
                state=eSTATEGAME.galaxy.ToString(); //"galaxy"
                break;
            case "EVENTSOLARSYSTEM":
                state=eSTATEGAME.solarsystem.ToString(); //"solarsystem"
                break;
            case "EVENTPLANET":
                state=eSTATEGAME.planet.ToString(); //"planet"
                break;
            default:
                break;
        }
        return state;
    }
}

//состояние позиции камеры
public class StatePosCamera : IState
{
    public string state{get;private set;}
    public StatePosCamera(){state="idle";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "EVENTMOVE":
                state="move";
                break;
            case "EVENTSCROLL":
                state="scroll";
                break;
            case "EVENTIDLE":
                state="idle";
                break;
            default:
                break;
        }
        return state;
    }
}

//состояние камеры
public class StateCamera : IStateCamera
{
    //public string name {get;private set;}
    //public StateCamera(string name){this.name = name;}
    public string state{get;private set;}
    public StateCamera(){state="galaxy";}
    public string UpdateState(string flag)
    {
        //проверка на тип принятого объекта
        //if(io.GetType() != typeof(IGlobalStateCamera)) return 1;
        //логика изменения состояния от принятого флага
        if(flag == "RETURN")
        {
            if(state == "solarsystem")
            {
                GlobalNameStar.SharedInstance.name=null;
                GlobalNamePlanet.SharedInstance.name=null;
                state = "galaxy";return state;
            }
            if(state == "planet")
            {
                GlobalNamePlanet.SharedInstance.name=null;
                state = "solarsystem";return state;
            }
        }
        if(flag == "SELECT")
        {
            if(state == "galaxy"){state = "solarsystem";return state;}
            if(state == "solarsystem"){state = "planet";return state;}
        }
        if(flag == "GALAXY"){state="galaxy";}
        return state;
    }
}

//состояние ЛКМ
public class StateMouseLKB : IStateMouse
{
    //public string name {get;private set;}
    //public StateMouse(string name){this.name = name;}
    public string state{get;private set;}
    public StateMouseLKB(){state="idle";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "EVENTPRESS":
                state=eSTATEMOUSE.press.ToString();
                break;
            case "EVENTHOLD":
                state=eSTATEMOUSE.hold.ToString();
                break;
            case "EVENTRELEASE":
                state=eSTATEMOUSE.release.ToString();
                break;
            case "EVENTIDLE":
                state=eSTATEMOUSE.idle.ToString();
                break;
            default:
                break;
        }
        return state;
    }
}

//состояние скролла мыши
public class StateMouseScroll : IStateMouse
{
    public string state{get;private set;}
    public StateMouseScroll(){state="idle";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "EVENTSCROLL":
                state=eSTATEMOUSE.scroll.ToString();
                break;
            case "EVENTIDLE":
                state=eSTATEMOUSE.idle.ToString();
                break;
            default:
                break;
        }
        return state;
    }
}

//состояния сенсора
public class StateTouch : IStateTach
{
    public string state{get;private set;}
    public StateTouch(){state="idle";}
    public string UpdateState(string flag)
    {
        switch(flag)
        {
            case "EVENTIDLE":
                state = "idle";
                break;
            case "EVENTPRESS":
                state = "press";
                break;
            case "EVENTSCROLL":
                state = "scroll";
                break;
        }
        return state;
    }
};

public class StateTarget : IState
{
    public string state{get;private set;}
    public StateTarget(){state="empty";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            //приготовиться к сбросу таргета
            case "PRERESET":
                state="reset";
                break;
            //сбросить таргет
            case "RESET":
                state="empty";
                break;
            //выбрать в таргет
            case "SELECT":
                state="full";
                break;
            default:
                break;
        }
        return state;
    }
}


public class StateGalaxy : IState
{
    public string state{get;private set;}
    StateGalaxy(){state="show";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "SHOW":
                state="show";
                break;
            case "HIDE":
                state="hide";
                break;
            default:
                break;
        }
        return state;
    }
}

public class StateSolarSystem : IState
{
    public string state{get;private set;}
    StateSolarSystem(){state="show";}
    public string UpdateState(string flag)
    {
        switch (flag)
        {
            case "SHOW":
                state="show";
                break;
            case "HIDE":
                state="hide";
                break;
            default:
                break;
        }
        return state;
    }
}

