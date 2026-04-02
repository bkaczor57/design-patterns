using FluentBuilder.Builders;

// --- Employee Builder ---
var emp1 = new Employee.Builder()
    .WithFirstName("Jan")
    .WithLastName("Kowalski")
    .WithSalary(8000)
    .WithBirthDate(1990, 5, 15)
    .Build();

var emp2 = new Employee.Builder()
    .WithFirstName("Anna")
    .WithLastName("Nowak")
    .WithSalary(12000)
    .WithBirthDate(1985, 3, 22)
    .Build();

Console.WriteLine(emp1);
Console.WriteLine(emp2);

Console.WriteLine();

// --- Email Builder ---
var email = new Email.Builder()
    .From("nadawca@firma.pl")
    .To("jan@firma.pl", "anna@firma.pl")
    .Cc("manager@firma.pl", "hr@firma.pl")
    .Subject("Spotkanie w piątek")
    .Body("Przypominam o spotkaniu w piątek o 10:00.")
    .Build();

email.Print();
