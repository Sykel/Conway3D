using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    public int size;
    public GameObject globalCube;

    // Use this for initialization
    void Start()
    {
        GameObject[,,] cubeList = new GameObject[size, size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    GameObject thisCube = (GameObject)Instantiate(globalCube);
                    thisCube.transform.position = new Vector3(i, j, k);

                    cubeList[i, j, k] = thisCube;
                    //thisCube.GetComponent<Neighbour>();
                }
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    // Get neighbours
                    GetNeighbours(i, j, k, cubeList);

                }
            }
        }
    }

    private static void GetNeighbours(int x, int y, int z, GameObject[,,] cubeList)
    {
        List<GameObject> neighbours = new List<GameObject>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {

                    if (i == 0 && j == 0 && k == 0)
                    {
                        continue;
                    }
                    AddNeighbour(x + i, y + j, z + k, cubeList, neighbours);
                    //Debug.Log("Adding neighbour to " + x + " " + y + " " + z + " " + i + " " + j + " " + k);
                }
            }
        }
        cubeList[x, y, z].GetComponent<Cube>().setNeighbours(neighbours);
                    

    }

    private static void AddNeighbour(int i, int j, int k, GameObject[,,] cubeList, List<GameObject> neighbours)
    {

        try { neighbours.Add(cubeList[i, j, k]);
            //Debug.Log("I DID ADD A NEIGHBOUR");
        }
        catch { }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
