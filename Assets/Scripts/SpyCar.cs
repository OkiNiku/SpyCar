using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpyCar : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [Header("State Flags")]
    [SerializeField] private bool isFlying;
    [SerializeField] private bool isDriving;
    [SerializeField] private bool isBoating;

    private SpyCarStateManager stateManager;
    private GameObject ceiling;
    private float calculatedMaxFlyHeight;

    public bool IsFlying { get => isFlying; set => isFlying = value; }
    public bool IsDriving { get => isDriving; set => isDriving = value; }
    public bool IsBoating { get => isBoating; set => isBoating = value; }

    public NavMeshAgent Agent => agent;
    public SpyCarStateManager StateManager => stateManager;
    public float CaluculatedMaxFlyHeight => calculatedMaxFlyHeight;

    public Animator Animator { get; private set; }

    [SerializeField]
    private IStateEventSO OnStateChanged;

    // Reference to the environment depth manager in MRUK
    //public EnvironmentDepthManager _environmentDepthManager;

    private void Start()
    {
        //MRUK.Instance.RegisterSceneLoadedCallback(FindCeiling);

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not assigned to SpyCar!");
            return;
        }

        if (OnStateChanged == null)
        {
            Debug.LogError("OnStateChanged event is not assigned to SpyCar!");
            return;
        }

        stateManager = new SpyCarStateManager(this, OnStateChanged);
        stateManager.Initialize(stateManager.drivingState);

    }

    private void Update()
    {
        if (stateManager == null) return;

        if (Input.GetKeyDown(KeyCode.F)) stateManager.TransitionTo(stateManager.flyingState);
        if (Input.GetKeyDown(KeyCode.D)) stateManager.TransitionTo(stateManager.drivingState);
        if (Input.GetKeyDown(KeyCode.B)) stateManager.TransitionTo(stateManager.boatState);

        stateManager.Execute();
    }

    /*MRUK locating the ceiling to understand how high you can fly
     * and also setting its depth texture so that the ceiling does not occlude you
  /* public void LocateCeiling()
    {
        StartCoroutine(LocateCeilingRoutine());
    }

    public IEnumerator LocateCeilingRoutine()
    {
        yield return new WaitForEndOfFrame();

        ceiling = GameObject.Find("CEILING");

        if (ceiling == null)
        {
            Debug.LogError("CEILING GameObject not found!");
            yield break;
        }

        maxFlyHeight = ceiling.transform.position.y - 0.6f;
        Debug.LogError(maxFlyHeight);

        yield return new WaitForEndOfFrame();

        Transform effectMeshTransform = ceiling.transform.Find("CEILING_EffectMesh");

        if (effectMeshTransform == null)
        {
            Debug.LogError("CEILING_EffectMesh child GameObject not found!");
            yield break;
        }

        MeshFilter effectMeshFilter = effectMeshTransform.GetComponent<MeshFilter>();

        if (effectMeshFilter == null)
        {
            Debug.LogError("CEILING_EffectMesh GameObject does not have a MeshFilter component!");
            yield break;
        }

        if (_environmentDepthManager.MaskMeshFilters == null)
        {
            _environmentDepthManager.MaskMeshFilters = new List<MeshFilter>();
            Debug.LogError("MaskMeshFilters was null and has now been initialized.");
        }

        _environmentDepthManager.MaskMeshFilters.Add(effectMeshFilter);
    }*/

    public void LerpToAltitude(float targetOffset, float duration)
    {
        StartCoroutine(LerpToAltitudeRoutine(targetOffset, duration));
    }

    private IEnumerator LerpToAltitudeRoutine(float targetOffset, float duration)
    {
        float startOffset = agent.baseOffset;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            agent.baseOffset = Mathf.Lerp(startOffset, targetOffset, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.baseOffset = targetOffset;
    }

    /* NEEDS TO BE TESTED!!!
     * Examples of running an animation that has an exit time using Unitask
     * You would have animation events, transitions, and has exit time on appropriate states in animator
    
    
    public async UniTask SetFlyingTrueAsync()
    {
        animator.SetBool("IsFlying", true);
        await animator.CrossFadeAsync("Flying", 0.2f);
    }

    public async UniTask SetFlyingToDrivingAsync()
    {
        animator.SetBool("IsFlying", false);
        await animator.CrossFadeAsync("Idle", 0.2f);
    }


            - - HAD BEEN TESTED - -

    CAN ALSO DO THIS LIKE SO WITH COROUTINES:

     Flying Class - car.StartToLand();
                car.IsInTakingOffMode = false;
                car.StartCoroutine(LerpToBaseOffset(car.Agent, 0f, 1.5f));
                car.StartCoroutine(HandleLandingToTopDown());

    public void StartToLand()
    {
        // Stop the coroutine when you want to stop the animation
        isInLanding = true;
        IsInTakingOffMode = false;
        animator.SetBool("isInLanding", true);
        animator.SetBool("IsInTakingOffMode", false);
    }

    private IEnumerator LerpBaseOffset(NavMeshAgent agent, float targetOffset, float duration)
    {
        float startOffset = agent.baseOffset;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            agent.baseOffset = Mathf.Lerp(startOffset, targetOffset, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        agent.baseOffset = targetOffset;
    }

     private IEnumerator HandleLandingToTopDown()
    {
        // Wait for the IsInTakingOffMode animation to finish
        yield return car.StartCoroutine(car.Land());
        yield return new WaitForSeconds(1);

        animator.Play("TopGoingDown");
        yield return new WaitForSeconds(2);

        car.IsInTakingOffMode = false;
        car.isInLanding = false;
        car.TopDown = True;
        stateManager.TransitionTo(stateManager.drivingState);

    }

    SpyCar class - public IEnumerator Land()
    {
        // Wait until the animation is done or the bool is false
        while (isInLanding)
        {


            yield return null; // Wait until the next frame
        }
        isInLanding = false;
    }


    */

}
