namespace Domain.Entities.HumanEntity
{
    using System;

    public class Client : Human, IEntity
    {
        public Client(string surname, string firstname, string patronymic) : base(surname, firstname, patronymic)
        {
        }
    }
}
