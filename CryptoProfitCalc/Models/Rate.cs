using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProfitCalc.Models {
  class Rate {

    public string result { get; set; }
    public int timestamp { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public decimal rate { get; set; }


    //"result":"success","timestamp":1515715090,"from":"USD","to":"GBP","rate":0.73852
  }
}
