

# Admission Analytics | KNUST

This is the analytics for the KNUST 2021/22 admission list

[Admission List](https://apps.knust.edu.gh/admissions/check/)

## Installation

Install [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/install/).

## Usage
Inside the Data directory, is a console application for scrapping data off the admission list and inserting into an [SQL Server Database](https://www.microsoft.com/en-us/sql-server)

The Views directory is a [Blazor app](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) which contains a graphical representation of the scrapped data.

To run any of the projects, a connection string to the database server is required.
 
```c
public static readonly string ConnectionString = "";
```

For the scrapping
``Data > Constants.cs``
And the views
``Views > Data > Constants.cs``

To run any of the projects, run

```bash
dotnet run
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)