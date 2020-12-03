using System;
using System.Collections.Generic;
using System.Text;

namespace internship_3_oop_intro
{
    public class Event
    {
        public Event(string name, int eventType)
        {
            Name = name;
            TypeOfEvent = (EventType)eventType;

        }
        public string Name { get; set; }
        public EventType TypeOfEvent { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }






    }
}
