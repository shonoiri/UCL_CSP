using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace EX_01
{
    public class MeetingCenter : IEditableObject
    {
        struct CenterData
        {
            internal string code;
            internal string name;
            internal string description;
        }

        private CenterData centerData;
        private CenterData backupData;

        private List<MeetingRoom> rooms { get; set; } = new List<MeetingRoom>();

        public MeetingCenter()
        {

        }

        public MeetingCenter(string code, string name, string description, List<MeetingRoom> rooms)
        {
            this.centerData = new CenterData();
            this.centerData.code = code;
            this.centerData.name = name;
            this.centerData.description = description;
            this.rooms = rooms;
        }

        public void BeginEdit()
        {
            this.backupData = centerData;
        }

        public void EndEdit()
        {
            this.backupData = new CenterData();
        }

        public void CancelEdit()
        {
            this.centerData = backupData;
        }

        public String Name
        {
            get
            {
                return this.centerData.name;
            }
            set
            {
                this.centerData.name = value;
            }
        }

        public string Code
        {
            get
            {
                return this.centerData.code;
            }
            set
            {
                this.centerData.code = value;
            }
        }

        public string Description
        {
            get
            {
                return this.centerData.description;
            }
            set
            {
                this.centerData.description = value;
            }
        }

        public List<MeetingRoom> Rooms
        {
            get
            {
                return this.rooms;
            }

            set
            {
                this.rooms = value;
            }
        }
    }
}
