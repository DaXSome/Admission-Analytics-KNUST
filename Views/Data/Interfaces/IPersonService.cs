namespace Views.Data
{
    public interface IPersonService
    {
        Task<IEnumerable<CourseTotal>> GetCourseTotals();
        Task<IEnumerable<CourseTotal>> GetDegreeTotals();
        Task<IEnumerable<Person>> GetAdmissionAll();
    }
}