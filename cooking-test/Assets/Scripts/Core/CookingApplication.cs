using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game's entry point
public class CookingApplication : MonoBehaviour
{
    [HideInInspector] public SettingsController settingsController;
    [HideInInspector] public GameplayController controller;
    [HideInInspector] public CookingTimer timer;
    //[HideInInspector] public 

    private void Awake()
    {
        this.settingsController = FindObjectOfType<SettingsController>();
        this.controller = FindObjectOfType<GameplayController>();
        this.timer = FindObjectOfType<CookingTimer>();

        InitializeSettings();
    }

    private void Start() {

    }

    private void InitializeSettings()
    {
        timer.timeInSeconds = settingsController.settings.timer;
    }
    
}
