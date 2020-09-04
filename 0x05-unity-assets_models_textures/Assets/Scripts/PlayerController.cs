using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody body;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        movement = Vector3.zero;
        
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        if (movement != Vector3.zero)
            transform.forward = movement;
    }

    private void FixedUpdate() {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
    }
}
