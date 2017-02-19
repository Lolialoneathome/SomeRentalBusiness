namespace Domain.Entities.HumanEntity
{
    using System;

    public class Employee : Human, IEntity
    {
        public Employee(string surname, string firstname, string patronymic) : base(surname, firstname, patronymic)
        {
        }
    }
}
