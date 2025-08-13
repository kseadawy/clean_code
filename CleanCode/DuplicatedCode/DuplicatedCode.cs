
using System;

namespace CleanCode.DuplicatedCode
{
    internal class TimeResult
    {
        public TimeResult(bool isTime, int time)
        {
            IsTime = isTime;
            Time = time;
        }

        public bool IsTime { get; }
        public int Time { get; }
    }

    internal class Time
    {
        

        public Time(int hours, int minutes)
        {
            Minutes = minutes;
            Hours = hours;
        }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public static Time GetTime(string admissionDateTime)
        {
            
            var timeResult = ValidateAdmissionDateTime(admissionDateTime);
            var time = timeResult.Time;
            int hours;
            int minutes;
            if (timeResult.IsTime)
            {
                    hours = time / 100;
                    minutes = time % 100;
            }
            else
                throw new ArgumentNullException("admissionDateTime");

            return new Time(hours, minutes);
        }

        private static TimeResult ValidateAdmissionDateTime(string admissionDateTime)
        {
            var time = 0;
            return new TimeResult(!string.IsNullOrWhiteSpace(admissionDateTime) && int.TryParse(admissionDateTime.Replace(":", ""), out time), time);
        }
    }

    class DuplicatedCode
    {
        public void AdmitGuest(string name, string admissionDateTime)
        {
            var time = Time.GetTime(admissionDateTime);

            // Some more logic 
            // ...
            if (time.Hours < 10)
            {

            }
        }

        public void UpdateAdmission(int admissionId, string name, string admissionDateTime)
        {


            var time = Time.GetTime(admissionDateTime);

            // Some more logic 
            // ...
            if (time.Hours < 10)
            {

            }
        }
    }
}
