using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Cam;
    public float Speed = 6f;
    public float JumpHeight = 2f;
    public float RotateSpeed = 2f;

    public GameObject PlayerModel;

    private Rigidbody body;
    private Vector3 movement;

    private uint cols = 0;

    private bool paused = false;

    private CameraController cameraController;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        cameraController = Cam.GetComponent<CameraController>();
        anim = PlayerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            Move();
            CheckFall();
        }
    }

    private void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 newMove = new Vector3(0, 0, 0);

        float totSpeed = Mathf.Max(Mathf.Abs(Horizontal), Mathf.Abs(Vertical));

        if (totSpeed > 0)
        {
            float direction = Mathf.Atan2(Horizontal, Vertical) * Mathf.Rad2Deg;

            Quaternion camRot = Cam.transform.rotation;
            Quaternion newRotation = Quaternion.identity;

            newRotation.eulerAngles = new Vector3(0, camRot.eulerAngles.y + direction, 0);
            transform.rotation = newRotation;
            newMove = transform.forward * totSpeed;

            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        /**
        if (Vertical != 0f)
        {
            Quaternion newRotation = Cam.transform.rotation;

            if (Horizontal != 0)
            {
                float lrRotation = 0f;
                lrRotation = (Horizontal > 0 ? 1 : -1);
                float fbRotation = (Vertical > 0 ? 0 : 1);
                float dir = ((newRotation.eulerAngles.y) * 2 + 90 * lrRotation) / 2;
                newRotation.eulerAngles = new Vector3(0, dir, 0);
                transform.rotation = newRotation;
                newMove = transform.forward * Mathf.Abs(Vertical);
            }
            else
            {
                newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y + 180 * (Vertical > 0 ? 0 : 1), 0);
                transform.rotation = newRotation;
                newMove = transform.forward * Mathf.Abs(Vertical);
            }
        }
        else if (Horizontal != 0f)
        {
            Quaternion newRotation = Cam.transform.rotation;
            float dir = (Horizontal > 0 ? 1 : -1) * 90 + newRotation.eulerAngles.y;
            newRotation.eulerAngles = new Vector3(0, dir, 0);
            transform.rotation = newRotation;
            newMove = transform.forward * Mathf.Abs(Horizontal);
        }
        **/

        /**
        Quaternion newRotation = Cam.transform.rotation;
        newRotation.z = 0;
        newRotation.x = 0;

        transform.rotation = newRotation;
        **/

        //transform.Rotate(0f, Horizontal * RotateSpeed, 0f);

        //Vector3 newMove = transform.right * Horizontal + transform.forward * Vertical;

        if (Input.GetButtonDown("Jump") && cols > 0)
        {
            body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        body.MovePosition(body.position + newMove * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        cols += 1;
    }

    private void OnCollisionExit(Collision other) {
        cols -= 1;
    }

    private void CheckFall()
    {
        if (transform.position.y < -30)
        {
            transform.position = new Vector3(0, 50, 0);
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        cameraController.TogglePause();
    }
}
