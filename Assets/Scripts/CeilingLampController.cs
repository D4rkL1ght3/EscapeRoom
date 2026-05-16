using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CeilingLampController : MonoBehaviour
{
    [Header("Light Reference")]
    public Light2D ceilingLight;

    [Header("Dimming")]
    public float maxIntensity = 1.2f;
    public float minIntensity = 0.15f;

    [Tooltip("Controls how the light dims over time.")]
    public AnimationCurve dimCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Timed Flicker")]
    public bool enableFlicker = true;

    [Tooltip("How often the light flickers, in seconds.")]
    public float flickerInterval = 30f;

    [Tooltip("How long each flicker lasts.")]
    public float flickerDuration = 0.15f;

    [Range(0f, 1f)]
    public float flickerIntensityMultiplier = 0.35f;

    private float flickerIntervalTimer;
    private float flickerTimer;
    private bool isFlickering;

    void Start()
    {
        if (ceilingLight == null)
        {
            ceilingLight = GetComponent<Light2D>();
        }

        flickerIntervalTimer = flickerInterval;
    }

    void Update()
    {
        if (ceilingLight == null)
            return;

        if (TimerManager.Instance == null)
            return;

        if (TimerManager.Instance.TimeEnded)
        {
            ceilingLight.intensity = 0f;
            return;
        }

        float targetIntensity = GetTimerBasedIntensity();

        HandleTimedFlicker(targetIntensity);
    }

    float GetTimerBasedIntensity()
    {
        float timePercent = TimerManager.Instance.TimePercent;

        float dimProgress = 1f - timePercent;

        float curvedDimProgress = dimCurve.Evaluate(dimProgress);

        return Mathf.Lerp(maxIntensity, minIntensity, curvedDimProgress);
    }

    void HandleTimedFlicker(float targetIntensity)
    {
        if (!enableFlicker)
        {
            ceilingLight.intensity = targetIntensity;
            return;
        }

        if (isFlickering)
        {
            flickerTimer -= Time.deltaTime;

            float lowIntensity = targetIntensity * flickerIntensityMultiplier;

            ceilingLight.intensity = Random.Range(lowIntensity, targetIntensity);

            if (flickerTimer <= 0f)
            {
                isFlickering = false;
                ceilingLight.intensity = targetIntensity;
                flickerIntervalTimer = flickerInterval;
            }

            return;
        }

        ceilingLight.intensity = targetIntensity;

        flickerIntervalTimer -= Time.deltaTime;

        if (flickerIntervalTimer <= 0f)
        {
            StartFlicker();
        }
    }

    void StartFlicker()
    {
        isFlickering = true;
        flickerTimer = flickerDuration;
    }
}