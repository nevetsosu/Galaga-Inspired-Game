using UnityEngine;
using UnityEngine.Splines;
using System.Threading.Tasks;

// Component that will make the gameObject face toward a certain direction or position.
// Component has a set turning speed
public class MobMoveDefinedPath : MobMove
{
    [SerializeField] protected SplineContainer splinePath;
    protected float progress;
    protected float relativeProgress;
    [SerializeField] protected bool loop;
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
    private float increment;

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
    }

    public bool IsPaused 
    {
        get { return isPaused; }
    }

    void Awake() {
        progress = 0f;
        relativeProgress = 0f;
        loop = false;
        isPaused = true;
        speed = 10;
    }

    void Start() {
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
                relativeProgress = Mathf.Floor(relativeProgress);
                progress = Mathf.Floor(progress) + relativeProgress;
                
                // loop or pause
                if (loop) resetRelativeProgress();
                else Pause();
            }

            gameObject.transform.position = splinePath.EvaluatePosition(relativeProgress);
            debugDetails(); 
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

    public bool isFinished() {
        return relativeProgress == 1;
    }

    // FOR DEBUGGING
    private void debugDetails() {
        Debug.Log("Progress: " + Progress + " RelProgress: " + RelativeProgress);
    }
}
