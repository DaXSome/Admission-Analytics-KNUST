using Views.Data;
using Views.Constants;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace Views.Data
{
    public class PersonService : IPersonService
    {
        public async Task<IEnumerable<CourseTotal>> GetCourseTotals()
        {
            using SqlConnection conn = new SqlConnection(Constants.Constants.ConnectionString);

            IEnumerable<CourseTotal> courses = await conn.QueryAsync<CourseTotal>(Queries.CourseTotal, commandType: CommandType.Text);

            return courses;
        }

        public async Task<IEnumerable<CourseTotal>> GetDegreeTotals()
        {
            using SqlConnection conn = new SqlConnection(Constants.Constants.ConnectionString);

            IEnumerable<CourseTotal> courses = await conn.QueryAsync<CourseTotal>(Queries.DegreeTotal, commandType: CommandType.Text);

            return courses;
        }

        public async Task<IEnumerable<Person>> GetAdmissionAll()
        {
            using SqlConnection conn = new SqlConnection(Constants.Constants.ConnectionString);

            IEnumerable<Person> persons = await conn.QueryAsync<Person>(Queries.AdmissionAll, commandType: CommandType.Text);

            return persons;
        }
    }
}