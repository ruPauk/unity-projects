using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class BootstrapHook
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadScene()
    {
        if(SceneManager.GetActiveScene().name != "Bootstrap")
        {
            SceneManager.LoadScene("Bootstrap");
        }
    }
}
