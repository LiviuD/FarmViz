using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public class Feed: IActivity
    {
        public Feed(DateTime dateTime, decimal amount)
        {
            DateTime = dateTime;
            Amount = amount;
        }

        public DateTime DateTime { get; set; }
        public decimal Amount { get; set;}
    }
}
