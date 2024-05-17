using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementSpace : MonoBehaviour {
    public float speed = 1f;
    public float rotationSpeed = 0.1f;
    public float fuelUsage = 1f;
    public float fuelSeconds = 100f;
    public float maxFuelSeconds = 100f;
    public Image fuelBar;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        if (Mathf.Abs(movementY) > 0) {
            fuelUsage = (Input.GetKey(KeyCode.LeftShift) ? 6f : 3f);
        } else {
            fuelUsage = 1f;
        }

        if (Mathf.Abs(movementX) > 0 && Mathf.Abs(movementY) > 0) {
            transform.Rotate(0, 0, -movementX * rotationSpeed);
        } else {
            transform.Rotate(Vector3.zero);
            rigidBody.angularVelocity = 0;
        }
        rigidBody.velocity = transform.up * speed * movementY * (Input.GetKey(KeyCode.LeftShift) ? 2 : 1);
        fuelSeconds -= fuelUsage * Time.deltaTime;
        fuelBar.fillAmount = fuelSeconds / maxFuelSeconds;
    }
}
