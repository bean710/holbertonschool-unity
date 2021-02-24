using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject ammoPrefab;
    public TrajectoryManager trajectoryManager;
    public UIManager uiManager;

    public float verticalVelocity = 10f;
    public float horizontalVelocity = 3f;

    private GameObject ammo;
    private Rigidbody ammoRB;

    private Vector2 initialTouchPos;
    private Vector3 originalPos;

    private bool dragging = false;

    public int ammoCount = 5;

    public void LoadAmmo()
    {
        Debug.Log("Loading Ammo");
        ammo = Instantiate(ammoPrefab, camera.transform);
        ammoRB = ammo.GetComponent<Rigidbody>();

        ammo.transform.rotation.SetLookRotation(camera.transform.up, -camera.transform.forward);
        ammoRB.constraints = RigidbodyConstraints.FreezeAll;

        originalPos = ammo.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 1 || ammoCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(touch.position);

            //Debug.DrawRay(ray.origin, ray.direction, Color.white, 5f);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit something");
                if (hit.transform.tag != "Ammo")
                    return;

                Debug.Log("Hit ammo");
                dragging = true;
                initialTouchPos = touch.position;
            }
        }
        else if (touch.phase == TouchPhase.Moved && dragging)
        {
            ammo.transform.localPosition = originalPos + ((camera.transform.up) * ((touch.position.y / Screen.height) - 0.5f));
            Vector3 newVelocity = GetVelocity(touch);
            trajectoryManager.Draw(ammo.transform.position, newVelocity);
        }
        else if (touch.phase == TouchPhase.Ended && dragging)
        {
            ammoRB.constraints = RigidbodyConstraints.None;
            Vector3 newVelocity = GetVelocity(touch);
            ammoRB.velocity = newVelocity;
            dragging = false;
            trajectoryManager.Disable();
            ammoCount--;
            //ammo.transform.localPosition = originalPos;
        }
        else if (touch.phase == TouchPhase.Canceled)
        {
            dragging = false;
            trajectoryManager.Disable();
        }
        else if (dragging)
        {
            Vector3 newVelocity = GetVelocity(touch);
            trajectoryManager.Draw(ammo.transform.position, newVelocity);
        }
        else
        {
            Debug.Log("Not Dragging");
        }
    }

    Vector3 GetVelocity(Touch touch)
    {
        Vector3 direction = (-camera.transform.up * verticalVelocity) + (-camera.transform.forward * horizontalVelocity);
        float stretchModifier = ((initialTouchPos.y - touch.position.y) / Screen.height) - 0.5f;
        Vector3 finalVelocity = direction * stretchModifier;
        
        return (finalVelocity);
    }
}
