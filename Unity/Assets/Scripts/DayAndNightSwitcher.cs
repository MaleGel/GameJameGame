using System;
using UnityEngine;

namespace Assets.Scripts
{
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
        [SerializeField] private Light _sun;
        private DayTime _timeOfDay;

        private void Start()
        {
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
            
            
            _sun.transform.rotation = Quaternion.Euler(_timeOfDay.CurrentTime * 360f, 180, 0);
        }
    }
}
