using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float speed = 1.0f;
    public Transform targetCharacter;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start() {
        offset = targetCharacter.position - transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (targetCharacter) {
            Vector3 anchorPos = transform.position + offset;
            Vector3 movement = targetCharacter.position - anchorPos;
            Vector3 newCamPos = transform.position + movement * speed * Time.deltaTime;
            transform.position = newCamPos;
        }
    }
}
