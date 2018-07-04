using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    float newSpeed;
    float speed = 1f;

    GameManager gM;

	// Use this for initialization
	void Start () {
        gM = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();	
	}

    void Movement() {
        if (Input.GetKeyDown(KeyCode.D)) {
            transform.position += Vector3.right;
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else {
                transform.position += Vector3.left;

            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.position += Vector3.right;

            }

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward;
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.position += Vector3.back;

            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.back;
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.position += Vector3.forward;

            }

        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 90, 0);
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, -90, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(90, 0, 0);
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                transform.Rotate(-90, 0, 0);

            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time > newSpeed + speed)
        {
            transform.position += Vector3.down;
            if (IsValidPos())
            {
                gM.UpdateGrid(this);
            }
            else
            {
                gM.CheckIfAnyRowsAreFull();
                SpawnNextPiece();
                transform.position += Vector3.up;

            }


            newSpeed = Time.time;
        }
    }




    bool IsValidPos() {
        foreach (Transform block in transform) {
            Vector3 newPos = gM.Round(block.position);
            if (!gM.IsInsideGrid(block.position)) {
                return false;
            }
            if (newPos.y < GameManager.height)
            {
                if (GameManager.grid[(int)newPos.x, (int)newPos.y, (int)newPos.z] != null && GameManager.grid[(int)newPos.x, (int)newPos.y, (int)newPos.z].parent != transform)
                {
                    return false;
                }
            }
        }

        return true;

    }

    void SpawnNextPiece() {
        enabled = false;
        gM.SpawnNewPiece();

    }
}
