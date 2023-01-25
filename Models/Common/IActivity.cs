using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmVizModels
{
    public interface IActivity
    {
        DateTime DateTime { get; set; }
        decimal Amount { get; set;}
    }
}
