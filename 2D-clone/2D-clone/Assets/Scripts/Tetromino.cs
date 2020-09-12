using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float speed = 1;
    public bool canRotate = true;
    public bool isGhost = false;
    
    private float fall = 0;
    private bool moving = false;

    private Tetromino ghost = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!isGhost)
        {
            UpdateGhost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && !isGhost)
        {
            if (Time.time - fall >= 1 / speed)
            {
                Vector3 oldPos = transform.position;

                transform.position += new Vector3(0, -1, 0);

                if (!Playfield.CheckPos(transform))
                {
                    transform.position = oldPos;
                    Playfield.AddToPlayspace(transform);
                    moving = false;

                    Destroy(ghost.transform.gameObject);
                    return;
                }

                fall = Time.time;
            }

            if (Input.GetKeyDown("left"))
            {
                Vector3 oldPos = transform.position;

                transform.position += new Vector3(-1, 0, 0);

                if (!Playfield.CheckPos(transform))
                {
                    transform.position = oldPos;
                }

                UpdateGhost();
            }
            else if (Input.GetKeyDown("right"))
            {
                Vector3 oldPos = transform.position;

                transform.position += new Vector3(1, 0, 0);

                if (!Playfield.CheckPos(transform))
                {
                    transform.position = oldPos;
                }

                UpdateGhost();
            }
            else if (Input.GetKeyDown("space"))
            {
                Vector3 oldPos = transform.position;

                while (Playfield.CheckPos(transform))
                {
                    oldPos = transform.position;
                    transform.position += new Vector3(0, -1, 0);
                }

                transform.position = oldPos;
                Playfield.AddToPlayspace(transform);
                moving = false;

                Destroy(ghost.transform.gameObject);
                return;
            }
            else if (canRotate && Input.GetKeyDown("up"))
            {
                transform.Rotate(0, 0, 90);

                if (!Playfield.CheckPos(transform))
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        child.Rotate(0, 0, -90);
                    }

                    UpdateGhost();
                }
            }
            else if (Input.GetKeyDown("down"))
            {
                speed *= 2;
            }

            if (Input.GetKeyUp("down"))
            {
                speed /= 2;
            }
        }
    }

    public void UpdateGhost()
    {
        Debug.Log($"Updating as ghost: {(ghost ? "true" : "false")}");
        if (moving)
        {
            if (ghost == null)
            {
                Debug.Log("New ghost");
                ghost = Instantiate(this, transform.position, transform.rotation);
                ghost.isGhost = true;
                foreach (Transform child in ghost.transform)
                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }

            ghost.transform.position = transform.position;
            ghost.transform.rotation = transform.rotation;
            Vector3 oldPos = ghost.transform.position;

            while (Playfield.CheckPos(ghost.transform))
            {
                oldPos = ghost.transform.position;
                ghost.transform.position += new Vector3(0, -1, 0);
            }

            ghost.transform.position = oldPos;
        }
    }

    public void Enable()
    {
        moving = true;
        UpdateGhost();
    }
}
