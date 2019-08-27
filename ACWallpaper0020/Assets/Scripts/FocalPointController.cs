using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPointController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        transform.RotateAround(Vector3.zero, Vector3.up, GlobalManager.Instance.focSpeed * Time.deltaTime);
    }
}
