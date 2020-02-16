using System;
using System.Collections.Generic;

namespace MeetingSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = new List<int>() { 1, 1, 2 };
            List<int> dep = new List<int>() { 1, 2, 2 };

            Console.WriteLine(countMeetings(arr, dep));
        }

        public static int countMeetings(List<int> arrival, List<int> departure)
        {
            List<int> availables = new List<int>();

            for(int i = 0; i < arrival.Count; i++)
            {
                if (arrival[i] == departure[i] && availables.IndexOf(arrival[i]) == -1)
                {
                    availables.Add(arrival[i]);
                    continue;
                }
                else if (arrival[i] == departure[i] && availables.IndexOf(arrival[i]) > -1)
                {
                    continue;
                }
                
                if (availables.IndexOf(arrival[i]) == -1)
                {
                    availables.Add(arrival[i]);
                    /* if (arrival[i] > departure[i] && availables.IndexOf(departure[i]) == -1)
                    {
                        availables.Add(departure[i]);
                    }
                    else
                    {
                        availables.Add(arrival[i]);
                    } */
                }
                else if (availables.IndexOf(departure[i]) == -1) {
                    availables.Add(departure[i]);
                }
            }
            return availables.Count;
        }
    }
}
