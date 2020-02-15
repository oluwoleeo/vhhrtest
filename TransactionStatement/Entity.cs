using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionStatement.Entity
{
    class Location
    {
        public int id { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int zipCode { get; set; }
    }

    class DataRecord
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public object timestamp { get; set; }
        public string txnType { get; set; }
        public string amount { get; set; }
        public Location location { get; set; }
        public string ip { get; set; }
    }

    class RootObject
    {
        public string page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<DataRecord> data { get; set; }
    }

    class UserIdAmount
    {
        public int userId { get; set; }
        public int amount { get; set; }
    }
}
