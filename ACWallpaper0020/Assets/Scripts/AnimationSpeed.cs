using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationSpeed : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = GlobalManager.Instance.fpsTarget;

        anim = gameObject.GetComponent<Animator>();
        anim.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        BassBPM._bpm.MinBPM = GlobalManager.Instance.bpmMin;
        BassBPM._bpm.MaxBPM = GlobalManager.Instance.bpmMax;
        BassBPM._bpm.BPMHistorySize = GlobalManager.Instance.bpmHSize;

        if (Screen.height != GlobalManager.Instance.resY || Screen.width != GlobalManager.Instance.resX)
        {
            Screen.SetResolution(GlobalManager.Instance.resX, GlobalManager.Instance.resY, true);
        }

        if (BassBPM._bpm.BPM != Double.NaN && GlobalManager.Instance.bpmCustom == false)
        {
            anim.speed = ((float)BassBPM._bpm.BPM / 25) * GlobalManager.Instance.animMult;
        }
        else
        {
            anim.speed = (GlobalManager.Instance.bpmMyBPM / 25) * GlobalManager.Instance.animMult;
        }
    }
}
