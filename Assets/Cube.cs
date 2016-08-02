using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cube : MonoBehaviour
{
	public GameManager world;

	public int aliveMinimum;
	public int aliveMaximum;
	public int deadMinimum;
	public int deadMaximum;

    private int counter;
    private int speed;

	public int x;
	public int y;
	public int z;

	public void setCoordinate(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

    // Use this for initialization
    void Start()
    {
        counter = 0;
        speed = 5;
    }
		
    // Update is called once per frame
    void Update()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			GetComponent<Renderer> ().material.color = Color.green;
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			GetComponent<Renderer> ().material.color = Color.blue;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			speed--;
		}
//
//		if (counter < speed) {
//			counter++;
//			return;
//		}
//		counter = 0;

	if (Input.GetKeyUp(KeyCode.M))
	{

		int numLivingNeighbours = getNumLivingNeighbours();

		if (!isAlive()) {
			if (deadMinimum <= numLivingNeighbours && numLivingNeighbours <= deadMaximum) {
				becomeAlive ();
			} else {
				becomeDead ();
			}
		} else {
			if (aliveMinimum <= numLivingNeighbours && numLivingNeighbours <= aliveMaximum) {
				becomeAlive ();
			} else {
				becomeDead ();
			}
		}

		if (isAlive ()) {
			world.makeSureNeighborsExist (x,y,z);
		} else if (tooFarAwayToCare (numLivingNeighbours)) {
			removeMeFromGame ();
		}
	}
    }

	public bool isAlive ()
	{
		return GetComponent<MeshRenderer>().enabled == true;
	}

	public void becomeAlive ()
	{
		GetComponent<MeshRenderer>().enabled = true;
	}

	public void becomeDead ()
	{
		GetComponent<MeshRenderer>().enabled = false;
	}

	bool tooFarAwayToCare (int numLivingNeighbours)
	{
		return numLivingNeighbours == 0;
	}

	void removeMeFromGame ()
	{
		world.removeMe (x, y, z);
		Destroy (this.gameObject);
	}

    private int getNumLivingNeighbours()
    {
		return world.GetLivingNeighbours (x, y, z).Count;
    }

}
