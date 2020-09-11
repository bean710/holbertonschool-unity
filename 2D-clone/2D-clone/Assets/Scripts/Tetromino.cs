using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float speed = 1;

    private float fall = 0;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && Time.time - fall >= speed)
        {
            Vector3 oldPos = transform.position;

            transform.position += new Vector3(0, -1, 0);

            if (!Playfield.CheckPos(transform))
            {
                transform.position = oldPos;
                Playfield.AddToPlayspace(transform);
                moving = false;
            }

            fall = Time.time;
        }
    }
}
