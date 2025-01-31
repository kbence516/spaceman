using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && LoadNextLevel.points == LoadNextLevel.maxPoints) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
