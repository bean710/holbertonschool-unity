using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int levelNum = 0;
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] playspace = new Transform[w, h];

    public Tetromino[] Tetrominos;

    private static bool needNew = true;
    private static bool gameOver = false;
    private static int linesClearedThisLevel = 0;

    private static List<int> bag = new List<int>();
    private static Tetromino upNext = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && needNew)
        {
            NewTetrimino();
            needNew = false;
        }
    }

    // Get new tetrimino
    private void NewTetrimino()
    {
        int bagIndex, index;

        if (upNext == null)
        {
            if (bag.Count == 0)
            {
                for (int i = 0; i < Tetrominos.Length; i++)
                    bag.Add(i);
            }

            bagIndex = (int)Random.Range(0, bag.Count);
            index = bag[bagIndex];
            bag.RemoveAt(bagIndex);
            upNext = Instantiate(Tetrominos[index], new Vector3(-10, 14, 0), Quaternion.identity);
            upNext.speed = levelNum + 1;
        }

        upNext.transform.position = new Vector3(5, 17, 0);
        upNext.Enable();
        if (!CheckPos(upNext.transform))
        {
            Destroy(upNext.transform.gameObject);
            gameOver = true;
            Scoring.GameOver();
        }

        if (bag.Count == 0)
        {
            for (int i = 0; i < Tetrominos.Length; i++)
                bag.Add(i);
        }

        bagIndex = (int)Random.Range(0, bag.Count);
        index = bag[bagIndex];
        bag.RemoveAt(bagIndex);

        upNext = Instantiate(Tetrominos[index], new Vector3(-10, 14, 0), Quaternion.identity);
        upNext.speed = levelNum + 1;
    }
    
    public static void NextLevel()
    {
        ClearField();
        Scoring.IncLevel();
        linesClearedThisLevel = 0;
        levelNum += 1;
    }

    // Checks for a full row in the playspace and removes full rows
    public static void CheckRows()
    {
        int rowsCleared = 0;

        for (int i = 0; i < h; i++)
        {
            bool empty = false;
            for (int j = 0; j < w; j++)
            {
                if (playspace[j, i] == null)
                {
                    empty = true;
                    break;
                }
            }

            if (!empty)
            {
                //Debug.Log("Found full row!");
                for (int y = i; y < h; y++)
                {
                    bool nonEmpty = false;

                    for (int x = 0; x < w; x++)
                    {
                        if (playspace[x, y] != null)
                        {
                            if (y == i)
                                Destroy(playspace[x, y].gameObject);
                        }
                        /**
                        else
                            nonEmpty = true;
                        **/

                        if (y + 1 < h)
                            playspace[x, y] = playspace[x, y + 1];
                        else
                            playspace[x, y] = null;

                        if (playspace[x, y] != null)
                            playspace[x, y].position += new Vector3(0, -1, 0);
                    }

                    if (nonEmpty)
                        break;
                }

                rowsCleared += 1;
                i--;
            }
        }

        switch (rowsCleared)
        {
            case 1:
                Scoring.AddPoints(40 * (levelNum + 1));
                break;
            case 2:
                Scoring.AddPoints(100 * (levelNum + 1));
                break;
            case 3:
                Scoring.AddPoints(300 * (levelNum + 1));
                break;
            case 4:
                Scoring.AddPoints(1200 * (levelNum + 1));
                break;
            default:
                break;
        }

        linesClearedThisLevel += rowsCleared;
        if (linesClearedThisLevel >= levelNum * 10 + 10)
        {
            NextLevel();
        }
    }

    // Adds a Tetromino to the playspace array
    public static void AddToPlayspace(Transform tm)
    {
        Vector2 r;

        foreach (Transform child in tm)
        {
            r = RoundV2(child.position);
            playspace[(int)r.x, (int)r.y] = child;
        }

        CheckRows();
        needNew = true;
    }

    // Checks a Tetromino against the playspace and boundaries
    public static bool CheckPos(Transform tm) {
        Vector2 r;

        foreach (Transform child in tm)
        {
            r = RoundV2(child.position);
            if (!inField(r) || playspace[(int)r.x, (int)r.y] != null)
            {
                //Debug.Log("Invalid spot");;
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

    public static void ClearField()
    {
        for (uint i = 0; i < h; i++)
        {
            for (uint j = 0; j < w; j++)
            {
                if (playspace[j, i] != null)
                {
                    Destroy(playspace[j, i].gameObject);
                    playspace[j, i] = null;
                }
            }
        }
    }

    // Rounds a 2D vector (position) to the nearest whole coordinates
    public static Vector2 RoundV2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
}
