using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

// Component that will make the gameObject face toward a certain direction or position.
// Component has a set turning speed
public class MobMoveDefinedPath : MobMove
{
    [SerializeField] protected SplineContainer splinePath;
    [SerializeField] protected float progress;
    [SerializeField] private float increment;
    [SerializeField] protected float relativeProgress;
    [SerializeField] protected bool loop;
    [SerializeField] protected bool isTracking;
    [SerializeField] protected bool isPaused;
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
    public bool Loop
    {
        set { loop = value; }
        get { return loop; }
    }

    public bool IsPaused 
    {
        get { return isPaused; }
    }

    public bool IsTracking
    {
        get { return isTracking; }
        set { isTracking = value;  }
    }

    public SplineContainer SplinePath
    {
        get { return splinePath; }
    }

    void Awake() {
        progress = 0f;
        relativeProgress = 0f;
        loop = false;
        isTracking = false;
        isPaused = true;
    }

    void Start() {
        if (Speed == 0) {
            Debug.Log("Movement with 0 speed! Removing MobMoveDefinedPath!");
            Destroy(this);
        }
        gameObject.transform.position = splinePath.EvaluatePosition(0);

        if (IsTracking && gameObject.GetComponent<MobLook>() == null) {
            Debug.Log("Object doesn't contain a MobLook object, disabling isTracking");
            IsTracking = false;
        } else {
            ML = gameObject.GetComponent<MobLook>();
        }
    }

    void Update() {
        if (!isPaused) {
            // update pos
            if (relativeProgress == 1) {
                relativeProgress = 1;
                progress = Mathf.Floor(progress) + relativeProgress;

                if (loop) resetRelativeProgress();
                else Pause();
            } else {
                progress = relativeProgress;
            }
            
            // increment progress 
            relativeProgress += increment * Time.deltaTime;
            relativeProgress = Mathf.Clamp01(relativeProgress);

            gameObject.transform.position = splinePath.EvaluatePosition(relativeProgress);

            if (isTracking) ML.lookToward(splinePath.EvaluateTangent(RelativeProgress));

            // debugDetails(); 
        }   
    }

    public void Resume() {
        isPaused = false;
    }

    public void Pause() {
        isPaused = true;
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
