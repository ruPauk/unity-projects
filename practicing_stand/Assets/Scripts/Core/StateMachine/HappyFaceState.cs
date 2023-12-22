public class HappyFaceState : FaceState
{
    public HappyFaceState()
    {
        faceState = FaceStateEnum.Happy;
    }

    public override void Update()
    {
        base.Update();
        if (Owner.GetDistance() < 4.0f)
        {
            StateMachine.ChangeState<SadFaceState>();
            return;
        }
        if (Owner.GetDistance() <= 6.0f)
        {
            StateMachine.ChangeState<NeutralFaceState>();
            return;
        }
    }
}

