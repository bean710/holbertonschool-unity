using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class InputManager : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager arRaycastManager;
    public EnemyManager enemyManager;

    public UIManager uiManager;

    public ARPlane plane = null;

    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    private List<ARPlane> planes = new List<ARPlane>();

    private bool planeSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (planeSelected)
            return;

        if (Input.touchCount <= 0)
            return;

        Touch touch = Input.GetTouch(0);
        //Debug.Log("Detected Touch");
        if (arRaycastManager.Raycast(touch.position, m_Hits))
        {
            //Debug.Log("Hit Something");
            ARRaycastHit hit = m_Hits[0];
            if ((hit.hitType & TrackableType.PlaneWithinPolygon) != 0)
            {
                Debug.Log("First hit was plane");
                plane = arPlaneManager.GetPlane(hit.trackableId);

                foreach (ARPlane onPlane in arPlaneManager.trackables)
                {
                    if (onPlane != plane)
                    {
                        Debug.Log("Removing secondary plane");
                        onPlane.gameObject.SetActive(false);
                    }
                }

                planeSelected = true;

                LineRenderer lineRenderer = plane.gameObject.GetComponent<LineRenderer>();

                Debug.Log(lineRenderer);

                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;

                Debug.Log("Updated Line render");

                arPlaneManager.enabled = false;
                uiManager.PlaneSelected(plane);
            }
        }
    }
}
