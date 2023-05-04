using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

// Component that will make the gameObject face toward a certain direction or position.
// Component has a set turning speed
public class MobMoveDefinedPath : Action
{
    [SerializeField] public bool Loop = false;
    [SerializeField] public bool IsTracking = false;

    [SerializeField] protected SplineContainer splinePath;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int pass = 0;
    [SerializeField] protected float progress = 0f;
    [SerializeField] protected float relativeProgress = 0f;
    [SerializeField] protected float increment = 0; 

    protected MobLookController MLC;
    protected MobMovementController MMC;

    protected MobLook ML;

    // Speed is in units of unity length per frame
    public float Speed 
    {
        get { return speed; }
        set 
        { 
            speed = value;
            increment = speed / splinePath.CalculateLength();
        }
    }

    public float Progress
    {
        get { return progress; }
    }

    public float RelativeProgress {
        get { return relativeProgress; }
    }

    public SplineContainer SplinePath {
        get { return splinePath; }
    }

    protected override void Awake() { 
        if (!PerformingObj.TryGetComponent<MobMovementController>(out MMC)) {
            MMC = PerformingObj.AddComponent<MobMovementController>();
        }

        if (IsTracking && !PerformingObj.TryGetComponent<MobLookController>(out MLC)) {
            MLC = PerformingObj.AddComponent<MobLookController>(); 
        }
    }

    void Update() {
            // update pos
            progress = pass + relativeProgress;

            if (singlePass()) {
                pass++;

                if (Loop) resetRelativeProgress();
                else Pause();
            }
            
            // increment progress 
            relativeProgress += increment * Time.deltaTime;
            relativeProgress = Mathf.Clamp01(relativeProgress);

            gameObject.transform.position = splinePath.EvaluatePosition(relativeProgress);

            if (IsTracking) ML.lookToward(splinePath.EvaluateTangent(RelativeProgress));

            // debugDetails();  
    }

    protected override void execute() {
        this.enabled = !this.enabled;
    }

    protected override bool preCheck() {
        bool valid = true;

        if (base.preCheck()) valid = false;

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
        gameObject.transform.position = splinePath.EvaluatePosition(0);
    }

    public void setSpline(SplineContainer spline) {
        splinePath = spline;
        resetProgress();
    }

    public bool singlePass() {
        return relativeProgress == 1;
    }

    // FOR DEBUGGING
    public void debugDetails() {
        Debug.Log("Progress: " + Progress + " RelProgress: " + RelativeProgress);
    }
}
