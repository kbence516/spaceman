using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingX : MonoBehaviour {
    public const int LEFT = -1;
    public const int RIGHT = 1;

    public float offsetX = 0.5f;

    public bool hasALeftNeighbour = false;
    public bool hasARightNeighbour = false;

    public bool tileable = true;

    private float spriteWidth = 0f;
    private Camera mainCamera;
    private Transform myTransform;

    void Awake() {
        mainCamera = Camera.main;
        myTransform = transform;
    }


    // Start is called before the first frame update
    void Start() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = Mathf.Abs(spriteRenderer.sprite.bounds.size.x * myTransform.localScale.x);
    }

    // Update is called once per frame
    void Update() {
        if (!hasARightNeighbour || !hasALeftNeighbour) {
            float camHorizontalExtend = mainCamera.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            if (mainCamera.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightNeighbour) {
                NewTile(RIGHT);
                hasARightNeighbour = true;
            } else if (mainCamera.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftNeighbour) {
                NewTile(LEFT);
                hasALeftNeighbour = true;
            }
        }
    }

    void NewTile(int direction) {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * direction,
                                           myTransform.position.y, myTransform.position.z);

        Transform newTile = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (!tileable) {
            newTile.localScale = new Vector3(newTile.localScale.x * -1,
                                               newTile.localScale.y,
                                               newTile.localScale.z);
        }

        newTile.parent = myTransform.parent;
        if (direction == RIGHT) {
            newTile.GetComponent<TilingX>().hasALeftNeighbour = true;
        } else {
            newTile.GetComponent<TilingX>().hasARightNeighbour = true;
        }
    }
}
