using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerEffect : MonoBehaviour
{
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light.")]
    public Light LightToFlicker;

    [Tooltip("Minimum random light intensity.")]
    public float MinIntensity = 0f;

    [Tooltip("Maximum random light intensity")]
    public float MaxIntensity = 1f;

    [Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern.")]
    [Range(1, 50)] public int Smoothing = 5;

    Queue<float> SmoothQueue;
    float LastSum = 0;

    public void Reset()
    {
        SmoothQueue.Clear();
        LastSum = 0;
    }

    private void Start()
    {
        SmoothQueue = new Queue<float>(Smoothing);
        // External or internal light?
        if (LightToFlicker == null) {
           LightToFlicker = this.GetComponent<Light>();
        }
    }

    private void Update()
    {
        if (LightToFlicker == null || Time.timeScale <= 0)
            return;

        // Pop off an item if too big.
        while (SmoothQueue.Count >= Smoothing) {
            LastSum -= SmoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average.
        float NewVal = Random.Range(MinIntensity, MaxIntensity);
        SmoothQueue.Enqueue(NewVal);
        LastSum += NewVal;

        // Calculate new smoothed average.
        LightToFlicker.intensity = LastSum / (float)SmoothQueue.Count;
    }

}