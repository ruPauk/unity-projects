using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Button _changeFaceButton;
    [SerializeField] private FaceController _faceController;
    private StateMachine<FaceController> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<FaceController>(_faceController);
        _stateMachine.ChangeState<NeutralFaceState>();
    }

    private void Update()
    {
        if (_stateMachine != null)
        {
            _stateMachine.Update();
        }
    }

}
