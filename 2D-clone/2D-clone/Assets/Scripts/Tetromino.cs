using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float speed = 1;

    private float fall = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - fall >= speed)
        {
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;
        }
    }
}
