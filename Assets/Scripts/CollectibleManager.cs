using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI collectibleCount;
    public Image collectiblesTaskImage;
    public Sprite checkedTask;


    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
            collectibleCount.text = ++LoadNextLevel.collectibles + " / " + LoadNextLevel.maxCollectibles;
            if (LoadNextLevel.collectibles == LoadNextLevel.maxCollectibles) {
                collectiblesTaskImage.sprite = checkedTask;
            }
        }
    }
}
