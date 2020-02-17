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
            Dictionary<int, int> arrDic = new Dictionary<int, int>();

            for (int i = 0; i < arrival.Count; i++)
            {
                int a = arrival[i];
                int d = departure[i];

                if (!arrDic.ContainsKey(a))
                {
                    arrDic.Add(a, d);
                }
                else if (!arrDic.ContainsKey(d))
                {
                    arrDic.Add(d, a);
                }
                else {
                    for (int j = a + 1; j <= d; j++)
                    {
                        if (!arrDic.ContainsKey(j))
                        {
                            arrDic.Add(j, j);
                            break;
                        }
                    }
                }
            }

            /* for (int i = 0; i < arrival.Count; i++)
            {
                int a = arrival[i];
                int d = departure[i];

                if (availables.IndexOf(a) == -1)
                {
                    availables.Add(a);
                }
                else
                {
                    for (int j = a + 1; j <= d; j++)
                    {
                        if (availables.IndexOf(j) == -1)
                        {
                            availables.Add(j);
                            break;
                        }
                    }
                }


                if (availables.IndexOf(arrival[i]) > -1 && availables.IndexOf(departure[i]) > -1)
                {
                    continue;
                }
                else
                {
                    if (arrival[i] == departure[i] && availables.IndexOf(arrival[i]) == -1)
                    {
                        availables.Add(arrival[i]);
                        continue;
                    }

                    if (availables.IndexOf(arrival[i]) == -1)
                    {
                        availables.Add(arrival[i]);
                        if (arrival[i] > departure[i] && availables.IndexOf(departure[i]) == -1)
                        {
                            availables.Add(departure[i]);
                        }
                        else
                        {
                            availables.Add(arrival[i]);
                        }
                    }
                    else if (availables.IndexOf(departure[i]) == -1)
                    {
                        availables.Add(departure[i]);
                    }
                } */
            return arrDic.Count;
        }
    }
}
