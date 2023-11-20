using Assets.Scripts.Core.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MVC_Test : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _tmpro;
    [SerializeField] private Button _button;
    private TextMeshProUGUI _buttonText;

    public event Action OnHide;
    public event Action OnShow;

    public TextMeshProUGUI Tmpro => _tmpro;
    public Button Button => _button;
    public TextMeshProUGUI ButtonTMP => _buttonText;
    

    private void Start()
    {
        _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
        var controller = new MVC_Controller();
        controller.AddView(this);
    }

    private void OnEnable()
    {
        OnShow?.Invoke();
    }

    private void OnDisable()
    {
        OnHide?.Invoke();
    }
}
