using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberBreakfast;

public static class Errors {
    //create Breakfast class in case other entities are added to project.
    // This allows us to delineate the error codes for each entity
    public static class Breakfast {
        
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be between {Models.Breakfast.MinNameLength} " +
                         $"and {Models.Breakfast.MaxNameLength} characters long"
        );
        
        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must be between {Models.Breakfast.MinDescriptionLength} " +
                         $"and {Models.Breakfast.MaxDescriptionLength} characters long"
        );
        // public static Error InvalidDescription => Error.Validation(
        //     code: "Breakfast.InvalidDescription",
        //     description: $"Breakfast description must be at least {Models.Breakfast.MindDescriptionLength} + characters long"
        // );
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound", 
            description: "Breakfast Not Found");
    }
}
