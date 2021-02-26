using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotManager : MonoBehaviour
{
    public GameObject camera;
    public GameObject ammoPrefab;
    public TrajectoryManager trajectoryManager;
    public UIManager uiManager;
    public EnemyManager enemyManager;

    public float verticalVelocity = 10f;
    public float horizontalVelocity = 3f;

    private GameObject ammo;
    private Rigidbody ammoRB;
    private Ammo SAmmo;

    private Vector2 initialTouchPos;
    private Vector3 originalPos;

    private bool dragging = false;

    public int ammoCount = 7;

    void Start()
    {
        Time.timeScale = 0.5f;
    }

    public void LoadAmmo()
    {
        if (ammoCount == 0)
            return;

        ammoCount--;

        Debug.Log("Loading Ammo");
        ammo = Instantiate(ammoPrefab, camera.transform);
        SAmmo = ammo.GetComponent<Ammo>();
        SAmmo.slingshotManager = this;
        SAmmo.plane = enemyManager.plane;
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
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(touch.position);

            //Debug.DrawRay(ray.origin, ray.direction, Color.white, 5f);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit something");
                if (hit.transform.tag != "Ammo")
                    return;

                //Debug.Log("Hit ammo");
                dragging = true;
                initialTouchPos = touch.position;
            }
        }
        else if (touch.phase == TouchPhase.Moved && dragging)
        {
            ammo.transform.localPosition = originalPos + (((camera.transform.up) * ((touch.position.y / Screen.height) - 0.5f) / 2));
            Vector3 newVelocity = GetVelocity(touch);
            trajectoryManager.Draw(ammo.transform.position, newVelocity);
        }
        else if (touch.phase == TouchPhase.Ended && dragging)
        {
            SAmmo.Fired();
            ammoRB.constraints = RigidbodyConstraints.None;
            Vector3 newVelocity = GetVelocity(touch);
            ammoRB.velocity = newVelocity;
            dragging = false;
            trajectoryManager.Disable();
            uiManager.UseAmmo(ammoCount + 1);
            StartCoroutine(Reload(2f));
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
            //Debug.Log("Not Dragging");
        }
    }

    Vector3 GetVelocity(Touch touch)
    {
        float stretchModifier = (((initialTouchPos.y - touch.position.y) / Screen.height) - 0.5f);
        Vector3 direction = (-camera.transform.up * verticalVelocity * stretchModifier * 1f)
                + (-camera.transform.forward * horizontalVelocity * stretchModifier * 2f);
        //Vector3 finalVelocity = direction * stretchModifier;
        
        return (direction);
    }

    public void HitTarget(GameObject enemy)
    {
        uiManager.ScorePoints(10);
        enemyManager.HitEnemy(enemy);
    }

    IEnumerator Reload(float delay)
    {
        yield return new WaitForSeconds(delay);

        LoadAmmo();
    }

    public void Replay()
    {
        ammoCount = 7;
    }

    public void CheckOutOfAmmo()
    {
        if (ammoCount == 0)
            uiManager.GameOver();
    }
}
