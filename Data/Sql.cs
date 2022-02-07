namespace Sql
{
    public class Queries
    {

        public const string CreatePerson = @"
        IF OBJECT_ID('Person') IS NULL
            BEGIN
                CREATE TABLE dbo.[Person] (
                    [ID] INT NOT NULL,
                    [StudentID] INT NOT NULL,
                    [Course] VARCHAR(250) NOT NULL,
                    [Degree] VARCHAR(35) NOT NULL,
                    [FirstName] VARCHAR(50) NOT NULL,
                    [Surname] VARCHAR(50) NOT NULL,
                    CONSTRAINT PK_Person PRIMARY KEY (ID)

                )
            END
        ";
        public const string InsertPerson = @"
        INSERT INTO dbo.[Person]
        VALUES (
            @ID,
            @StudentID,
            @Course,
            @Degree,
            @FirstName,
            @Surname
        )
        ";
    }
}