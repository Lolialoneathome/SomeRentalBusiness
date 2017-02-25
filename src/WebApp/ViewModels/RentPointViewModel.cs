using Domain.Entities;
using Domain.Entities.Deposits;
using Domain.Entities.HumanEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class RentPointViewModel
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeePatronymic { get; set; }
        public decimal MoneyInCashBox { get; set; }
        public decimal MoneyInSafe { get; set; }
        public List<PassportDeposit> PassportsInsafe = new List<PassportDeposit>();
        public string EmployeeFullName => $"{EmployeeSurname} {EmployeeName} {EmployeePatronymic}";

        public int Money { get; set; }
        public List<Bike> Bikes { get; set; }

        public RentPointViewModel()
        {

        }

        public RentPointViewModel(RentPoint rp)
        {
            Name = rp.Name;
            Adress = rp.Adress;
            EmployeeName = rp.Employee.FirstName;
            EmployeeSurname = rp.Employee.Surname;
            EmployeePatronymic = rp.Employee.Patronymic;
            Bikes = rp.Bikes.ToList();
            MoneyInCashBox = rp.CashBox.Money;
            MoneyInSafe = rp.Safe.Money;
            PassportsInsafe = rp.Safe.PassportDeposits.ToList();
        }
    }
}
