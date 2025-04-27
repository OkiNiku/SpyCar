using UnityEngine;
using UnityEngine.AI;

public class DrivingState : IState
{
    private SpyCar car;
    private NavMeshAgent agent;

    public DrivingState(SpyCar car)
    {
        this.car = car;
        this.agent = car.Agent;
    }

    public void Enter()
    {
        car.IsDriving = true;
        agent.speed = 5f;
        SetRandomDestination();
    }

    public void Execute()
    {
        /*if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            car.StateManager.TransitionTo(car.StateManager.boatState);
        }*/
    }

    public void Exit()
    {
        car.IsDriving = false;
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = car.transform.position + Random.insideUnitSphere * 15f;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 15f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
