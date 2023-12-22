public class NeutralFaceState : FaceState
{
    public NeutralFaceState()
    {
        faceState = FaceStateEnum.Neutral;
    }

    public override void Update()
    {
        base.Update();
        if (Owner.GetDistance() > 6.0f)
        {
            StateMachine.ChangeState<HappyFaceState>();
            return;
        }
        if (Owner.GetDistance() < 4.0f)
        {
            StateMachine.ChangeState<SadFaceState>();
            return;
        }
    }
}

