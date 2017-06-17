public class Person
{
  public Person(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
  }
  public string FirstName { get; }
  public string LastName { get; }
  public string FullName => $"{FirstName} {LastName}";
}