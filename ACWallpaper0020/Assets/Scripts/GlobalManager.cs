using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    //This class is being used as a placeholder for all the settings.
    //It is intented to be replaced by a singleton and a config file.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static GlobalManager Instance { get; private set; }

    //Focal Point---------------------------------------------
    //Focal Point Speed Rotation
    [Range(-100, 100)]
    public float focSpeed = 30.0f;


    //Camera--------------------------------------------------
    [Range(-20, 15)]
    public float camAngle = 0.0f;//X=0.0f 15 -> -20
    [Range(25, 90)]
    public float camFov = 60.0f;//X=60.0f 25 ->90
    [Range(3, 14)]
    public float camDist = 9.0f;//Z=90.0f 50 -> 140
    [Range(-1.5f, 9)]
    public float camHeight = 0.0f;//Y

    //Effects-------------------------------------------------
    public bool ppBloom = true;
    [Range(0, 30)]
    public float ppBInt = 13;   //V=13 0 -> 30
    [Range(1, 10)]
    public float ppBDif = 7;    //V=7 1 -> 10

    public bool ppChromAb = true;
    [Range(0, 1)]
    public float ppChInt = 1;   //V=1 0 -> 1

    public bool ppDof = true;
    [Range(5.5f, 20)]
    public float ppDofFocd = 11;//V=11 5.5 -> 20
    [Range(1, 300)]
    public float ppDofFocl = 50; //V=50 1 -> 300

    public bool ppVig = true;
    [Range(0, 1)]
    public float ppVigInt = 0.474f; //V=0.474 0 -> 1

    //Bass--------------------------------------------------------
    [Range(30, 100)]
    public float bpmMin = 60;//Min BPM
    [Range(120, 250)]
    public float bpmMax = 140;//Max BPM
    [Range(2, 50)]
    public int bpmHSize = 10;//BPM History Size/Buffer Size
    [Range(0.5f, 4)]
    public float animMult = 1f;//Speed multiplier

    //Runtime
    [Range(30, 90)]
    public int fpsTarget = 60;//Execution FPS
    public bool bpmCustom = false;//Custom BPM
    [Range(60, 300)]
    public float bpmMyBPM = 128;//My custom BPM
    public int resY = Screen.width;
    public int resX = Screen.height;

    //Shader
    //Shader vars

    //

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//Destroys all new duplicated instances
        }
    }

}
