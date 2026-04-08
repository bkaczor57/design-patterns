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

var emailBuilder = new Email.Builder();

var email = emailBuilder
    .From("nadawca@firma.pl")
    .To("jan@firma.pl", "anna@firma.pl")
    .Cc("manager@firma.pl", "hr@firma.pl")
    .Subject("Spotkanie w piątek")
    .Body("Przypominam o spotkaniu w piątek o 10:00.")
    .Build();

emailBuilder.Reset();

var email2 = emailBuilder
    .From("odbiorca@firma.pl")
    .To("pawel@firma.pl", "grzegorz@firma.pl")
    .Cc("prezes@firma.pl", "it@firma.pl")
    .Subject("Spotkanie w czwartek")
    .Body("Przypominam o spotkaniu w czwartek o 17:00.")
    .Build();

email.Print();
email2.Print();