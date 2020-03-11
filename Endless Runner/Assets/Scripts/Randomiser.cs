using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomiser : MonoBehaviour
{
    public string ObstalceTag = "Obstacle";

    public GameObject[] Obstacles;
    public float ObstacleSpawnRate = 0.2f;
    public GameObject[] Buildings;

    [Header("Spawn Locations")]
    public Transform[] buildingSpawnLocations;
    public Transform[] obstacleSpawnLocations;

    public Transform buildingPool;
    public Transform obstaclePool;

    int DecimalPlaces = 10000;
    public bool ObstaclesEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (ObstaclesEnabled)
        {
            //Spawn an obstacle
            float obstacleRandomiser = (float)((float)Random.Range(0, DecimalPlaces) / (float)DecimalPlaces);
            if (obstacleRandomiser <= ObstacleSpawnRate)
            {
                SpawnObstacle();
            }
        }

        //Spawn a building
        SpawnBuilding();
    }

    public void SpawnObstacle()
    {
        //Select an obstacle
        int obstacleIndex = Random.Range(0, Obstacles.Length);
        int lane = Random.Range(0, obstacleSpawnLocations.Length);
        InstantiatePrefab(Obstacles[obstacleIndex], obstacleSpawnLocations[lane].position, obstaclePool, obstacleSpawnLocations[lane]).tag = ObstalceTag;
    }

    public void SpawnBuilding()
    {
        //Spawn a building and randomise it
        int buildingRandomiser = Random.Range(0, Buildings.Length);
        InstantiatePrefab(Buildings[buildingRandomiser], buildingSpawnLocations[0].position, buildingPool, buildingSpawnLocations[0]).GetComponent<BuildingRandomiser>().Randomise();

        //Spawn a building and randomise it
        int buildingRandomiser1 = Random.Range(0, Buildings.Length);
        InstantiatePrefab(Buildings[buildingRandomiser1], buildingSpawnLocations[1].position, buildingPool, buildingSpawnLocations[1]).GetComponent<BuildingRandomiser>().Randomise();
    }

    public GameObject InstantiatePrefab(GameObject obj, Vector3 position, Transform parent = null, Transform target = null)
    {
        //Clone the object
        GameObject newObj = Instantiate(obj);
        newObj.name = obj.name;
        newObj.transform.position = position;

        //Apply the target transform
        if (target != null)
        {
            newObj.transform.localRotation *= target.localRotation;
            newObj.transform.localScale = new Vector3(target.localScale.x * newObj.transform.localScale.x, target.localScale.y * newObj.transform.localScale.y, target.localScale.z * newObj.transform.localScale.z);
        }

        //Parent it
        if (parent != null)
        {
            newObj.transform.SetParent(parent);
        }
        return newObj;
    }
}
