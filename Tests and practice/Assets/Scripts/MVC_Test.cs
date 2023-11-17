using Assets.Scripts.Core.MVC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MVC_Test : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _tmpro;
    [SerializeField] private Button _button;

    public TextMeshProUGUI Tmpro => _tmpro;
    public Button Button => _button;

    private void Start()
    {
        var controller = new MVC_Controller();
        controller.AddView(this);
    }
}
