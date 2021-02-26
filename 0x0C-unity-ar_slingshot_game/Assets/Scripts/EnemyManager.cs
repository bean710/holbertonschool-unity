using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemyManager : MonoBehaviour
{
    public UIManager uiManager;
    
    public GameObject enemyPrefab;

    public int enemyNum = 5;
    public int activeEnemies = 5;

    public ARPlane plane = null;
    private float maxDimension;
    private float minDimension;

    private List<GameObject> enemies = new List<GameObject>();

    public void SetupPlane(ARPlane newPlane)
    {
        plane = newPlane;

        maxDimension = Mathf.Max(plane.size.x, plane.size.y);
        minDimension = Mathf.Min(plane.size.x, plane.size.y);
        
        for (int i = 0; i < enemyNum; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab, plane.center + (new Vector3(0, 0.05f, 0)), Quaternion.identity).GetComponent<Enemy>();
            enemy.em = this;

            enemies.Add(enemy.gameObject);

            float adjustedScale = minDimension / 10f;
            
            enemy.transform.localScale = new Vector3(adjustedScale, adjustedScale, adjustedScale);
            enemy.scale = minDimension / 10f;
            

            Debug.Log("Spawning in Enemy");
        }
    }

    public Vector3 GetDestination()
    {
        Vector2 newPos2d = new Vector2(plane.center.x, plane.center.z) + Random.insideUnitCircle * maxDimension;
        Vector3 newPos = new Vector3(newPos2d.x, plane.center.y, newPos2d.y);
        RaycastHit hit;

        while (!Physics.Raycast(newPos, Vector3.down, out hit, Mathf.Infinity))
        {
            newPos2d = new Vector2(plane.center.x, plane.center.z) + Random.insideUnitCircle * maxDimension;
            newPos = new Vector3(newPos2d.x, plane.center.y, newPos2d.y) + (Vector3.up * 10f);
            /*
            Debug.Log("Getting destination");
            Debug.Log($"Center: {plane.center}, Size: {plane.size}");
            Debug.Log($"Position: {newPos}");
            */
        }

        newPos = new Vector3(newPos.x, plane.center.y, newPos.z);

        return (newPos);
    }

    public void HitEnemy(GameObject enemy)
    {
        activeEnemies--;
        Destroy(enemy);

        if (activeEnemies == 0)
            uiManager.GameOver();
    }

    public void Replay()
    {
        foreach (GameObject enemy in enemies)
            Destroy(enemy);

        enemies.Clear();

        activeEnemies = enemyNum;
    }
}
