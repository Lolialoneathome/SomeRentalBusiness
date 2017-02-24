using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class BikeViewModel
    {
        public string RentPointAdress { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public string HourCost { get; set; }
        public BikeViewModel(string rpAdress)
        {
            RentPointAdress = rpAdress;
        }

    }
}
