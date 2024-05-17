using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public static int collectibles = 0;
    public static int maxCollectibles;
    public static int points = 0;
    public static int maxPoints;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player") && collectibles == maxCollectibles && points == maxPoints) {
            SceneManager.LoadScene("SpaceLevel", LoadSceneMode.Single);
            maxCollectibles = 0;
            maxPoints = 0;
        }
    }
}
