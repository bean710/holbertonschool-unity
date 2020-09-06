using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Cam;
    public float Speed = 6f;
    public float JumpHeight = 2f;

    private Rigidbody body;
    private Vector3 movement;

    private uint cols = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckFall();
    }

    private void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Quaternion newRotation = Cam.transform.rotation;
        newRotation.z = 0;
        newRotation.x = 0;

        transform.rotation = newRotation;

        Vector3 newMove = transform.right * Horizontal + transform.forward * Vertical;

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
}
