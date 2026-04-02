using System;

namespace FluentBuilder.Builders;

public class Email
{
    public string From { get; private set; } = string.Empty;
    public List<string> To { get; private set; } = [];
    public List<string> Cc { get; private set; } = [];
    public string Subject { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;

    public void Print()
    {
        Console.WriteLine($"From:    {From}");
        Console.WriteLine($"To:      {string.Join(", ", To)}");
        if (Cc.Count > 0)
            Console.WriteLine($"CC:      {string.Join(", ", Cc)}");
        Console.WriteLine($"Subject: {Subject}");
        Console.WriteLine($"Body:    {Body}");
    }

    public sealed class Builder
    {
        private readonly Email _email = new();

        public Builder From(string from)
        {
            _email.From = from;
            return this;
        }

        public Builder To(params string[] recipients)
        {
            _email.To.AddRange(recipients);
            return this;
        }

        public Builder Cc(params string[] recipients)
        {
            _email.Cc.AddRange(recipients);
            return this;
        }

        public Builder Subject(string subject)
        {
            _email.Subject = subject;
            return this;
        }

        public Builder Body(string body)
        {
            _email.Body = body;
            return this;
        }

        public Email Build()
        {
            if (string.IsNullOrWhiteSpace(_email.From))
                throw new InvalidOperationException("Pole 'From' jest wymagane.");
            if (_email.To.Count == 0)
                throw new InvalidOperationException("Wymagany co najmniej jeden odbiorca 'To'.");

            return _email;
        }
    }
}
