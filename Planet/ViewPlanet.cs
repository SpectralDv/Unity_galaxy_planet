using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//генерация тайлов террейна по шуму перлина от заданного значения(сида)
//для того чтобы получить участки, вместо рандомно разбросанных тайлов
//интересно как сид применить к солнечным системам и галактики

enum eLISTTERRAIN
{
    tundra,
    taiga,
    steppe,

    ise,
    grass,
    forrest,
    jungle,
    desert,
    oasis,
    lake,
    ocean
}




public class ModelTerrain : IModel
{
    public string name{get;set;}
    public float width;
    public float height;

    public ModelTerrain(){}
}

public class ModelHex : IModel
{
    public string name{get;set;}
    public float scale;

    public ModelHex(){}
}

public class ModelSphere : IModel
{
    public string name{get;set;}
    public float radius;
    public ModelHex mHex;
    public ModelSphere(float radiusSphere){radius=radiusSphere;}
}

public class ControllerPlanet
{
    private List<ModelSphere> lestModelTerrain;

    public ControllerPlanet()
    {
        lestModelTerrain = new List<ModelSphere>();
        lestModelTerrain.Add(new ModelSphere(9.94f)); //9.94f //20.91f //14.91f
    }
    public float GeRadius(int index)
    {
        return lestModelTerrain[index].radius;
    }
    public ModelSphere GetModel(int index)
    {
        return lestModelTerrain[index];
    }
}

public class ModelObjectHex
{
    public List<GameObject> listHex;
    public ModelObjectHex(){listHex = new List<GameObject>();}
}

//////////////////////////////////////////
public class ViewPlanet : MonoBehaviour
{
    private ControllerPlanet cPlanet;
    private List<GameObject> listHex;
    private List<ModelObjectHex> listObjectHex;

    public GameObject prefubTerrain;
    public GameObject prefubSphere;
    public GameObject prefubHexForrest;

    private GameObject hex;
    private GameObject planet;
    private GameObject sphere;
    private GameObject terrain;

    void Start()
    {
        cPlanet = new ControllerPlanet();
        listHex = new List<GameObject>();
        listObjectHex = new List<ModelObjectHex>();
        this.CreatePlanet();
    }

    void Update()
    {
        this.StatePlanet();
    }

    private void StatePlanet()
    {
        //if(GlobalState.SharedInstance.state==eGLOBALSTATE.planet.ToString())
        if(GlobalStateCamera.SharedInstance.state==eGLOBALSTATE.planet.ToString())
        {
            this.ShowPlanet();
        }
        //if(GlobalState.SharedInstance.state!=eGALAXYSTATE.planet.ToString())
        if(GlobalStateCamera.SharedInstance.state!=eGALAXYSTATE.planet.ToString())
        {
            this.HidePlanet();
        }
    }

    private void CreatePlanet()
    {
        this.CreateSphere();
        this.HidePlanet();
    }
    private void ShowPlanet()
    {
        planet.SetActive(true);
    }
    private void HidePlanet()
    {
        planet.SetActive(false);
    }
    private void CreateSphere()
    {
        planet = new GameObject("Planet");
        planet.transform.parent = transform;

        //создать сферу с заданным деаметорм
        float d = cPlanet.GeRadius(0)*2;
        sphere = Instantiate(prefubSphere);
        sphere.transform.parent = planet.transform;
        sphere.transform.localScale = new Vector3(d,d,d);
    }

