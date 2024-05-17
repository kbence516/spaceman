using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingXY : MonoBehaviour {
    public const int LEFT = -1;
    public const int RIGHT = 1;
    public const int UP = 0;
    public const int DOWN = 2;

    public float offsetX = 0.5f;
    public float offsetY = 0.5f;

    public bool hasALeftNeighbour = false;
    public bool hasARightNeighbour = false;
    public bool hasABottomNeighbour = false;
    public bool hasAnUpperNeighbour = false;

    public bool tileableAtX = true;
    public bool tileableAtY = true;

    private float spriteWidth = 0f;
    private float spriteHeight = 0f;
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
        spriteHeight = Mathf.Abs(spriteRenderer.sprite.bounds.size.y * myTransform.localScale.y);
    }

    // Update is called once per frame
    void Update() {
        if (!hasARightNeighbour || !hasALeftNeighbour || !hasAnUpperNeighbour || !hasABottomNeighbour) {
            float camHorizontalExtend = mainCamera.orthographicSize * Screen.width / Screen.height;
            float camVerticalExtend = mainCamera.orthographicSize * Screen.height / Screen.width;

            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;
            float edgeVisiblePositionTop = (myTransform.position.y + spriteHeight / 2) - camVerticalExtend;
            float edgeVisiblePositionBottom = (myTransform.position.y - spriteHeight / 2) + camVerticalExtend;

            if (mainCamera.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightNeighbour) {
                NewTile(RIGHT);
                hasARightNeighbour = true;
            } else if (mainCamera.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftNeighbour) {
                NewTile(LEFT);
                hasALeftNeighbour = true;
            } else if (mainCamera.transform.position.y >= edgeVisiblePositionTop - offsetY && !hasAnUpperNeighbour) {
                NewTile(UP);
                hasAnUpperNeighbour = true;
            } else if (mainCamera.transform.position.y <= edgeVisiblePositionBottom + offsetY && !hasABottomNeighbour) {
                NewTile(DOWN);
                hasABottomNeighbour = true;
            }
        }
    }

    void NewTile(int direction) {

        Vector3 newPosition;
        if (direction == RIGHT || direction == LEFT) {
            newPosition = new Vector3(myTransform.position.x + spriteWidth * (direction == RIGHT ? 1 : -1),
                                   myTransform.position.y,
                                   myTransform.position.z);
        } else {
            newPosition = new Vector3(myTransform.position.x,
                                   myTransform.position.y + spriteHeight * (direction == UP ? 1 : -1),
                                   myTransform.position.z);
        }


        Transform newTile = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (!tileableAtX) {
            newTile.localScale = new Vector3(newTile.localScale.x * -1,
                                                newTile.localScale.y,
                                                newTile.localScale.z);
        }
        if (!tileableAtY) {
            newTile.localScale = new Vector3(newTile.localScale.x,
                                                newTile.localScale.y * -1,
                                                newTile.localScale.z);
        }

        newTile.parent = myTransform.parent;
        if (direction == RIGHT) {
            newTile.GetComponent<TilingXY>().hasALeftNeighbour = true;
        } else if (direction == LEFT) {
            newTile.GetComponent<TilingXY>().hasARightNeighbour = true;
        } else if (direction == UP) {
            newTile.GetComponent<TilingXY>().hasABottomNeighbour = true;
        } else {
            newTile.GetComponent<TilingXY>().hasAnUpperNeighbour = true;
        }
    }
}
