using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cube : MonoBehaviour
{

    // Use this for initialization
    private MeshRenderer meshRenderer;

    public List<GameObject> neighbors;

    private int counter;
    private int speed;

    public void setNeighbours(List<GameObject> neighboursList)
    {
        neighbors = neighboursList;
    }

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = UnityEngine.Random.Range(0, 2) == 1;
        counter = 0;
        speed = 5;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            speed--;
        }

        if (counter < speed)
        {
            counter++;
            return;
        }

        counter = 0;
        //if (Input.GetKeyUp(KeyCode.M))
        //{
            int aliveMinimum = 1;
            int aliveMaximum = 2;
            int deadMinimum = 4;
            int deadMaximum = 4;

            int numNeighbours = getNumNeighbours(neighbors);

            if (meshRenderer.enabled == false)
            {
                if (deadMinimum <= numNeighbours && numNeighbours <= deadMaximum)
                {
                    meshRenderer.enabled = true;
                }
                else
                {
                    meshRenderer.enabled = false;
                }
            }
            else
            {
                if (aliveMinimum <= numNeighbours && numNeighbours <= aliveMaximum)
                {
                    meshRenderer.enabled = true;
                }
                else
                {
                    meshRenderer.enabled = false;
                }
            }

            //meshRenderer.enabled = Random.Range(0, 2) == 1;
        //}
    }

    private int getNumNeighbours(List<GameObject> neighbors)
    {
        int numNeighbours = 0;
        foreach (var neighbour in neighbors)
        {
            if (neighbour.GetComponent<Cube>().meshRenderer.enabled == true)
            {
                numNeighbours++;
            }
        }
        return numNeighbours;
    }

    void Awake()
    {
// Debug.Log("Awake called.");
    }


}
