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
        public decimal Cost { get; set; }
        public decimal HourCost { get; set; }
        public BikeViewModel()
        {
        }

    }
}
