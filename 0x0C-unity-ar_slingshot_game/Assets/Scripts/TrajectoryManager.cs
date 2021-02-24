using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryManager : MonoBehaviour
{
    public InputManager inputManager;

    private LineRenderer lr;

    private float simDuration = 5f;
    private float planeHeight = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        planeHeight = inputManager.plane.center.y;
    }

    public void Draw(Vector3 position, Vector3 velocity)
    {
        lr.enabled = true;
        
        List<Vector3> linePositions = new List<Vector3>();
        linePositions.Add(position);
        float timeStep = Time.fixedDeltaTime;
    
        for (float i = 0f; i < simDuration; i += timeStep)
        {
            velocity += Physics.gravity * timeStep;
            position += velocity * timeStep;
            linePositions.Add(position);
        }

        lr.positionCount = linePositions.Count;
        lr.SetPositions(linePositions.ToArray());
        
        Debug.Log("Drawing lr");
    }

    public void Disable()
    {
        lr.enabled = false;
    }
}
