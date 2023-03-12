using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//надо расширить набор планет, чтобы в каждой солнечной систесмы были уникальные планеты

public class ModelPlanet : IModel
{
    public string name {get;set;}
    public float radiusOrbit{set;get;}
    public float speedMove{set;get;}
    public float speedRotate{set;get;}
    public ModelPlanet(){}
}


public class ModelSolarSystem : IModel
{
    public string name {get;set;}
    public Vector3 cor;
    public float countPlanet;
    public List<ModelPlanet> ListModelPlanet;
    private float beginStep = 5;
    private float endStep = 6;
    public string state;
    public ModelSolarSystem(float _countPlanet,string nameStar,Vector3 corSolarSystem)
    {
        name = nameStar;
        countPlanet=_countPlanet;
        cor = corSolarSystem;
        state = eSOLARSYSTEM.init.ToString();
        ListModelPlanet = new List<ModelPlanet>();
        this.InitModel();
    }
    private void InitModel()
    {
        for(int i=0;i<countPlanet;i++)
        {
            ListModelPlanet.Add(new ModelPlanet());
            ListModelPlanet[i].radiusOrbit = Random.Range(beginStep,endStep);
            if(i>=1){ListModelPlanet[i].radiusOrbit += ListModelPlanet[i-1].radiusOrbit;}

            ListModelPlanet[i].speedMove = 0.05f + 0.05f*(countPlanet-i);//Random.Range(0,ListModelPlanet[i].radiusOrbit*0.005f);
            ListModelPlanet[i].speedRotate = 50;//* Random.Range(0,ListModelPlanet[i].radiusOrbit*0.01f);
        }
    }
    public int GetCountPlanet()
    {
        return ListModelPlanet.Count;
    }
}

public class ControllerSolarSystem //ModelGalaxy
{
    private List<ModelSolarSystem> listModelSolarSystem;
    Vector3 cor; //координаты солнечной системы

