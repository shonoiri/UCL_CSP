using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_01
{
    public class MeetingRoom
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MeetingCenterCode { get; set; }
        public int Capacity { get; set; }
        public bool VideoConference { get; set; }

        public MeetingRoom(string code, string name, string description, string meetingCenterCode, int capacity, bool videoConference)
        {
            Code = code;
            Name = name;
            Description = description;
            MeetingCenterCode = meetingCenterCode;
            Capacity = capacity;
            VideoConference = videoConference;
        }

        public MeetingRoom()
        {
        }
    }
}
