using System;
using System.Collections.Generic;


namespace EX_01
{
    public class MeetingCenter
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MeetingRoom> Rooms { get; set; } = new List<MeetingRoom>();
        
        public MeetingCenter()
        {

        }

        public MeetingCenter(string code, string name, string description, List<MeetingRoom> rooms)
        {
            Code = code;
            Name = name;
            Description = description;
            Rooms = rooms;
        }
    }
}
