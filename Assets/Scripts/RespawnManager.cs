using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour {
    public List<Respawnable> respawnableObjects;
    public static bool inSpace = false;
    public TMPro.TextMeshProUGUI enemyCount;
    public TMPro.TextMeshProUGUI collectibleCount;

    void Awake() {
        respawnableObjects = new List<Respawnable>();
        LoadNextLevel.maxPoints = 0;
        LoadNextLevel.maxCollectibles = 0;
    }

    public void Reset() {
        foreach (Respawnable respawnable in this.respawnableObjects) {
            respawnable.Respawn();
        }
    }

    public void Register(Respawnable respawnable) {
        this.respawnableObjects.Add(respawnable);
        if (respawnable.CompareTag("Enemy")) {
            enemyCount.text = LoadNextLevel.points + " / " + ++LoadNextLevel.maxPoints;
        } else if (respawnable.CompareTag("Collectible")) {
            collectibleCount.text = LoadNextLevel.collectibles + " / " + ++LoadNextLevel.maxCollectibles;
        }
    }
}
