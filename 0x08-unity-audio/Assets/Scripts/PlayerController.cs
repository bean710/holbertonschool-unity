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

        if (Input.GetButtonDown("Jump") && cols > 0)
        {
            anim.SetBool("Jumping", true);
            body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }

        anim.SetBool("Falling", body.velocity.y < -12);

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
        if (transform.position.y < -40)
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
