using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Ammo : MonoBehaviour
{
    public SlingshotManager slingshotManager;
    public ARPlane plane;

    private bool canHitTarget = false;
    private bool fired = false;
    
    public void Fired()
    {
        canHitTarget = true;
        fired = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fired && transform.position.y < plane.center.y)
            StartCoroutine(Despawn(1f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canHitTarget || !fired)
            return;

        if (collision.gameObject.tag == "Target")
        {
            slingshotManager.HitTarget(collision.gameObject);
            StartCoroutine(Despawn(0.1f));
        }
        else if (collision.gameObject.tag == "Plane")
        {
            StartCoroutine(Despawn(1f));
        }
    }

    IEnumerator Despawn(float delay)
    {
        canHitTarget = false;
        yield return new WaitForSeconds(delay);

        Debug.Log("Despawning ammo");
        Destroy(gameObject);

        slingshotManager.CheckOutOfAmmo();
    }
}
