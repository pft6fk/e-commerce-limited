namespace e_commerce.Domain.Models;

public class Customer : AggregateRoot<CustomerId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Customer(string firstName, string lastName, string email)
    {
        if(string.IsNullOrEmpty(email))
            throw new ArgumentNullException("Email cannot be null");

        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.RegisteredAt = DateTime.UtcNow;
    }
}
