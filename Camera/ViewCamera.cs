using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ModelCamera : ModelBase
{
    public StateCamera stateCamera;
    public StateTarget stateTarget;
    public StatePosCamera statePosCamera;

    //константы
    public float speedMove;// {get;}
    public float speedVert;// {get;}
    public float speedScaleAndroid;// {get;}

    //динамическик
    public Vector3 startPosCamera; //{get;set;}
    public Vector3 curPosCamera; //{get;set;}
    public Vector2 startPosMouse; //{get;set;}
    public Vector2 curPosMouse; //{get;set;}
    public Vector2 dPosMouse;// {get;set;}

    //извне
    public Vector3 corPlanet{get;private set;}
    public Vector3 corSolarSystem{get;private set;}
    public Vector3 corGalaxy{get;private set;}
    public Vector3 roteCameraPlanet{get;private set;}
    public Vector3 roteCameraSolarSystem{get;private set;}
    public Vector3 roteCameraGalaxy{get;private set;}

    public float limitSolar {get;set;}
    public float limitGalaxy {get;set;}



    public float distance;

    public string state{get;set;}// = eCAMERASTATE.init.ToString();

    public ModelCamera()
    {
        //name = "ModelCamera";
        state = "galaxy";
        GlobalListModel.SharedInstance.AddModel((ModelBase)this);

        stateCamera = new StateCamera();
        GlobalStateCamera.SharedInstance.AddState(stateCamera);
        GlobalStateCamera.SharedInstance.UpdateState("GALAXY");

        stateTarget = new StateTarget();
        GlobalStateTarget.SharedInstance.AddState(stateTarget);
        GlobalStateCamera.SharedInstance.UpdateState("RESET");

        speedMove = 5;
        speedVert = 100;
        speedScaleAndroid = 0.1f;

        startPosCamera = new Vector3();
        curPosCamera = new Vector3();
        startPosMouse = new Vector2();
        curPosMouse = new Vector2();
        dPosMouse = new Vector2();

        corPlanet = new Vector3(0,0,0);
        corSolarSystem = new Vector3(0,0,0);
        corGalaxy = new Vector3(0,0,0);
        roteCameraPlanet = new Vector3(0,0,0);
        roteCameraSolarSystem = new Vector3(90,0,0);
        roteCameraGalaxy = new Vector3(90,0,0);

        limitSolar = 20;
        limitGalaxy = 10;
    }
}

///////////////////////////////////////////////////
public class ViewCamera : MonoBehaviour
{
    public Vector3 angleEuler;
    public Quaternion angleQuaternion;
    public Vector3 offset;
    private Transform target;
    private Vector3 vTarget;
    
    public float roteX;
    public float roteY;
    private float sensitivity = 3; 
    private float limit = 85; // ограничение вращения по Y
    private float zoom = 1f; // чувствительность при увеличении, колесиком мышки
    private float zoomMax = 100; // макс. увеличение
    private float zoomMin = 20; // мин. увеличение

    private ViewCamera viewCamera;
    private ModelCamera mCamera;
    private ModelMouse mMouse;
    public GameObject PanelDebug;
    public GameObject PointRigthUp;
    public GameObject ForwardRay;
    public GameObject PanelInfo;
    public GameObject targetGO;
    private Camera camera;
    
    private Text textDebug;
    //private Text textName;

    private string state;
    public float kScroll = 1;

    private float hMin = 8;
    private float hSolarSystem = 90;
    private float hGalaxy = 90;
    private float hPlanet = 90;

    private ModelTouch mTouch;
    private string stateTach;

    void Start()
    {
        mCamera = new ModelCamera();
        mMouse = new ModelMouse();
        mTouch = new ModelTouch();

        camera = GetComponent<Camera>();

        textDebug = PanelDebug.transform.Find("TextDebug").GetComponent<Text>();

        StateGalaxy(10);
    }

    void Update()
    {
        TargetCamera();
        MoveCamera();
        UpdateStateCamera();
    }

