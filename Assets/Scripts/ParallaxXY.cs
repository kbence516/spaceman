using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxXY : MonoBehaviour {
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform mainCamera;
    private Vector3 previousCameraPosition;

    // Is called before Start(). Great for references.
    void Awake() {
        mainCamera = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start() {
        previousCameraPosition = mainCamera.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallaxX = (previousCameraPosition.x - mainCamera.position.x) * parallaxScales[i];
            float parallaxY = (previousCameraPosition.y - mainCamera.position.y) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,
                                                       backgroundTargetPosY,
                                                       backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,
                                                    backgroundTargetPos,
                                                    smoothing * Time.deltaTime);
        }

        previousCameraPosition = mainCamera.position;
    }
}
