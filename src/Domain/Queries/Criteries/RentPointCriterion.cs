using Domain.Entities;

namespace Domain.Queries.Criteries
{
    //!!! А Надо ли делать прям такую критерию? Не стоит ли сделать OnRentPointCriterion? 
    //Всё равно для поиска хотьчего c точки (Employee, etc) всегда будет в критерии только RentPoint
    public class RentPointCriterion : ICriterion
    {
        public RentPoint RentPoint { get; set; }
    }
}
