using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    public int size;
    public GameObject globalCube;


	private Dictionary<string, GameObject> cellIdToCellMap = new Dictionary<string, GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
					createCube (i, j, k, Random.Range(0,5) == 1);
                }
            }
        }
		List<GameObject> cells = new List<GameObject>(cellIdToCellMap.Values);
		foreach (var gameObject in cells) {
			var cube = gameObject.GetComponent<Cube> ();
			if (cube.isAlive ()) {
				makeSureNeighborsExist (cube.x, cube.y, cube.z);
			}
		}
    }

	GameObject createCube (int x, int y, int z, bool isAlive)
	{
		GameObject thisCube = (GameObject)Instantiate (globalCube);
		thisCube.transform.position = new Vector3 (x, y, z);

		var thisCubeAsACube = thisCube.GetComponent<Cube> ();
		thisCubeAsACube.setCoordinate (x, y, z);
		thisCubeAsACube.enabled = true;
		if (isAlive) {
			thisCubeAsACube.becomeAlive ();
		}
		else {
			thisCubeAsACube.becomeDead ();
		}

		cellIdToCellMap.Add (getHashOfIndex (x,y,z), thisCube);
		return thisCube;
	}

	string getHashOfIndex (int i, int j, int k)
	{
		return i + "," + j + "," + k;
	}

	public List<GameObject> GetLivingNeighbours(int x, int y, int z)
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
					GameObject potentialNeighbour = null;
					if (cellIdToCellMap.TryGetValue(getHashOfIndex (x + i, y + j, z + k), out potentialNeighbour)) {
						if (potentialNeighbour.GetComponent<Cube> ().isAlive ()) {
							neighbours.Add (potentialNeighbour);
						}
					}
                }
            }
        }
		return neighbours;
    }

	public void makeSureNeighborsExist (int x, int y, int z)
	{
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

					if (!cellIdToCellMap.ContainsKey(getHashOfIndex (x + i, y + j, z + k))) {
						cellIdToCellMap [getHashOfIndex (x + i, y + j, z + k)] = createCube (x + i, y + j, z + k, false);
					}
				}
			}
		}
	}

	public void removeMe (int x, int y, int z)
	{
		cellIdToCellMap.Remove(getHashOfIndex(x,y,z));
	}
}
