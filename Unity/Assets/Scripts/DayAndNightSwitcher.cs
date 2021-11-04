using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(ParticleSystem))]
public class DayAndNightSwitcher : MonoBehaviour
{
    public static DayAndNightSwitcher Instance { get; private set; } = null;

    private DayAndNightSwitcher()
    {
            
    }
        
    private class DayTime
    {
        public const float StartOfDay = 0f;
        public const float EndOfDay = 0.5f;
        public const float StartOfNight = 0.5f;
        public const float EndOfNight = 1f;
            
        public float CurrentTime { get; set; }

        public bool IsEndOfNight => CurrentTime >= EndOfNight;
    }
        
    [Range(1, 59)]
    [SerializeField] private int _dayDurationInMinutes;
    #region Sun
    [Header("Sun properties")]
    [SerializeField] private Light _sun;
    [SerializeField] private AnimationCurve _sunCurve;
    private float _sunIntensity;
    #endregion
    [Space]
    #region Moon
    [Header("Moon properties")]
    [SerializeField] private Light _moon;
    [SerializeField] private AnimationCurve _moonCurve;
    private float _moonIntensity;
    #endregion

    [SerializeField] private ParticleSystem _stars;
    private DayTime _timeOfDay;

    private void Start()
    {
        _sunIntensity = _sun.intensity;
        _moonIntensity = _moon.intensity;
        _timeOfDay = new DayTime();
    }

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        UpdateDayAndNightCycle();
    }

    private void UpdateDayAndNightCycle()
    {
        _timeOfDay.CurrentTime += Time.deltaTime / _dayDurationInMinutes.ToSeconds();
            
        if (_timeOfDay.IsEndOfNight) 
            _timeOfDay.CurrentTime = DayTime.StartOfDay;

        var mainModule = _stars.main;
        //TODO: змiнити _sunCurve на SkyBox.Curve
        mainModule.startColor = new Color(1, 1, 1, 1 - _sunCurve.Evaluate(_timeOfDay.CurrentTime));
            
        _sun.transform.rotation = Quaternion.Euler(_timeOfDay.CurrentTime * 360f, 180, 0);
        _moon.transform.rotation = Quaternion.Euler(_timeOfDay.CurrentTime * 360f + 180f, 180, 0);
        _sun.intensity = _sunIntensity * _sunCurve.Evaluate(_timeOfDay.CurrentTime);
        _moon.intensity = _moonIntensity * _moonCurve.Evaluate(_timeOfDay.CurrentTime);
    }
}