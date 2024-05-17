using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
            float fuelSeconds = collision.gameObject.GetComponent<PlayerMovementSpace>().fuelSeconds;
            fuelSeconds = Mathf.Min(fuelSeconds + 25, collision.gameObject.GetComponent<PlayerMovementSpace>().maxFuelSeconds);
            collision.gameObject.GetComponent<PlayerMovementSpace>().fuelSeconds = fuelSeconds;
        }
    }
}