    public ControllerSolarSystem()
    {
        cor = new Vector3(0,0,0);
        listModelSolarSystem = new List<ModelSolarSystem>();
        this.InitController();
    }
    private void InitController()
    {
        listModelSolarSystem.Add(new ModelSolarSystem(2,"Star1",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(3,"Star2",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(4,"Star3",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(5,"Star4",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(2,"Star5",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(3,"Star6",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(4,"Star7",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(5,"Star8",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(2,"Star9",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(3,"Star10",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(4,"Star11",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(5,"Star12",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(2,"Star13",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(3,"Star14",cor)); 
        listModelSolarSystem.Add(new ModelSolarSystem(4,"Star15",cor)); 
    }
    public int GetCount()
    {
        return listModelSolarSystem.Count;
    }
    public int GetCountPlanet(int index)
    {
        return listModelSolarSystem[index].GetCountPlanet();
    }
    public string GetName(int index)
    {
        return listModelSolarSystem[index].name;
    }
    public ModelSolarSystem GetModel(int index)
    {
        return listModelSolarSystem[index];
    }
    //определить какую показать и изменить параметр состояния
    public void ShowSolarSystem()
    {

    }
    //изменить параметр состояния, чтобы скрыть
    public void HideSolarSystem()
    {

    }
}

//////////////////////////////////////////////////////
public class ModelObjectSolarSystem
{
    public string name;
    public string state;
    public GameObject sun;
    public GameObject sunObject;
    public List<GameObject> listPlanet;
    public List<GameObject> listOrbit;
    public ModelObjectSolarSystem()
    {
        //state=eSOLARSYSTEM.hide.ToString();
        listPlanet = new List<GameObject>();
        listOrbit = new List<GameObject>();
    }
}

///////////////////////////////////////////////////////////
public class ViewSolarSystem : MonoBehaviour
{
    ControllerSolarSystem cSolarSystem;
    ModelSolarSystem mSolarSystem;
    ModelObjectSolarSystem mListSolarSystem;
    public GameObject prefabPoint;
    public GameObject prefabSun;
    public List<GameObject> listPrefabPlanet;

    private GameObject sunObject;
    public List<GameObject> listSolarSystem;
    public List<ModelObjectSolarSystem> listObjectSolarSystem;
    public List<GameObject> listPlanet;
    public List<GameObject> listOrbit;

    private float countPoint = 360;
    private float pi = 57.3f;
    float time = 0;

    private bool eventMove = false;
    private string state;

    void Start()
    {
        cSolarSystem = new ControllerSolarSystem();
        listObjectSolarSystem = new List<ModelObjectSolarSystem>();
        listPlanet = new List<GameObject>();
        listOrbit = new List<GameObject>();

        this.CreateSolarSystem();
    }
    void Update()
    {
        StateSolarSystem();
        UpdateSolarSystem();
    }
    /////////////////////////////////////////////////////
    void UpdateSolarSystem()
    {
        time = GlobalTime.SharedInstance.time;

        if(eventMove==true)
        {
            this.MoveSun();
            this.MovePlanet();
        }
    }
    void StateSolarSystem()
    {
        //if(GlobalState.SharedInstance.state==eGLOBALSTATE.solarsystem.ToString())
        if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.solarsystem.ToString())
        {
            this.ShowSolarSystem();
        }
        //if(GlobalState.SharedInstance.state!=eGALAXYSTATE.solarsystem.ToString())
        if(GlobalStateCamera.SharedInstance.state!=eGALAXYSTATE.solarsystem.ToString())
        {
            this.HideSolarSystem();
        }
    }
    public int CreateSolarSystem()
    {
        for (int i = 0; i < cSolarSystem.GetCount(); i++)
        {
            mSolarSystem = cSolarSystem.GetModel(i);

            GameObject solarSystem = new GameObject("SolarSystem"+mSolarSystem.name); 
            listSolarSystem.Add(Instantiate(solarSystem));
            Destroy(solarSystem);
            listSolarSystem[listSolarSystem.Count-1].transform.parent = transform;
            listSolarSystem[listSolarSystem.Count-1].name = "SolarSystem"+mSolarSystem.name;
            solarSystem = listSolarSystem[listSolarSystem.Count-1];

            sunObject = Instantiate(prefabSun);
            sunObject.transform.position = solarSystem.transform.position;
            sunObject.transform.parent = solarSystem.transform;
            sunObject.name = mSolarSystem.name;
            listObjectSolarSystem.Add(new ModelObjectSolarSystem());
            listObjectSolarSystem[listSolarSystem.Count-1].sun = sunObject.transform.Find("sun").gameObject;

            for(int j=0;j<cSolarSystem.GetCountPlanet(i);j++)
            {
                float radius = mSolarSystem.ListModelPlanet[j].radiusOrbit;
                float x = Mathf.Cos(j/pi)*radius+Mathf.Cos(j/pi)*radius;
                float z = Mathf.Sin(j/pi)*radius+Mathf.Sin(j/pi)*radius;

                int index = j; 
                if(j>=listPrefabPlanet.Count){index=listPrefabPlanet.Count-1;}
                listObjectSolarSystem[listSolarSystem.Count-1].listPlanet.Add(Instantiate(listPrefabPlanet[index]));
                listObjectSolarSystem[listSolarSystem.Count-1].listPlanet[j].transform.position = new Vector3(x,0,z) + solarSystem.transform.position;
                listObjectSolarSystem[listSolarSystem.Count-1].listPlanet[j].transform.parent = solarSystem.transform;
                listObjectSolarSystem[listSolarSystem.Count-1].listPlanet[j].name = "Planet"+(j+1);
            }
            solarSystem.SetActive(false);
        }
        
        return 0;
    }
    public int ShowSolarSystem()
    {
        if(state == "show"){return 0;}
        state = "show";
          
        for(int i=0;i<listSolarSystem.Count;i++)
        {
            if(cSolarSystem.GetName(i) == GlobalNameStar.SharedInstance.name)
            {
                mSolarSystem = cSolarSystem.GetModel(i);
            }
            if(listSolarSystem[i].name == "SolarSystem"+GlobalNameStar.SharedInstance.name)
            {
                listSolarSystem[i].SetActive(true);
                listObjectSolarSystem[i].state="show";
                eventMove = true;

                for (int j = 0; j < listObjectSolarSystem[i].listPlanet.Count; j++)
                {
                    listPlanet.Add(listObjectSolarSystem[i].listPlanet[j]);
                }

                this.GenerateOrbit();
                break;
            }
        }
        return 0;
    }
    public int HideSolarSystem()
    {
        if(state == "hide"){return 0;}
        state = "hide";
        eventMove = false;

        for (int i = 0; i < listObjectSolarSystem.Count; i++)
        {
            if(listObjectSolarSystem[i].state=="show")
            {
                listObjectSolarSystem[i].state="hide";
                listSolarSystem[i].SetActive(false);
            }
        }
            
        listPlanet.Clear();
        for(int i=0;i<listOrbit.Count;i++)
        {
            Destroy(listOrbit[i]);
        }
        listOrbit.Clear();

        sunObject.SetActive(false);
        return 0;
    }
    ///////////////////////////////////////////////////////////
    private void MoveSun()
    {
        float speedRotate = 2;
        sunObject.transform.rotation = Quaternion.Euler(90, speedRotate*time, 0);
    }
    private void MovePlanet()
    {
        for(int i=0;i<listPlanet.Count;i++)
        {
            float speedRotate = mSolarSystem.ListModelPlanet[i].speedRotate;
            float speedMove = mSolarSystem.ListModelPlanet[i].speedMove;
            float radius = mSolarSystem.ListModelPlanet[i].radiusOrbit;

            listPlanet[i].transform.rotation = Quaternion.Euler(-180, speedRotate*time, 0);

            float x = Mathf.Cos(speedMove*time) * radius;
            float z = Mathf.Sin(speedMove*time) * radius;
            listPlanet[i].transform.position = new Vector3(x, 0, z) + transform.position;
        }
    }
    ////////////////////////////////////////////////////////
    private void GenerateOrbit()
    {
        for(int i=0;i<listPlanet.Count;i++)
        {
            GameObject orbit = new GameObject("orbit"+(i+1));
            //orbit.transform.parent = transform;
            listOrbit.Add(Instantiate(orbit));
            listOrbit[listOrbit.Count-1].transform.parent = transform;
            Destroy(orbit);

            for(int j=0;j<360;j++)
            {
                float radius = mSolarSystem.ListModelPlanet[i].radiusOrbit;
                float x = Mathf.Cos(j/pi)*radius;
                float z = Mathf.Sin(j/pi)*radius;

                GameObject point = Instantiate(prefabPoint);
                point.transform.position = new Vector3(x, 0, z) + transform.position;
                //point.transform.parent = orbit.transform;
                point.transform.parent = listOrbit[listOrbit.Count-1].transform;
            }
        }
    }
}
