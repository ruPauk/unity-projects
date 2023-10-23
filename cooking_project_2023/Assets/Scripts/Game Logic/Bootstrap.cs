using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private VisitorView _visitorPrefab;

    void Start()
    {
        DOTween.Init();
        ModuleLocator.AddModule(new TableModule());
       // А как мне модуль создавать с TableSeats, если TableSeats еще отсутствует в момент Start Bootstrap?
        ModuleLocator.AddModule(new VisitorsModuleR(new ObjectPool<VisitorView>(_visitorPrefab)));
        SceneManager.LoadScene(1);
    }

}
