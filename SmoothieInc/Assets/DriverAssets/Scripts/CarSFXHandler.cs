using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSFXHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource screechAS;
    public AudioSource engineAS;
    public AudioSource hitAS;
    public AudioSource nitroAS;

    float enginePitch = 0.5f;
    float screechPitch = 0.5f;
    
    // components
    CarController controller;

    void Awake()
    {
        controller = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateScreechingSFX();
    }

    void UpdateEngineSFX()
    {
        float velocityMag = controller.GetVelocityMagnitude();

        float engineVolume = velocityMag * 0.05f;

        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1.0f);

        engineAS.volume = Mathf.Lerp(engineAS.volume, engineVolume, Time.deltaTime * 10);

        enginePitch = velocityMag * 0.2f;
        enginePitch = Mathf.Clamp(enginePitch, 0.5f, 2f);
        engineAS.pitch = Mathf.Lerp(engineAS.pitch, enginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateScreechingSFX()
    {
        if(controller.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                screechAS.volume = Mathf.Lerp(screechAS.volume, 1.0f, Time.deltaTime * 10);
                screechPitch = Mathf.Lerp(screechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                screechAS.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                screechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else
            screechAS.volume = Mathf.Lerp(screechAS.volume, 0, Time.deltaTime * 10);
    }

    public void PlayNitro()
    {
        if (!nitroAS.isPlaying)
            nitroAS.Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float relativeVelocity = col.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        hitAS.pitch = Random.Range(0.95f, 1.05f);
        hitAS.volume = volume;

        if (!hitAS.isPlaying)
            hitAS.Play();
    }
}
