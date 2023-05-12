using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

// Component that will make the gameObject face toward a certain direction or position.
// Component has a set turning speed
public class MobMoveDefinedPath : Action
{
    [SerializeField] public bool Loop = false;
    [SerializeField] public bool IsTracking = false;
    [SerializeField] public float Speed = 2f;

    [SerializeField] protected SplineContainer splinePath = null;
    [SerializeField] protected int pass = 0;
    [SerializeField] protected float progress = 0f;
    [SerializeField] protected float relativeProgress = 0f;
    [SerializeField] protected float increment = 0; 
    [SerializeField] public float awaitRelativeProgress = 1; 

    [SerializeField] protected MobLookController MLC;
    [SerializeField] protected MobMovementController MMC;

    // Speed is in units of unity length per frame

    public float Progress
    {
        get { return progress; }
    }

    public float RelativeProgress {
        get { return relativeProgress; }
    }

    public SplineContainer SplinePath {
        get { return splinePath; }
        set 
        {
            splinePath = value;
            resetProgress();
        }
    }

    protected override void Awake() {
        base.Awake();

         if (!PerformingObj.TryGetComponent<MobMovementController>(out MMC)) {
            MMC = PerformingObj.AddComponent<MobMovementController>();
        }

        if (!PerformingObj.TryGetComponent<MobLookController>(out MLC)) {
            MLC = PerformingObj.AddComponent<MobLookController>(); 
        }
    }

    void Update() {
            // update pos
            progress = pass + relativeProgress;

            if (relativeProgress == 1) {
                pass++;

                if (Loop) resetRelativeProgress();
                else Pause();
            }

            calculateIncrement();
            if (increment == 0) Pause();
            
            // increment progress 
            relativeProgress += increment * Time.deltaTime;
            relativeProgress = Mathf.Clamp01(relativeProgress);

            MMC.MoveTo(splinePath.EvaluatePosition(relativeProgress));

            if (IsTracking) MLC.lookToward(splinePath.EvaluateTangent(RelativeProgress));

            // debugDetails();  
    }

    protected override void execute() {
        Resume();
    }

    protected override bool preCheck() {
        bool valid = true;

        if (!base.preCheck()) valid = false;

        if (splinePath == null) {
            Debug.Log("spline Path null");
            valid = false;
        }

        if (Speed == 0) {
            Debug.Log("Speed is set to 0");
            valid = false;
        }

        return valid;    
    }

    public void Resume() {
        this.enabled = true;
    }

    public void Pause() {
        this.enabled = false;
    }

    public void resetProgress() {
        progress = 0;
        resetRelativeProgress(); 
    }

    public void resetRelativeProgress() {
        relativeProgress = 0;
        MMC.MoveTo(Vector3.zero);

        splinePath.EvaluatePosition(0);

        MMC.MoveTo(splinePath.EvaluatePosition(0));
    }

    public bool singlePass() {
        return relativeProgress >= awaitRelativeProgress;
    }

    // FOR DEBUGGING
    public void debugDetails() {
        Debug.Log("Progress: " + Progress + " RelProgress: " + RelativeProgress);
    }

    // increment is a function of speed and total length
    private void calculateIncrement() {
        increment = Speed / splinePath.CalculateLength();
    }
}
