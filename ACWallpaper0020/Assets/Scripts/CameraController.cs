using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, 9.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //tilt
        transform.eulerAngles = new Vector3(GlobalManager.Instance.camAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        //FOV
        Camera.main.fieldOfView = GlobalManager.Instance.camFov;
        //distance
        transform.localPosition = new Vector3(0.0f, GlobalManager.Instance.camHeight, GlobalManager.Instance.camDist);
    }
}
