using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSun : MonoBehaviour
{
    public GameObject point;
    private List<GameObject> listPoint;
    private List<ModelPoint> listModelPoint;
    private float radius = 2.5f;
    private float radiusMove = 0.1f;
    private int countPoint = 0;

    class Point
    {
        public float x;
        public float y;
        public float z;
        public Point(float X,float Y,float Z){x=X;y=Y;z=Z;}
    };

    class ModelPoint
    {
        public bool eventMove = false;
        public float randX = 0;
        public float randY = 0;
        public float randZ = 0;
        public float hipotenuza = 0;
        public int numStep = 0;
        public List<Point> listStep;
        public ModelPoint(){listStep = new List<Point>();}
    }

    void Start()
    {
        listModelPoint = new List<ModelPoint>();
        listPoint = new List<GameObject>();
        GameObject points = new GameObject("Points");
        points.transform.position = transform.position;
        points.transform.parent = transform;

        for(int i=0;i<10000;i++)
        {
            float randX = Random.Range(-radius, radius);
            float randY = Random.Range(-radius, radius);
            float randZ = Random.Range(-radius, radius);

            float hipotenuza = Mathf.Sqrt(randX*randX+randY*randY+randZ*randZ);

            if(hipotenuza<=radius)
            {
                //float angleX = Mathf.Sin(randX/hipotenuza/57.3f);
                //float angleY = Mathf.Sin(randY/hipotenuza/57.3f);
                //float angleZ = Mathf.Sin(randZ/hipotenuza/57.3f);

                listPoint.Add(Instantiate(point));
                listModelPoint.Add(new ModelPoint());
                listPoint[countPoint].transform.parent = points.transform;

                listModelPoint[countPoint].randX = randX;
                listModelPoint[countPoint].randY = randY;
                listModelPoint[countPoint].randZ = randZ;
                
                listPoint[countPoint].transform.position = new Vector3(
                    transform.position.x+randX, 
                    transform.position.y+randY, 
                    transform.position.z+randZ);

                //listPoint[countPoint].transform.rotation = Quaternion.Euler();
                countPoint++;
            }
        }
    }

    void Update()
    {
        MovePoint();
    }

    void MovePoint()
    {
        for(int i=0;i<listPoint.Count;i++)
        {
            //listModelPoint[i].hipotenuza = Mathf.Sqrt(
                //(listModelPoint[i].randX)*(listModelPoint[i].randX)+
                //(listModelPoint[i].randY)*(listModelPoint[i].randY)+
                //(listModelPoint[i].randZ)*(listModelPoint[i].randZ));
            

            //if(listModelPoint[i].hipotenuza<=radius)
            //{
                //Debug.Log("hipotenuza");
                if(listModelPoint[i].eventMove==false)
                {
                    if(listModelPoint[i].numStep==0)
                    {
                        listModelPoint[i].numStep = Random.Range(1, 10);
                        for(int j=0;j<listModelPoint[i].numStep;j++)
                        {
                            listModelPoint[i].randX = Random.Range(-radiusMove, radiusMove);
                            listModelPoint[i].randY = Random.Range(-radiusMove, radiusMove);
                            listModelPoint[i].randZ = Random.Range(-radiusMove, radiusMove);
                            listModelPoint[i].listStep.Add(new Point(
                                listModelPoint[i].randX,
                                listModelPoint[i].randY,
                                listModelPoint[i].randZ));
                        }
                        listModelPoint[i].numStep=0;
                    }

                    listPoint[i].transform.position = new Vector3(
                        listPoint[i].transform.position.x+listModelPoint[i].listStep[listModelPoint[i].numStep].x, 
                        listPoint[i].transform.position.y+listModelPoint[i].listStep[listModelPoint[i].numStep].y, 
                        listPoint[i].transform.position.z+listModelPoint[i].listStep[listModelPoint[i].numStep].z);

                    listModelPoint[i].numStep++;
                    if(listModelPoint[i].listStep.Count==listModelPoint[i].numStep)
                    {
                        listModelPoint[i].numStep=0;
                        listModelPoint[i].eventMove=true;
                    }
                }
                else
                {
                    listPoint[i].transform.position = new Vector3(
                        listPoint[i].transform.position.x-listModelPoint[i].listStep[listModelPoint[i].numStep].x, 
                        listPoint[i].transform.position.y-listModelPoint[i].listStep[listModelPoint[i].numStep].y, 
                        listPoint[i].transform.position.z-listModelPoint[i].listStep[listModelPoint[i].numStep].z);
                    
                
                    //listModelPoint[i].randX = 1;
                    //listModelPoint[i].randY = 1;
                    //listModelPoint[i].randZ = 1;
                    
                    listModelPoint[i].numStep++;
                    if(listModelPoint[i].listStep.Count==listModelPoint[i].numStep)
                    {
                        listModelPoint[i].numStep=0;
                        listModelPoint[i].listStep.Clear();
                        listModelPoint[i].eventMove=false;
                    }
                }
            //}
        }
    }
}
