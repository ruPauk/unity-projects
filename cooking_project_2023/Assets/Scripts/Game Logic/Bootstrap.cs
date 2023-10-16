using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{


    void Start()
    {
        ModuleLocator.AddModule(new TableModule());
        SceneManager.LoadScene(1);
    }

}
