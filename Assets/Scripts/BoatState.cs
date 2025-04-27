using UnityEngine;
using UnityEngine.AI;

public class BoatState : IState
{
    private SpyCar car;
    private NavMeshAgent agent;

    public BoatState(SpyCar car)
    {
        this.car = car;
        this.agent = car.Agent;
    }

    public void Enter()
    {
        car.IsBoating = true;
        agent.speed = 2.5f;
        Debug.Log("Entering Boat State");
    }

    public void Execute()
    {
       /* if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            car.StateManager.TransitionTo(car.StateManager.drivingState);
        } */
    }

    public void Exit()
    {
        car.IsBoating = false;
    }
}
