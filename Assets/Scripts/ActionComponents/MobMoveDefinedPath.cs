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
    [SerializeField] protected bool trackPath;
    [SerializeField] protected bool isPaused;

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

    public bool TrackPath
    {
        get { return trackPath; }
        set { trackPath = value;  }
    }

    public SplineContainer SplinePath
    {
        get { return splinePath; }
    }

    void Awake() {
        progress = 0f;
        relativeProgress = 0f;
        loop = false;
        trackPath = false;
        isPaused = true;
    }

    void Start() {
        if (Speed == 0) {
            Debug.Log("Movement with 0 speed! Removing MobMoveDefinedPath!");
            Destroy(this);
        }
        gameObject.transform.position = splinePath.EvaluatePosition(0);
    }

    void Update() {
        if (!isPaused) {
            // update pos
            
            // increment progress 
            relativeProgress += increment * Time.deltaTime;
            
            // catch what to do when the end is reached
            if (Mathf.Abs(relativeProgress) >= 1) {
                // update progresses
                relativeProgress = 1;
                progress = Mathf.Floor(progress) + relativeProgress;
                
                // loop or pause
                if (loop) resetRelativeProgress();
                else Pause();
            } else {
                progress = relativeProgress;
            }

            gameObject.transform.position = splinePath.EvaluatePosition(relativeProgress);

            if (trackPath) gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, splinePath.EvaluateTangent(RelativeProgress));

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
