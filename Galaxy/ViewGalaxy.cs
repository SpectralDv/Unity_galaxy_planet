using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModelStar : IModel
{
    public string name {get;set;}
    public Vector3 cor;
    public float countPlanet;
    public ModelSolarSystem mSolarSystem;

    public ModelStar(string nameStar,Vector3 corStar)
    {
        name = nameStar;
        cor = corStar;
    }
    public void AddSolarSystem(ModelSolarSystem modelSolarSystem)
    {
        mSolarSystem = modelSolarSystem;
    }
}

public class ControllerStar //ModelGalaxy
{
    private List<ModelStar> listModelStar;
    private Vector3 cor; //координаты галактики

    public ControllerStar()
    {
        cor = new Vector3(0,0,0);
        listModelStar = new List<ModelStar>();
        this.InitController();
    }
    private void InitController()
    {
        listModelStar.Add(new ModelStar("Star1",new Vector3(cor.x,cor.y,cor.z)));
        listModelStar.Add(new ModelStar("Star2",new Vector3(cor.x+4,cor.y,cor.z+4)));
        listModelStar.Add(new ModelStar("Star3",new Vector3(cor.x-6,cor.y,cor.z-6)));
        listModelStar.Add(new ModelStar("Star4",new Vector3(cor.x-8,cor.y,cor.z+9)));
        listModelStar.Add(new ModelStar("Star5",new Vector3(cor.x+5,cor.y,cor.z-5)));
        listModelStar.Add(new ModelStar("Star6",new Vector3(cor.x-11,cor.y,cor.z-7)));
        listModelStar.Add(new ModelStar("Star7",new Vector3(cor.x-14,cor.y,cor.z+5)));
        listModelStar.Add(new ModelStar("Star8",new Vector3(cor.x+14,cor.y,cor.z-1)));
        listModelStar.Add(new ModelStar("Star9",new Vector3(cor.x+14,cor.y,cor.z+7)));
        listModelStar.Add(new ModelStar("Star10",new Vector3(cor.x+20,cor.y,cor.z+3)));
        listModelStar.Add(new ModelStar("Star11",new Vector3(cor.x+18,cor.y,cor.z-7)));
        listModelStar.Add(new ModelStar("Star12",new Vector3(cor.x-20,cor.y,cor.z-4)));
        listModelStar.Add(new ModelStar("Star13",new Vector3(cor.x-20,cor.y,cor.z+11)));
        listModelStar.Add(new ModelStar("Star14",new Vector3(cor.x+9,cor.y,cor.z+12)));
        listModelStar.Add(new ModelStar("Star15",new Vector3(cor.x-1,cor.y,cor.z+13)));
    }
    public int GetCount()
    {
        return listModelStar.Count;
    }
    public ModelStar GetModel(int index)
    {
        return listModelStar[index];
    }
    public string GetNameStar(int index)
    {
        return listModelStar[index].name;
    }
    public Vector3 GetCorStar(int index)
    {
        if(index>listModelStar.Count && index<0){return cor;}
        return listModelStar[index].cor;
    }
    //или все показывать
    //или все не показывать
}

/////////////////////////////////////////////////////////////
public class ViewGalaxy : MonoBehaviour
{
    private ControllerStar cStar;
    public List<GameObject> listPrefabStar;
    public GameObject GalaxySystem;
    public List<GameObject> listStar;
    private string state;

    void Start()
    {
        cStar = new ControllerStar();
        this.CreateGalaxy();
    }

    void Update()
    {
        this.StateGalaxy();
    }

    private void StateGalaxy()
    {
        //if(GlobalState.SharedInstance.state==eGLOBALSTATE.galaxy.ToString())
        if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.galaxy.ToString())
        {
            this.ShowGalaxy();
        }
        //if(GlobalState.SharedInstance.state!=eGALAXYSTATE.galaxy.ToString())
        if(GlobalStateCamera.SharedInstance.state!=eGALAXYSTATE.galaxy.ToString())
        {
            this.HideGalaxy();
        }
    }

    private void CreateGalaxy()
    {
        GalaxySystem = new GameObject("GalaxySystem"); 
        GalaxySystem.transform.parent = transform;

        for (int i = 0; i < cStar.GetCount(); i++)
        {
            listStar.Add(Instantiate(listPrefabStar[0]));
            listStar[i].name = cStar.GetNameStar(i);
            listStar[i].transform.position = cStar.GetCorStar(i);
            listStar[i].transform.parent = GalaxySystem.transform;
        }
    }
    private int ShowGalaxy()
    {
        if(state=="show"){return 0;}
        state="show";
        GalaxySystem.SetActive(true);
        return 0;
    }
    private int HideGalaxy()
    {
        if(state=="hide"){return 0;}
        state="hide";
        GalaxySystem.SetActive(false);
        return 0;
    }
}