    //////////////////////////////////////////////////////
    private void CreateHexSphere(string flag)
    {
        switch(flag)
        {
            case "hand":
                HandGenerateTail();
                break;
            case "auto":
                AutoGenerateTail();
                break;
            default:
                break;
        }
    }
    ////////////////////////////////////////////////
    private void HandGenerateTail()
    {
        float rSphere = cPlanet.GeRadius(0);
        float scaleHex = 1;
        float dHex = 1.6f;

        //длина окружности
        float lengthCircle = (2*3.14f*rSphere);

        //количество тайлов на экваторе от длины окружности
        float countMaxHex = lengthCircle/dHex-1;
        //Debug.Log("countMaxHex: "+countMaxHex);

        //угол на экваторе между тайлами
        float angleY = dHex/rSphere*57.3f;
        Debug.Log("angleY: "+angleY);

        //один по центру
        float x = 0;
        float y = rSphere;
        float z = 0;
        hex = Instantiate(prefubHexForrest);
        hex.transform.position = new Vector3(x,y,z);
        hex.transform.localEulerAngles = new Vector3(0,0,0);
        hex.transform.parent = planet.transform;
        listHex.Add(hex);

        //количество колец в полушарии
        float countCirle = lengthCircle*0.25f;
        //Debug.Log("countCirle: "+countCirle);

        //генерация каждого кольца
        for (int i = 1; i <= 2; i++)
        {
            switch(i)
            {
                case 1:
                    GenerateStripHex(x,y,z);
                    break;
                default:
                    break;
            }
        }
    }
    private void GenerateStripHex(float x,float y,float z)
    {
        float dHex = 1.6f;
        float countTail = 6;
        float lengthRing = 1.6f;

        float rSphere = cPlanet.GeRadius(0);
        float angleY = dHex/rSphere*57.3f;
        
        //радиус текущей окружности
        float radiusCurCircle = Mathf.Sin(angleY/57.3f)*rSphere;
        Debug.Log("radiusCurCircle: "+radiusCurCircle);
        //длина текущей окружности
        float lengthCurCircle = 2*3.14f*radiusCurCircle;

        float lengthCur = lengthCurCircle/countTail;
        //Debug.Log("lengthCur: "+lengthCur);

        //угол окружности текущего уольца
        float angleCur = (lengthCur*180)/(3.14f*radiusCurCircle);

        float angleFirstCircle = 0;
        float countAngleFirstCircle = 1;

        //количество тайлов в кольце
        for (int j = 1; j <= countTail; j++)
        {
                x = Mathf.Cos(angleCur/57.3f*j)*radiusCurCircle;
                y = Mathf.Cos(angleY/57.3f)*rSphere;
                z = Mathf.Sin(angleCur/57.3f*j)*radiusCurCircle;

                //Debug.Log("angleCur: "+angleCur*j+" / "+angleFirstCircle*countAngleFirstCircle);
                //0,60,120,180,240,300
                if(Mathf.Round(angleCur*j) == angleFirstCircle*countAngleFirstCircle)
                {
                //    //Debug.Log("angleCur: "+angleCur*j+" / "+angleFirstCircle*countAngleFirstCircle);
                //    countAngleFirstCircle = countAngleFirstCircle+1;
                    x = Mathf.Cos(angleCur/57.3f*j)*(radiusCurCircle+0.1f);
                    z = Mathf.Sin(angleCur/57.3f*j)*(radiusCurCircle+0.1f);
                }
                //other angle
                else
                {
                    x = Mathf.Cos(angleCur/57.3f*j)*(radiusCurCircle-0.3f);//*i);
                    z = Mathf.Sin(angleCur/57.3f*j)*(radiusCurCircle-0.3f);//*i);
                }

                hex = Instantiate(prefubHexForrest);
                hex.transform.position = new Vector3(x,y,z);

                //направление от центра сферы до положения тайла
                float angleX = Mathf.Sin(angleY/57.3f);
                float angleZ = Mathf.Cos(angleY/57.3f);
                hex.transform.localEulerAngles = new Vector3(0,0,0);
                //hex.transform.localEulerAngles = new Vector3(0,0,-angleY);
                hex.transform.parent = planet.transform;
                listHex.Add(hex);
        }
    }
    ///////////////////////////////////////////////
    private void AutoGenerateTail()
    {
        float rSphere = cPlanet.GeRadius(0);
        float scaleHex = 1;
        float dHex = 1.6f;

        //длина окружности
        float lengthCircle = (2*3.14f*rSphere);

        //количество тайлов на экваторе от длины окружности
        float countMaxHex = lengthCircle/dHex-1;
        //Debug.Log("countMaxHex: "+countMaxHex);

        //угол на экваторе между тайлами
        float angleY = dHex/rSphere*57.3f;
        Debug.Log("angleY: "+angleY);

        //один по центру
        float x = 0;
        float y = rSphere;
        float z = 0;
        hex = Instantiate(prefubHexForrest);
        hex.transform.position = new Vector3(x,y,z);
        hex.transform.localEulerAngles = new Vector3(0,0,0);
        hex.transform.parent = planet.transform;
        listHex.Add(hex);

        //количество колец в полушарии
        float countCirle = lengthCircle*0.25f;
        //Debug.Log("countCirle: "+countCirle);
        float angleFirstCircle = 0;

        //количетсво колец
        for (int i = 1; i < 19; i++)
        {
            //радиус текущей окружности
            float radiusCurCircle = Mathf.Sin(angleY/57.3f*i)*rSphere;
            Debug.Log("radiusCurCircle: "+radiusCurCircle);
            //длина текущей окружности
            float lengthCurCircle = 2*3.14f*radiusCurCircle;
            //Debug.Log("lengthCurCircle: "+lengthCurCircle);
            //количество тайлов(длина окружности на ширину тайла)
            float countTail = Mathf.Round(lengthCurCircle/dHex); // 6*i;//
            //Debug.Log("countTail: "+countTail);
            //длина дуги
            float lengthCur = lengthCurCircle/countTail;
            //Debug.Log("lengthCur: "+lengthCur);

            //L=(pi*r*angle)/180
            //L*180=pi*r*angle
            //angle=(L*180)/(pi*r)
            //угол окружности текущего кольца
            float angleCur = (lengthCur*180)/(3.14f*radiusCurCircle);
            //Debug.Log("angleCur: "+angleCur);
            //углы первого кольца
            //float[] angleFirstCircle = new float[6];
            if(i==1){angleFirstCircle = angleCur;}
            //Debug.Log("angleCur: "+angleCur);
            float countAngleFirstCircle = 1;

            //количество тайлов в кольце
            for (int j = 1; j <= countTail; j++)
            {
                x = Mathf.Cos(angleCur/57.3f*j)*radiusCurCircle;
                y = Mathf.Cos(angleY/57.3f*i)*rSphere;
                z = Mathf.Sin(angleCur/57.3f*j)*radiusCurCircle;
                if(i!=1)
                {
                    //Debug.Log("angleCur: "+angleCur*j+" / "+angleFirstCircle*countAngleFirstCircle);
                    //0,60,120,180,240,300
                    if(Mathf.Round(angleCur*j)==angleFirstCircle*countAngleFirstCircle)
                    {
                        //Debug.Log("angleCur: "+angleCur*j+" / "+angleFirstCircle*countAngleFirstCircle);
                        countAngleFirstCircle = countAngleFirstCircle+1;
                        x = Mathf.Cos(angleCur/57.3f*j)*(radiusCurCircle+0.1f*i);
                        z = Mathf.Sin(angleCur/57.3f*j)*(radiusCurCircle+0.1f*i);
                    }
                    //other angle
                    else
                    {
                        x = Mathf.Cos(angleCur/57.3f*j)*(radiusCurCircle-0.3f);//*i);
                        z = Mathf.Sin(angleCur/57.3f*j)*(radiusCurCircle-0.3f);//*i);
                    }
                }
                hex = Instantiate(prefubHexForrest);
                hex.transform.position = new Vector3(x,y,z);

                //направление от центра сферы до положения тайла
                float angleX = Mathf.Sin(angleY/57.3f*i);
                float angleZ = Mathf.Cos(angleY/57.3f*i);
                //hex.transform.localEulerAngles = new Vector3(0,0,0);
                hex.transform.localEulerAngles = new Vector3(0,0,-angleY);
                hex.transform.parent = planet.transform;
                listHex.Add(hex);
            }
        }
    }

    private void CreateGridSphere()
    {

    }

    //////////////////////////////////////////
    private void CreateTerrain()
    {
        planet = new GameObject("Planet");
        planet.transform.parent = transform;

        terrain = Instantiate(prefubTerrain);
        terrain.transform.parent = planet.transform;
    }
}
