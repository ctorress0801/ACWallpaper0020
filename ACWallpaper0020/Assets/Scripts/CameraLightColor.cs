using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLightColor : MonoBehaviour
{
    Light lt;
    Color color;
    float duration = 5;
    float lerpControl = 0;
    bool proc = false;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (proc == false)
        {
            color.r = AnotherColor();
            color.g = AnotherColor();
            color.b = AnotherColor();
            proc = true;
            lerpControl = 0;
        }
        else if (proc == true)
        {
            if (!double.IsNaN(BassBPM._bpm.BPM))
            {
                duration = (float)BassBPM._bpm.BPM / 60f;
                //Debug.Log("Es NaN!!!");
            }
            lt.color = Color.Lerp(lt.color, color, lerpControl);
            if (lerpControl < 1)
            {
                lerpControl += Time.deltaTime / duration;
            }
        }

        if (lt.color.r.Equals(color.r) && lt.color.g.Equals(color.g) && lt.color.b.Equals(color.b))
        {
            proc = false;
        }
    }

    private float AnotherColor()
    {
        float randomNumber = Random.Range(0.3137f, 1.0f);
        return randomNumber;
    }
}
