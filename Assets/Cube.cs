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

    public  int aliveMinimum = 1;
    public int aliveMaximum = 2;
    public int deadMinimum = 4;
    public int deadMaximum = 4;

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
        InvokeRepeating ("checkLife", .2f + UnityEngine.Random.Range (.01f, .1f), .2f + UnityEngine.Random.Range (.01f, .1f));
    }

    public void checkLife() {
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
