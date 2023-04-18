using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    public CarController carController;
    public TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
