using MoviesRental.Core.DomainObjects;

namespace MoviesRental.Domain.Entities;
public class Director : Entity
{
    protected Director()
    { }

    public Director(string name, string surname)
    {
        UpdateName(name);
        UpdateName(surname);
    }

    public const int MIN_LENGTH = 3;
    public const int MAX_LENGTH = 30;

    public string Name { get; private set; }
    public string Surname { get; private set; }
    private List<Dvd> _dvs = new();
    public IReadOnlyList<Dvd> Dvds => _dvs;

    public string Fullname()
        => $"{Name} {Surname}";

    public void UpdateName(string name)
    {
        if (ValidateName(name))
            throw new DomainException($"Invalid name for director");

        Name = name;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateSurname(string surname)
    {
        if (ValidateName(surname))
            throw new DomainException($"Invalid surname for director");

        Surname = surname;
        UpdatedAt = DateTime.Now;
    }

    private bool ValidateName(string value)
    {
        if (string.IsNullOrEmpty(value)
            || value.Length < MIN_LENGTH
            || value.Length > MAX_LENGTH)
        {
            return false;
        }
        //da pra criar um regex para validar
        return true;
    }

}
