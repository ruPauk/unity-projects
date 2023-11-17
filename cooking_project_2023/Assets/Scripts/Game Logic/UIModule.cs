using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIModule : MonoBehaviour, IModule
{
    [SerializeField] private TextMeshProUGUI _visitorsCounter;
    [SerializeField] private TextMeshProUGUI _satisfiedVisitorsCounter;
    [SerializeField] private TextMeshProUGUI _sadVisitorsCounter;

    private void Start()
    {
        ModuleLocator.GetModule<VisitorsModule>().OnVisitorCounterUpdate += UpdateUI;
        
    }

    public void UpdateUI(int left, int satisfied, int sad)
    {
        Debug.Log("Updating UI");
        _visitorsCounter.text = left.ToString();
        _satisfiedVisitorsCounter.text = satisfied.ToString();
        _sadVisitorsCounter.text = sad.ToString();
    }

}
