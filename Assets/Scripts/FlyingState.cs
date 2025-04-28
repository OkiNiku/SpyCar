using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class FlyingState : IState
{
    private SpyCar car;
    private NavMeshAgent agent;
    private Animator animator;


    private float targetAltitude;
    private float turnThreshold = 0.1f;
    private float previousRotationY;
    private float angularVelocityY;
    public FlyingState(SpyCar car)
    {
        this.car = car;
        this.animator = car.Animator;
        this.agent = car.Agent;
    }

    public void Enter()
    {
        car.IsFlying = true;
        targetAltitude = car.CaluculatedMaxFlyHeight;
        car.LerpToAltitude(targetAltitude, 2f);
        SetRandomDestination();
    }

    public void Execute()
    {
        /*HandleTurning();
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            car.StateManager.TransitionTo(car.StateManager.drivingState);
        }*/
    }

    public void Exit()
    {
        car.IsFlying = false;
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = car.transform.position + Random.insideUnitSphere * 10f;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void HandleTurning()
    {
        if (agent == null || animator == null)
        {
            if (agent == null) Debug.LogError("NavMeshAgent is null in FlyingState.");
            if (animator == null) Debug.LogError("Animator is null in FlyingState.");
            return;
        }

        float currentRotationY = agent.transform.eulerAngles.y;
        float rotationDifference = Mathf.Abs(currentRotationY - previousRotationY);
        angularVelocityY = rotationDifference / Time.deltaTime;

        if (angularVelocityY <= turnThreshold)
        {
            animator.SetBool("IsTurningLeft", false);
            animator.SetBool("IsTurningRight", false);
            previousRotationY = currentRotationY;
            return;
        }

        if (currentRotationY > previousRotationY)
        {
            animator.SetBool("IsTurningLeft", false);
            animator.SetBool("IsTurningRight", true);
        }
        else if (currentRotationY < previousRotationY)
        {
            animator.SetBool("IsTurningRight", false);
            animator.SetBool("IsTurningLeft", true);
        }

        previousRotationY = currentRotationY;
    }
    
}
