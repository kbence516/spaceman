using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPreviousLevel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene("StartLevel", LoadSceneMode.Single);
            LoadNextLevel.maxCollectibles = 0;
            LoadNextLevel.maxPoints = 0;
        }
    }
}
