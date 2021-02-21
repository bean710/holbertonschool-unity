using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject ammoPrefab;

    private GameObject ammo;
    private Rigidbody ammoRB;

    private Vector2 initialTouchPos;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        ammo = Instantiate(ammoPrefab, camera.transform);
        ammoRB = ammo.GetComponent<Rigidbody>();

        ammo.transform.rotation.SetLookRotation(camera.transform.up, -camera.transform.forward);
        ammoRB.constraints = RigidbodyConstraints.FreezeAll;

        originalPos = ammo.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;

            if (Physics.Raycast(touch.position, camera.transform.forward, out hit))
            {
                initialTouchPos = touch.position;
            }
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            ammo.transform.localPosition = originalPos + ((camera.transform.up * 2) * ((touch.position.y / Screen.height) - 0.5f) * 1.2f);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            ammoRB.constraints = RigidbodyConstraints.None;
            ammoRB.velocity = ((camera.transform.up + camera.transform.forward) * 10f * ((touch.position.y - initialTouchPos.y) / Screen.height));
            //ammo.transform.localPosition = originalPos;
        }
        else if (touch.phase == TouchPhase.Canceled)
        {

        }
    }
}
