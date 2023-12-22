using UnityEngine;

public abstract class FaceState : BaseState<FaceController>
{
    public FaceStateEnum faceState;

    public override void Enter()
    {
        base.Enter();
        Owner = StateMachine.Owner as FaceController;
        Owner.SetFace(faceState);

    }
}

