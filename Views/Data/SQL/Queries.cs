namespace Views.Data
{
    public class Queries
    {
        public const string CourseTotal = @"
        SELECT DISTINCT COUNT(Course) as Total, Course FROM Person
        GROUP BY Course 
        ORDER BY COUNT(Course) DESC";

        public const string DegreeTotal = @"
        SELECT DISTINCT COUNT(Degree) as Total, Degree as Course FROM Person
        GROUP BY Degree 
        ORDER BY COUNT(Degree) DESC";

        public const string AdmissionAll = @"
        SELECT * FROM Person";
    }
}