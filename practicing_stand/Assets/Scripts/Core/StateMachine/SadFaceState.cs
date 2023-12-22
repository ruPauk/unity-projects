public class SadFaceState : FaceState
{
    public SadFaceState()
    {
        faceState = FaceStateEnum.Sad;
    }

    public override void Update()
    {
        base.Update();
        if (Owner.GetDistance() > 6.0f)
        {
            StateMachine.ChangeState<HappyFaceState>();
            return;
        }
        if (Owner.GetDistance() >= 4.0f)
        {
            StateMachine.ChangeState<NeutralFaceState>();
            return;
        }
    }
}

