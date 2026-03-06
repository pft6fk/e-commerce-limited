namespace e_commerce.Domain.Models;

public class Customer : AggregateRoot<CustomerId>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime RegisteredAt { get; private set; }
    private Customer()
    {
        
    }
    public Customer(string firstName, string lastName, string email)
        : base(CustomerId.New())
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
