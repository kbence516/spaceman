using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour {
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool originalActive;
    private Rigidbody2D rigidBody;
    private int maxHp;

    private RespawnManager respawnManager;

    protected virtual void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        originalActive = this.gameObject.activeSelf;
        
        if (rigidBody != null) {
            if (rigidBody.name == "Player" || rigidBody.name == "PlayerRocket") {
                maxHp = rigidBody.GetComponent<PlayerDamage>().maxHp;
            }
            else {
                maxHp = rigidBody.GetComponent<EnemyDamage>().maxHp;
            }
        }

        respawnManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>();
        respawnManager.Register(this);
    }

    public virtual void Respawn() {
        this.transform.position = originalPosition;
        if (rigidBody != null) {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0;
            if (rigidBody.name == "Player" || rigidBody.name == "PlayerRocket") {
                rigidBody.GetComponent<PlayerDamage>().hp = maxHp;
            }
            else {
                rigidBody.GetComponent<EnemyDamage>().hp = maxHp;
            }
        }
        if (RespawnManager.inSpace) {
            this.transform.rotation = originalRotation;
        }

        this.gameObject.SetActive(originalActive);
    }
}
