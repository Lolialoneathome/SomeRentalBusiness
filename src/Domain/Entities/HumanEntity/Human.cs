using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.HumanEntity
{
    public abstract class Human
    {
        public Human(string surname, string firstname, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));

            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentNullException(nameof(firstname));

            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentNullException(nameof(patronymic));

            Surname = surname;
            FirstName = firstname;
            Patronymic = patronymic;
        }



        public readonly string Surname;

        public readonly string FirstName;

        public readonly string Patronymic;

        public string FullName => $"{Surname} {FirstName} {Patronymic}";
    }
}
