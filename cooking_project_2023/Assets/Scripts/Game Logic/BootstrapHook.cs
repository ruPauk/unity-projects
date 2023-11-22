using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapHook
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadScene()
    {
        if(SceneManager.GetActiveScene().name != "Bootstrap")
        {
            SceneManager.LoadScene(0);
        }
    }
}
