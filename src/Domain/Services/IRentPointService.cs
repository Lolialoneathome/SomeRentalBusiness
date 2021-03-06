﻿using Domain.Entities;
using Domain.Entities.HumanEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IRentPointService
    {
        RentPoint AddRentPoint(string name, string adress, Employee employee, decimal money = 10000);

        void CloseRentPoint(RentPoint rentPoint);
    }
}
