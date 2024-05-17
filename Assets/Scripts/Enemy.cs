using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private EnemiesManager enemiesManager;
    // Start is called before the first frame update
    void Start() {
        enemiesManager = GameObject.Find("Enemies").GetComponent<EnemiesManager>();
        enemiesManager.Register(this);
    }
}
