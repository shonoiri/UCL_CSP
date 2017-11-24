using System;
using System.ComponentModel;


namespace EX_01
{
    public class MeetingRoom : IEditableObject
    {
        struct RoomData
        {
            internal string code;
            internal string name;
            internal string description;
            internal string meetingCenterCode;
            internal int capacity;
            internal bool videoConference;
        }

        private RoomData roomData;
        private RoomData backupData;

        public MeetingRoom(string code, string name, string description, string meetingCenterCode, int capacity, bool videoConference)
        {
            this.roomData = new RoomData();
            this.roomData.code = code;
            this.roomData.name = name;
            this.roomData.description = description;
            this.roomData.meetingCenterCode = meetingCenterCode;
            this.roomData.capacity = capacity;
            this.roomData.videoConference = videoConference;
        }

        public MeetingRoom()
        {
        }

        public void BeginEdit()
        {
            this.backupData = roomData;
        }

        public void EndEdit()
        {
            this.backupData = new RoomData();
        }

        public void CancelEdit()
        {
            this.roomData = backupData;
        }

        public string Name {
            get {
                return this.roomData.name;
            }
            set {
                this.roomData.name = value;
            }
        }

        public string Code {
            get {
                return this.roomData.code;
            }
            set {
                this.roomData.code = value;
            }
        }

        public string Description {
            get {
                return this.roomData.description;
            }
            set {
                this.roomData.description = value;
            }
        }

        public string MeetingCenterCode
        {
            get {
                return this.roomData.meetingCenterCode;
            }
            set {
                this.roomData.meetingCenterCode = value;
            }
        }

        public int Capacity {
            get {
                return this.roomData.capacity;
            }
            set {
                this.roomData.capacity = value;
            }
        }

        public bool VideoConference {
            get {
                return this.roomData.videoConference;
            }
            set {
                this.roomData.videoConference = value;
            }
        }
    }
}
