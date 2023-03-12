using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

//синглтон хранит список объектов на все модели
public class GlobalListModel
{
    private List<ModelBase> listModel;
    private static GlobalListModel instance = null;
    public static GlobalListModel SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalListModel();
            }
            return instance;
        }
    }
    public void AddModel(ModelBase model)
    {
        if(listModel==null){listModel = new List<ModelBase>();}
        listModel.Add(model);
    }
    public ModelBase GetModel(string nameModel)
    {
        for (int i = 0; i <= listModel.Count; i++)
        {
            if(listModel[i].name == nameModel)
            {
                return listModel[i];
            }
        }
        return null;
    }
}

//синглтон для взаимодействия с состоянием позиции камеры
public class GlobalStatePosCamera
{
    public string state {get;private set;}
    private StatePosCamera statePosCamera;
    private static GlobalStatePosCamera instance = null;
    public static GlobalStatePosCamera SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStatePosCamera();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        statePosCamera = (StatePosCamera)io;
    }
    public void UpdateState(string flag)
    {
        state = statePosCamera.UpdateState(flag);
    }
}

//синглтон для взаимодействия с состоянием камеры
public class GlobalStateCamera
{
    public string state {get;private set;}
    private StateCamera stateCamera;
    private static GlobalStateCamera instance = null;
    public static GlobalStateCamera SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateCamera();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        stateCamera = (StateCamera)io;
    }
    public void UpdateState(string flag)
    {
        state = stateCamera.UpdateState(flag);
    }
}

//синглтон для взаимодействия с состоянием таргета
public class GlobalStateTarget
{
    public string state {get;private set;}
    private StateTarget stateTarget;
    private static GlobalStateTarget instance = null;
    public static GlobalStateTarget SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateTarget();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        stateTarget = (StateTarget)io;
    }
    public void UpdateState(string flag)
    {
        state = stateTarget.UpdateState(flag);
    }
}

//синглтон для взаимодействия с состоянием ЛКМ
public class GlobalStateMouseLKB
{
    public string state {get;private set;}
    private StateMouseLKB sMouseLKB;
    private static GlobalStateMouseLKB instance = null;
    public static GlobalStateMouseLKB SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateMouseLKB();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        sMouseLKB = (StateMouseLKB)io;
        state = sMouseLKB.state;
    }
    public void UpdateState(string flag)
    {
        state = sMouseLKB.UpdateState(flag);
    }
}

//синглтон для взаимодействия с состоянием скрола мыши
public class GlobalStateMouseScroll
{
    public string state {get;private set;}
    private StateMouseScroll sMouseScroll;
    private static GlobalStateMouseScroll instance = null;
    public static GlobalStateMouseScroll SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateMouseScroll();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        sMouseScroll = (StateMouseScroll)io;
        state = sMouseScroll.state;
    }
    public void UpdateState(string flag)
    {
        state = sMouseScroll.UpdateState(flag);
    }
}

//синглтон для взаимодействия с состоянием сенсора
public class GlobalStateTouch
{
    public string state {get;private set;}
    private StateTouch sTouch;
    private static GlobalStateTouch instance = null;
    public static GlobalStateTouch SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateTouch();
            }
            return instance;
        }
    }
    public void AddState(IState io)
    {
        sTouch = (StateTouch)io;
        state = sTouch.state;
    }
    public void UpdateState(string flag)
    {
        state = sTouch.UpdateState(flag);
    }
}

/////////////////////////////////////////
public class GlobalModelPlanet
{
    private static GlobalModelPlanet instance = null;
    public static GlobalModelPlanet SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalModelPlanet();
            }
            return instance;
        }
    }
    public ModelPlanet mPlanet;
}

public class GlobalModelSolarSystem
{
    private static GlobalModelSolarSystem instance = null;
    public static GlobalModelSolarSystem SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalModelSolarSystem();
            }
            return instance;
        }
    }
    public ModelSolarSystem mSolarSystem;
}

public class GlobalModel
{
    private static GlobalModel instance = null;
    public static GlobalModel SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalModel();
            }
            return instance;
        }
    }
    public IModel iModel;
}

//////////////////////////////////////////////////////////////
public class GlobalTarget
{
    private static GlobalTarget instance = null;
    public static GlobalTarget SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalTarget();
            }
            return instance;
        }
    }
    public string name = "";
}

public class GlobalNameStar
{
    private static GlobalNameStar instance = null;
    public static GlobalNameStar SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalNameStar();
            }
            return instance;
        }
    }
    public string name;
}

public class GlobalNamePlanet
{
    private static GlobalNamePlanet instance = null;
    public static GlobalNamePlanet SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalNamePlanet();
            }
            return instance;
        }
    }
    public string name;
}

//////////////////////////////////////////////////////
public class GlobalState
{
    private static GlobalState instance = null;
    public static GlobalState SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalState();
            }
            return instance;
        }
    }
    public string state; 
}

public class GlobalStateGalaxy
{
    private static GlobalStateGalaxy instance = null;
    public static GlobalStateGalaxy SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateGalaxy();
            }
            return instance;
        }
    }
    public string state; 
}

public class GlobalStateSolarSystem
{
    private static GlobalStateSolarSystem instance = null;
    public static GlobalStateSolarSystem SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateSolarSystem();
            }
            return instance;
        }
    }
    public string state; 
}

public class GlobalStateButton
{
    private static GlobalStateButton instance = null;
    public static GlobalStateButton SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalStateButton();
            }
            return instance;
        }
    }
    public string state; 
}

//по нажатию на кнопку выбора солнечной системы
//ViewCamera записывает имя выбранной звезды в GlobalNameStar
//ViewGalaxy проверяет, есть ли что-то в GlobalNameStar
//ViewGalaxy от правляет имя в ModelGalaxy для отображения солнечной системы


public class GlobalTargetGO
{
    private static GlobalTargetGO instance = null;
    public static GlobalTargetGO SharedInstance
    {
        get
        {
            if(instance == null)
            {
                instance = new GlobalTargetGO();
            }
            return instance;
        }
    }
    public GameObject targetGO; 
}

//корутина со своим MonoBehaviour на борту
public sealed class GlobalCoroutine : MonoBehaviour
{
    private static GlobalCoroutine instance = null;
    private static GlobalCoroutine SharedInstance
    {
        get
        {
            if(instance == null)
            {
                //instance = new GlobalCoroutines();
                var go = new GameObject();
                instance = go.AddComponent<GlobalCoroutine>();
                DontDestroyOnLoad(go); //защита от уничтожения
            }
            return instance;
        }
    }
    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        
        return instance.StartCoroutine(enumerator);
    }
    public static void StopRoutine(Coroutine routine)
    {
        if(routine!=null){return;}
        instance.StopCoroutine(routine);
    }
}