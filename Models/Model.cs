namespace DesDer.Models;

public abstract class Model
{
    public Guid Id
    {
        get; set;
    }

    public Model()
    {
        Id = Guid.NewGuid();
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is not Model model)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return model.Id.Equals(Id);
    }
}