    //состояние камеры от нажатия на ЛКМ
    private int UpdateStateCamera()
    {
        if(mCamera.stateCamera.state == mCamera.state){return 1;}

        switch(mCamera.stateCamera.state)
        {
            case "galaxy":
                mCamera.state = mCamera.stateCamera.state;
                StateGalaxy(20);
                PrintPanelDebug();
                break;
            case "solarsystem":
                mCamera.state = mCamera.stateCamera.state;
                StateSolarSystem(20);
                PrintPanelDebug();
                break;
            case "planet":
                mCamera.state = mCamera.stateCamera.state;
                StatePlanet();
                PrintPanelDebug();
                break;
            default:
                break;
        }
        Debug.Log("ModelCamera: "+mCamera.state);
        return 0;
    }

    //состояние камеры от нажатия на ЛКМ
    private void MoveCamera()
    {
        switch(GlobalStateCamera.SharedInstance.state)
        {
            case "galaxy":
                MoveCamera2D();
                ScrollCamera2D();
                break;
            case "solarsystem":
                MoveCamera2D();
                ScrollCamera2D();
                break;
            case "planet":
                MoveScrollCamera3D();
                break;
            default:
                break;
        }
    }

    private int TargetCamera()
    {
        //if(Input.GetMouseButtonDown(0))
        if(GlobalStateMouseLKB.SharedInstance.state=="press")
        {      
            float widthRU = PointRigthUp.transform.position.x;
            float heightRU = PointRigthUp.transform.position.y;   
            //центр
            float centerX = widthRU/2;
            float centerY = heightRU/2;
            //соотношение
            float ratioWidth = widthRU/120;
            float ratioHeight = heightRU/120;
            //длина и высота точки на экранае в пикселяйх
            float pX = Mathf.Round(Input.mousePosition.x)-centerX;
            float pY = Mathf.Round(Input.mousePosition.y)-centerY;
            //максимальный угол по ширине и высоте это FOV/2
            //можно найти угол по ширине widthRU/(FOV)=pX/angleX
            //можно найти угол по высоте heightRU/(FOV)=pY/angleY
            float FOV = transform.GetComponent<Camera>().fieldOfView;
            float angleX = Mathf.Round(pX*(FOV)/widthRU);
            float angleY = Mathf.Round(pY*(FOV)/heightRU);

            float gain = ratioWidth/ratioHeight;
            //задает напраление объекта согласно расчетным углам
            ForwardRay.transform.localEulerAngles = new Vector3(-angleY,angleX*gain,0);
            //направление объекта в кватернионах
            Vector3 rayForward = ForwardRay.transform.forward;

            //начальная точка луча
            Vector3 rayXYZ = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z
            );

            Ray ray = new Ray(rayXYZ,rayForward);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,300))
            {
                var target = hit.collider.GetComponent<Interact>();

                if(target!=null)
                {
                    ResetTarget();
                    target.transform.GetComponent<Outline>().enabled = true;
                    targetGO = target.transform.gameObject;
                    GlobalTargetGO.SharedInstance.targetGO = target.transform.gameObject;
                    GlobalStateTarget.SharedInstance.UpdateState("SELECT");
                    
                    //GlobalStateCamera.SharedInstance.state = eCAMERASTATE.targetGO.ToString();
                    //GlobalStateCamera.SharedInstance.UpdateState("SELECT");
                    GlobalTarget.SharedInstance.name = targetGO.name;
                    if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.galaxy.ToString())
                    {
                        GlobalNameStar.SharedInstance.name=targetGO.name;
                    }
                    if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.solarsystem.ToString())
                    {
                        GlobalNamePlanet.SharedInstance.name=targetGO.name;
                    }
                    PrintPanelDebug();
                    return 0;
                }
            }

            StartCoroutine(ResetTargetSkipTick());
            PrintPanelDebug();
        }
        return 1;
    }

    private IEnumerator ResetTargetSkipTick()
    {
        //пропустит 0.1с, чтобы успела сработать кнопка если она была нажата
        if(GlobalStateTarget.SharedInstance.state!="reset")
        {
            GlobalStateTarget.SharedInstance.UpdateState("PRERESET");
        }
        yield return new WaitForSeconds(0.1f);
        if(GlobalStateTarget.SharedInstance.state=="reset")
        {
            ResetTarget();
        }
        yield return null;
    }

    private int ResetTarget()
    {
        if(targetGO!=null)
        {
            //сбрасывает таргет
            targetGO.transform.GetComponent<Outline>().enabled = false;
            GlobalTarget.SharedInstance.name = "";
            GlobalTargetGO.SharedInstance.targetGO = null;
            targetGO = null;
            GlobalStateTarget.SharedInstance.UpdateState("RESET");
        }
        return 0;
    }

    //метод перемещения камеры в 2d
    private void MoveCamera2D()
    {
        bool eventCameraMove = false;
        bool eventMouseMove = false;

        //если нажата ЛКМ, запоминает позицию камеры в мировой СК
        if(Input.GetMouseButtonDown(0))
        {
            mCamera.startPosCamera = camera.ScreenToWorldPoint(Input.mousePosition);
            mCamera.startPosMouse.x = Mathf.Round(Input.mousePosition.x);
            mCamera.startPosMouse.y = Mathf.Round(Input.mousePosition.y);
        }
        //если зажата ЛКМ проверяет на движение
        //else if(Input.GetMouseButton(0))
        if(GlobalStateMouseLKB.SharedInstance.state=="hold")
        {
            float widthRU = PointRigthUp.transform.position.x;
            float heightRU = PointRigthUp.transform.position.y;   
            float x = 0;
            float z = 0;

            mCamera.curPosMouse.x = Mathf.Round(Input.mousePosition.x);
            mCamera.curPosMouse.y = Mathf.Round(Input.mousePosition.y);

            float evX = mCamera.curPosMouse.x/widthRU - mCamera.startPosMouse.x/widthRU;
            float evY = mCamera.curPosMouse.y/heightRU - mCamera.startPosMouse.y/heightRU;

            mCamera.dPosMouse.x = Mathf.Abs(mCamera.startPosMouse.x - mCamera.curPosMouse.x);
            mCamera.dPosMouse.y = Mathf.Abs(mCamera.startPosMouse.y - mCamera.curPosMouse.y);

            //
            if(mCamera.dPosMouse.x>10 || mCamera.dPosMouse.y>10)
            {eventMouseMove=true;}

            if(eventMouseMove==true)
            {
                if(mCamera.state=="solarsystem")
                //if(GlobalStateCamera.SharedInstance.state == eCAMERASTATE.solarsystem.ToString())
                {
                    x = transform.position.x-mCamera.speedMove*evX*Time.deltaTime*(transform.position.y*0.5f);
                    x = Mathf.Clamp(x,-mCamera.limitSolar,mCamera.limitSolar);
                    z = transform.position.z-mCamera.speedMove*evY*Time.deltaTime*(transform.position.y*0.5f);
                    z = Mathf.Clamp(z,-mCamera.limitSolar,mCamera.limitSolar);
                }
                else if(mCamera.state=="galaxy")
                //else if(GlobalStateCamera.SharedInstance.state == eCAMERASTATE.galaxy.ToString())
                {
                    x = transform.position.x-mCamera.speedMove*evX*Time.deltaTime*((transform.position.y-mCamera.corGalaxy.y)*0.5f);
                    x = Mathf.Clamp(x,mCamera.corGalaxy.x-mCamera.limitGalaxy,mCamera.corGalaxy.x+mCamera.limitGalaxy);
                    z = transform.position.z-mCamera.speedMove*evY*Time.deltaTime*((transform.position.y-mCamera.corGalaxy.y)*0.5f);
                    z = Mathf.Clamp(z,mCamera.corGalaxy.z-mCamera.limitGalaxy,mCamera.corGalaxy.z+mCamera.limitGalaxy);
                }
                transform.position = new Vector3(
                    x, 
                    transform.position.y,
                    z
                );
            }

            mCamera.curPosCamera.x = (camera.ScreenToWorldPoint(Input.mousePosition).x - mCamera.startPosCamera.x);// * 0.5f; 
            mCamera.curPosCamera.z = (camera.ScreenToWorldPoint(Input.mousePosition).z - mCamera.startPosCamera.z);// * 0.5f;  

            //проверка было ли движение камеры
            if(mCamera.curPosCamera.x!=0 || mCamera.curPosCamera.z!=0){eventCameraMove=true;}
            else{eventCameraMove=false;}
        }
    }

    //метод вращения камеры в 3d
    public void MoveScrollCamera3D()
    {
        //eventStartCameraContinent=true;

        //кватарнионы камеры
        //angleQuaternion = transform.localRotation;
        //углы эйлера
        //angleEuler = transform.localEulerAngles;

        //если нажата ЛКМ, запоминает позицию в мировой СК
        //if(Input.GetMouseButtonDown(0))
        //{
            //при зажатии событие движения камеры сбрасывается
            //dCamMove = false;
            //offset = new Vector3(offset.x, offset.y, offset.z);
        //}
        //если зажата ЛКМ 
        //else if(Input.GetMouseButton(0))
        if(GlobalStateMouseLKB.SharedInstance.state=="hold")
        {
            roteX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            roteY += Input.GetAxis("Mouse Y") * sensitivity;
            roteY = Mathf.Clamp (roteY, -limit, limit);

            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
            transform.localEulerAngles = new Vector3(-roteY, roteX, 0);
            transform.position = transform.localRotation * (offset);// + vTarget;
        }
        //зум
        else if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
		    else if(Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
		    offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            transform.position = transform.localRotation * offset;// + target.position;

            mCamera.distance = offset.z;
            if((-mCamera.distance)>hPlanet){StateSolarSystem(8);}
            MoveScroll();
        }
        //зум для сенсорного экрана
        //if(Input.touches.Length == 2)
        if(GlobalStateTouch.SharedInstance.state=="scroll")
        {
            Touch touchFirst = Input.GetTouch(0);
            Touch touchSecond = Input.GetTouch(1);

            Vector2 touchFirstPrev = touchFirst.position - touchFirst.deltaPosition;
            Vector2 touchSecondPrev = touchSecond.position - touchSecond.deltaPosition;

            float prevMagnitude = (touchFirstPrev - touchSecondPrev).magnitude;
            float currentMagnitude = (touchFirst.position - touchSecond.position).magnitude;

            float difference = (currentMagnitude - prevMagnitude);

            if(Input.GetMouseButton(0))
            {
                //mCamera.curPosCamera.y = transform.position.y+mCamera.speedScaleAndroid*(-difference)*Time.deltaTime*kScroll;
                
                if(difference>0){offset.z += zoom;}
                if(difference<0){offset.z -= zoom;}
                offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

                transform.position = transform.localRotation * offset;// + target.position;

                mCamera.distance = offset.z;
                if((-mCamera.distance)>hPlanet){StateSolarSystem(8);}
                MoveScroll();
            }
        }
    }

    private void ScrollCamera2D()
    {
        //для сенсорного экрана
        //if(Input.touches.Length == 2)
        if(GlobalStateTouch.SharedInstance.state=="scroll")
        {
            Touch touchFirst = Input.GetTouch(0);
            Touch touchSecond = Input.GetTouch(1);

            Vector2 touchFirstPrev = touchFirst.position - touchFirst.deltaPosition;
            Vector2 touchSecondPrev = touchSecond.position - touchSecond.deltaPosition;

            float prevMagnitude = (touchFirstPrev - touchSecondPrev).magnitude;
            float currentMagnitude = (touchFirst.position - touchSecond.position).magnitude;

            float difference = (currentMagnitude - prevMagnitude);

            if(Input.GetMouseButton(0))
            {
                mCamera.curPosCamera.y = transform.position.y+mCamera.speedScaleAndroid*(-difference)*Time.deltaTime*kScroll;
                mCamera.distance = mCamera.curPosCamera.y;
                MoveScroll();
            }
        }
        //для мыши
        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            float scrollMouse = Input.GetAxis("Mouse ScrollWheel");
            mCamera.curPosCamera.y = transform.position.y+mCamera.speedVert*(-scrollMouse)*Time.deltaTime*kScroll;
            mCamera.distance = mCamera.curPosCamera.y;
            MoveScroll();
        }
    }

    private int MoveScroll()
    {
        //переход из галактики в солнечную систему
        if(GlobalStateCamera.SharedInstance.state == eGLOBALSTATE.galaxy.ToString())
        {
            if(mCamera.curPosCamera.y<=3){mCamera.curPosCamera.y=3;}
            if(mCamera.curPosCamera.y<=5){StateSolarSystem(hSolarSystem);}
            if(mCamera.curPosCamera.y>=mCamera.corGalaxy.y+hGalaxy){mCamera.curPosCamera.y=mCamera.corGalaxy.y+hGalaxy;}
            kScroll = (transform.position.y-mCamera.corGalaxy.y)*1f;
            if(kScroll>10){kScroll=10;}
            transform.position = new Vector3(
                transform.position.x,
                mCamera.curPosCamera.y,
                transform.position.z
            );
        }
        //переход из солнечной системы в галактику
        if(GlobalStateCamera.SharedInstance.state == eGLOBALSTATE.solarsystem.ToString())
        {
            if(mCamera.curPosCamera.y<=8){mCamera.curPosCamera.y=8;}
            if(mCamera.curPosCamera.y>100){mCamera.curPosCamera.y=100;}
            if(mCamera.distance>hSolarSystem){StateGalaxy(10);}
            //if(mCamera.curPosCamera.y>hSolarSystem){StateGalaxy();}
            kScroll = (transform.position.y-mCamera.corPlanet.y)*0.5f;
            if(kScroll>10){kScroll=10;}
            transform.position = new Vector3(
                transform.position.x,
                mCamera.curPosCamera.y,
                transform.position.z
            );
        }
        //переход из солнечной системы в планету
        if(GlobalStateCamera.SharedInstance.state == eGLOBALSTATE.solarsystem.ToString())
        {
            //переход по кнопки выбора планеты
        }
        //переход из планеты в солнечную систему
        if(GlobalStateCamera.SharedInstance.state == eGLOBALSTATE.planet.ToString())
        {
            //if(mCamera.curPosCamera.y<=3){mCamera.curPosCamera.y=3;}
            if((-mCamera.distance)>hPlanet){StateSolarSystem(8);}
            //kScroll = (transform.position.y-mCamera.corPlanet.y)*0.5f;
        }
        return 0;
    }

    private int StatePlanet()
    {
        //if(state=="planet"){return 0;}
        //state="planet";
        transform.position = new Vector3(0,0,-zoomMin);
        transform.localEulerAngles = mCamera.roteCameraPlanet;

        //this.PrintPanelDebug();
        //GlobalState.SharedInstance.state = eGLOBALSTATE.planet.ToString();

        return 0;
    }

    private int StateSolarSystem(float distance)
    {
        if(GlobalNameStar.SharedInstance.name == null){return 0;}
        //if(GlobalTarget.SharedInstance.name==null){return 0;}
        //if(state=="solarsystem"){return 0;}
        //state="solarsystem";
        mCamera.distance = 0;
        offset.z=0;
        transform.localEulerAngles = new Vector3(90,0,0);

        //GlobalState.SharedInstance.state = eGLOBALSTATE.solarsystem.ToString();
        //this.PrintPanelDebug();

        transform.position = new Vector3(
            mCamera.corSolarSystem.x,
            mCamera.corSolarSystem.y+distance,
            mCamera.corSolarSystem.z
        );
        //mCamera.curPosCamera.y=mCamera.corSolarSystem.y+distance;

        return 0;
    }

    private int StateGalaxy(float distance)
    {
        //if(state=="galaxy"){return 0;}state="galaxy";

        //GlobalState.SharedInstance.state = eGLOBALSTATE.galaxy.ToString();
        //this.PrintPanelDebug();

        transform.position = new Vector3(
            mCamera.corGalaxy.x,
            mCamera.corGalaxy.y+distance,
            mCamera.corGalaxy.z
        );
        mCamera.curPosCamera.y=mCamera.corGalaxy.y+10;

        return 0;
    }

    private int PrintPanelDebug()
    {
        //if(GlobalStateCamera.SharedInstance.state!=null)
        //{textDebug.text = GlobalStateCamera.SharedInstance.state;}
        //if(GlobalNameStar.SharedInstance.name!=null)
        //{textDebug.text = GlobalStateCamera.SharedInstance.state+": "+GlobalNameStar.SharedInstance.name;}
        //if(GlobalNamePlanet.SharedInstance.name!=null)
        //{textDebug.text = GlobalStateCamera.SharedInstance.state+": "+GlobalNameStar.SharedInstance.name+": "+GlobalNamePlanet.SharedInstance.name;}
        return 0;
    }
}
