using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float sensitivity = 2.0f;

    public bool isInverted = false;

    private Vector3 offset;
    private float distance;

    private float curX = 0f;
    private float curY = 0f;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        distance = Vector3.Distance(transform.position, player.transform.position);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void LateUpdate() {
        if (paused)
            return;

        curX += Input.GetAxis("Mouse X") * sensitivity;
        curY += Input.GetAxis("Mouse Y") * sensitivity * (isInverted ? -1 : 1);

        //curX = Mathf.Clamp(curX, 89, -89);
        curY = Mathf.Clamp(curY, -7f, 89.9f);

        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(curY, curX, 0);
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position + rotation * direction, rotSpeed * Time.deltaTime);
        transform.position = player.transform.position + rotation * direction;

        transform.LookAt(player.transform.position);
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}
