using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    private int count;

    public bool isMobileBuild = true;

    public Text accText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        if (isMobileBuild)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (isMobileBuild)
        {
            Vector3 a = UnityEngine.InputSystem.Accelerometer.current.acceleration.ReadValue();
            accText.text = "Accelerometer: " + a.ToString("F6");
            movement = new Vector3(a.x, 0.0f, a.y);
        }

        else
        {
            movement = new Vector3(movementX, 0.0f, movementY);
        }

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }
}