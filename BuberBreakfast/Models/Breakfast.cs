using BuberBreakfast.Contracts;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public DateTime LastModifiedDate { get; private set; }
    public List<string>? Savory { get; private set; }
    public List<string>? Sweet { get; private set; }

    private Breakfast() { }

    private Breakfast(
        Guid id, 
        string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        DateTime lastModifiedDate,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDate = lastModifiedDate;
        Savory = savory;
        Sweet = sweet;
    }

    // ? implies that the field is optional
    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet,
        Guid? id = null
    ) 
    {
        List<Error> errors = new List<Error>();

        if (name.Length < MinNameLength || name.Length > MaxNameLength) {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength) {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if (errors.Count > 0)
            return errors;

        return new Breakfast(
            id: id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime, 
            DateTime.UtcNow,
            savory,
            sweet
        );
    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request) {
        return Create(
            name: request.Name,
            description: request.Description,
            startDateTime: request.StartDateTime,
            endDateTime: request.EndDateTime,
            savory: request.Savory,
            sweet: request.Sweet
        );
    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request) {
        return Create(
            name: request.Name,
            description: request.Description,
            startDateTime: request.StartDateTime,
            endDateTime: request.EndDateTime,
            savory: request.Savory,
            sweet: request.Sweet,
            id: id
        );
    }
}
