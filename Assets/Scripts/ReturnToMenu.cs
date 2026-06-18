using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        NetworkManager manager = NetworkManager.Singleton;

        if (manager != null)
        {
            manager.Shutdown();
            Destroy(manager.gameObject);
        }

        yield return null;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
