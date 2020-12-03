using System;
using System.Collections.Generic;
using System.Text;

namespace internship_3_oop_intro
{
    public class Event
    {
        public Event(string name, int eventType, int startTime, int endTime)
        {
            Name = name;
            TypeOfEvent = (EventType)eventType;
            StartTime = startTime;
            EndTime = endTime;

        }
        public string Name { get; set; }
        public EventType TypeOfEvent { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }






    }
}
