using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using TransactionStatement.Entity;
using System.Linq;

namespace TransactionStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> txnSums = txnSummary(1, "debit");
            foreach (List<int> ele in txnSums)
            {
                Console.WriteLine($"{ele[0]}, {ele[1]}");
            }
        }

        public static List<List<int>> txnSummary(int locationId, string transactionType)
        {
            List<List<int>> userIdDetails = new List<List<int>>();
            for (int i = 1; i <= 16; i++)
            {
                using (var client = new WebClient())
                {
                    client.BaseAddress = "https://jsonmock.hackerrank.com/";
                    string path = $"api/transactions/search?txnType={transactionType}&page={i}";

                    try
                    {
                        string response = client.DownloadString(path);

                        RootObject rtObj = JsonConvert.DeserializeObject<RootObject>(response);
                        List<DataRecord> dataRecords = rtObj.data;
                        IEnumerable<UserIdAmount> filteredRecs =  dataRecords.Where(rcrd => rcrd.location.id == locationId)
                            ?.Select(r => new UserIdAmount { userId = r.userId, amount = Convert.ToDecimal(r.amount.Substring(1)) });

                        /* .Select(r => new UserIdAmount { userId = r.userId, amount = Convert.ToInt32(Math.Floor(Convert.ToDecimal(r.amount.Substring(1)))) });
                        .Select(r => new UserIdAmount { userId = r.userId, amount = Convert.ToInt32(Convert.ToDecimal(r.amount.Substring(1))) }); */

                        if (filteredRecs != null)
                        {
                            Dictionary<int, decimal> overfD = new Dictionary<int, decimal>();
                            
                            foreach (UserIdAmount uIA in filteredRecs)
                            {
                                int arr = userIdDetails.IndexOf(userIdDetails.Where(r => r[0] == uIA.userId).FirstOrDefault());

                                decimal overf = Convert.ToDecimal(uIA.amount.ToString().Substring(uIA.amount.ToString().IndexOf('.')));
                                int real = Convert.ToInt32(Convert.ToDecimal(uIA.amount.ToString().Substring(0, uIA.amount.ToString().IndexOf('.'))));

                                if (!overfD.ContainsKey(uIA.userId))
                                {
                                    overfD.Add(uIA.userId, overf);
                                }
                                else
                                {
                                    overfD[uIA.userId] += overf;
                                }

                                if (arr == -1)
                                {
                                    userIdDetails.Add(new List<int>() { uIA.userId, real });
                                }
                                else
                                {
                                    userIdDetails[arr][1] += real + Convert.ToInt32(Math.Floor(overfD[uIA.userId])); 
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception occured: {ex.Message}, stack trace: {ex.StackTrace}");
                    }
                }
            }

            if (userIdDetails.Count == 0)
            {
                userIdDetails.Add(new List<int>() { -1, -1 });
            }

            return userIdDetails.OrderBy(x => x[0]).ToList();
        }
    }
}
