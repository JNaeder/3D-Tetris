using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int width = 10;
    public static int depth = 10;
    public static int height = 15;

    public static Transform[,,] grid = new Transform[width, height, depth];


    public GameObject[] pieces;
    public Transform spawnPos;

	// Use this for initialization
	void Start () {
        SpawnNewPiece();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public bool IsInsideGrid(Vector3 pos) {
        Vector3 newPos = Round(pos);
        return ((int)newPos.x >= 0 && (int)newPos.x < width && (int)newPos.z >= 0 && (int)newPos.z < depth && (int)newPos.y >= 0);    
    }


    public Vector3 Round(Vector3 pos) {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));

    }


    public void SpawnNewPiece() {
        int randNum = Random.Range(0, pieces.Length);

        Instantiate(pieces[randNum], spawnPos.position, Quaternion.identity);

    }


    public void UpdateGrid(Piece piece) {
        for (int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++){
                for (int z = 0; z < depth; z++) {
                    if (grid[x, y, z] != null) {
                        if (grid[x, y, z].parent == piece.transform) {
                            grid[x, y, z] = null;
                        }
                    }

                }
        }
        }


        foreach (Transform block in piece.transform) {
            Vector3 newPos = Round(block.position);
            if (newPos.y < height)
            {
                grid[(int)newPos.x, (int)newPos.y, (int)newPos.z] = block;
            }
        }



    }



    public void CheckIfAnyRowsAreFull() {
        for (int y = 0; y < height; y++) {
            if (IsRowFull(y)) {
                Debug.Log("Delete Row " + y);
                DeleteRow(y);
                MoveAllRowsDown(y);
                y--;
            }
        }

    }


    public bool IsRowFull(int y) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                if (grid[x, y, z] == null) {
                    return false;
                }
            }
        }
        return true;
    }



    void DeleteRow(int y) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    void MoveRowDown(int y) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                if (grid[x, y, z] != null) {
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;
                    grid[x, y - 1, z].position += Vector3.down;

                }
            }
        }

    }

    void MoveAllRowsDown(int y) {
        for (int i = y; y < height; y++) {
            MoveRowDown(y);
        }

    }
}
