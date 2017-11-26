using System;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Windows.Forms;

namespace EX_01
{
    /*
    This class parse input data and saves output data.
     */
    public class DataLoader
    {
        //Saves output data
        public static void SaveData(string fileName, ObservableCollection<MeetingCenter> data)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName, false))
            {
                outputFile.WriteLine("MEETING_CENTRES\n");
                foreach (MeetingCenter center in data)
                    outputFile.WriteLine(center.ToString());
                outputFile.WriteLine("MEETING_ROOMS\n");
                foreach (MeetingCenter center in data)
                    foreach (MeetingRoom room in center.Rooms)
                        outputFile.WriteLine(room.ToString());
                outputFile.Close();
            }
        }

        //parse input data
        public static void LoadData(string InputFileName, ObservableCollection<MeetingCenter> centres)
        {
            using (TextFieldParser parser = new TextFieldParser(InputFileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();

                //MEETING_CENTRES
                fields = parser.ReadFields();
                while (!fields[0].Equals("MEETING_ROOMS", StringComparison.InvariantCultureIgnoreCase))
                {
                    MeetingCenter center = new MeetingCenter();
                    center.Name = fields[0];
                    center.Code = fields[1];
                    center.Description = fields[2];
                    centres.Add(center);
                    fields = parser.ReadFields();
                }

                //MEETING_ROOMS
                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                    MeetingRoom room = new MeetingRoom();
                    room.Name = fields[0];
                    room.Code = fields[1];
                    room.Description = fields[2];
                    room.Capacity = Int32.Parse(fields[3]);
                    room.VideoConference = fields[4].Equals("yes", StringComparison.InvariantCultureIgnoreCase);
                    room.MeetingCenterCode = fields[5];
                    foreach (MeetingCenter c in centres)
                    {
                        if (c.Code.Equals(fields[5]))
                            c.Rooms.Add(room);
                    }
                }
                parser.Close();
            }
        }
    }
}
