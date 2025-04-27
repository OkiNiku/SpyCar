public class SpyCarStateManager
{
    public IState CurrentState { get; private set; }

    public FlyingState flyingState;
    public DrivingState drivingState;
    public BoatState boatState;

    private SpyCar car;

    private IStateEventSO stateChangedEvent;


    public SpyCarStateManager(SpyCar car, IStateEventSO stateChangedEvent)
    {
        this.car = car;
        this.stateChangedEvent = stateChangedEvent;

        flyingState = new FlyingState(car);
        drivingState = new DrivingState(car);
        boatState = new BoatState(car);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
        stateChangedEvent.Raise(state);
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        nextState.Enter();
        CurrentState = nextState;
        stateChangedEvent.Raise(nextState);
    }

    public void Execute()
    {
        CurrentState?.Execute();
    }
}