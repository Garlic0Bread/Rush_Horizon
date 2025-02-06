using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyPlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2;
    public float horizontalSpeed = 3;
    public float jumpForce = 5f; 
    private bool isGrounded = true;
    [SerializeField] AudioSource coinFx;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.Self);

        float xPosition = Mathf.Clamp(transform.position.x, -8.5f, 8.5f); // Adjust based on your ground size
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);

        float horizontalInput = Input.GetAxis("Horizontal"); // Supports both A/D and Left/Right Arrow keys
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime, Space.World);

        //jump functionality
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Player is in the air
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinFx.Play();
            Destroy(collision.gameObject);
        }
    }
}
