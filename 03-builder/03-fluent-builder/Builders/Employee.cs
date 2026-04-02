using System;

namespace FluentBuilder.Builders;

public class Employee
{
    private readonly int Id;
    private readonly string FirstName;
    private readonly string LastName;
    private readonly int Salary;
    private readonly DateTime BirthDate;

    public string FullName() =>$"{FirstName} {LastName}";

    public int Age()
    {
        var age = DateTime.Today.Year - BirthDate.Year;
        if(BirthDate>DateTime.Today.AddYears(-age)) 
            age--;
        return age;
    }

     public override string ToString() =>
        $"Employee #{Id}: {FullName()}, age={Age()}, salary={Salary:N0} PLN";
    private Employee(Builder b)
    {
        Id = b.Id;
        FirstName = b.FirstName;
        LastName = b.LastName;
        Salary = b.Salary;
        BirthDate = b.BirthDate;
    }

    public class Builder
    {
        internal static int _nextId = 1;
        public int Id {get;} = _nextId++;
        public string FirstName {get; private set;} = "First Name";
        public string LastName {get; private set;} = "Second Name";
        public int Salary {get; private set;} = 5000;
        public DateTime BirthDate {get;private set;} = new DateTime(1990,1,1);
        public Builder WithFirstName(string firstName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            return this;
        }

        public Builder WithLastName(string lastName)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            return this;
        }

        public Builder WithSalary(int salary)
        {
            if(salary < 0) throw new ArgumentNullException(nameof(salary));
            Salary = salary;
            return this;
        }

        public Builder WithBirthDate(DateTime birthDate)
        {
            if(birthDate>DateTime.Today)
                throw new ArgumentException("Data urodzenia nie może być w przyszłości");
            BirthDate = birthDate;
            return this;
        }

        public Builder WithBirthDate(int year, int month, int day) =>
            WithBirthDate(new DateTime(year,month,day));

        public Employee Build()
        {
            return new Employee(this);
        }
        
    }



    
}
