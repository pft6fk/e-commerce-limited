namespace e_commerce.Domain.Models;

public class Customer : AggregateRoot<CustomerId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Customer(string firstName, string lastName, string email)
    {
        if(!IsValidEmail(email))
            throw new ArgumentNullException("Provide valid email");

        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.RegisteredAt = DateTime.UtcNow;
    }
    private bool IsValidEmail(string email)
    {
        EmailAddressAttribute e = new EmailAddressAttribute();
        if (e.IsValid(email))
            return true;
        else
            return false;
    }
}
