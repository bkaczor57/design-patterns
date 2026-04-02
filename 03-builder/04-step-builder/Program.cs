using StepBuilder; 

var q1 = SqlQueryBuilder.Query()
    .From("employees")
    .SelectAll()
    .Where("department = 'IT'")
    .OrderBy("salary", descending: true)
    .Limit(10)
    .Build();

Console.WriteLine($"  SQL: {q1.ToSql()}");

var q2 = SqlQueryBuilder.Query()
    .From("orders")
    .Select("id", "customer_id", "total")
    .Where("total > 1000")
    .Build();

Console.WriteLine($"  SQL: {q2.ToSql()}");

var q3 = SqlQueryBuilder.Query()
    .From("products")
    .SelectAll()
    .Build();   // bez WHERE — bezpośrednio z IWhereStep

Console.WriteLine($"  SQL: {q3.ToSql()}");

Console.WriteLine("\n  WNIOSEK: Nie możemy napisać SqlQueryBuilder.Query().Build()");
Console.WriteLine("  — kompilator nie pozwoli, bo IFromStep nie ma metody Build()!");