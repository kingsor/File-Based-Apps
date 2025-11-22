var targetDate = new DateTime(2025, 12, 31);
Console.WriteLine($"New Year's 2025 falls on a {targetDate.DayOfWeek}");
Console.WriteLine($"That's {(targetDate - DateTime.Today).Days} days from now");

targetDate = new DateTime(2025, 12, 25);
Console.WriteLine($"XMas on 2025 fall on a {targetDate.DayOfWeek}");
Console.WriteLine($"That's {(targetDate - DateTime.Today).Days} days from now");

