using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] playspace = new Transform[w, h];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddToPlayspace(Transform tm)
    {
        Vector2 r;

        foreach (Transform child in tm)
        {
            r = RoundV2(child.position);
            playspace[(int)r.x, (int)r.y] = child;
        }
    }

    public static bool CheckPos(Transform tm) {
        Vector2 r;

        foreach (Transform child in tm)
        {
            r = RoundV2(child.position);
            if (!inField(r) || playspace[(int)r.x, (int)r.y] != null)
            {
                Debug.Log("Invalid spot");;
                return false;
            }
        }

        return true;
    }

    // Checks if a position is in the field
    public static bool inField(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    // Rounds a 2D vector (position) to the nearest whole coordinates
    public static Vector2 RoundV2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
