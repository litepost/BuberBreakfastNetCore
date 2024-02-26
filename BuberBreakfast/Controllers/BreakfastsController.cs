using BuberBreakfast.Contracts;
using BuberBreakfast.Models;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("[controller]")] // This line uses the name of the controller to create the prefix. Ex. breakfasts
public class BreakfastsController : ApiController
{
    // This is dependency injection
    private readonly IBreakfastService _breakfastService;
    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    // Convert request into an internal representation --> 
    //able to change the contract and only need to change the request -> model mapping
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var createBreakfastResult = Breakfast.From(request);

        if (createBreakfastResult.IsError) {
            return Problem(createBreakfastResult.Errors);
        }

        Breakfast breakfast = createBreakfastResult.Value;
        ErrorOr<Created> CreateBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return CreateBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );
        // if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        // {
        //     return NotFound();
        // }

        // var breakfast = getBreakfastResult.Value;
        // BreakfastResponse response = MapBreakfastResponse(breakfast);

        // return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> createBreakfastResult = Breakfast.From(id, request);

        if (createBreakfastResult.IsError) {
            return Problem(createBreakfastResult.Errors);
        }

        Breakfast breakfast = createBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertedBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

        return upsertedBreakfastResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private IActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
    }

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
                    Id: breakfast.Id,
                    Name: breakfast.Name,
                    Description: breakfast.Description,
                    StartDateTime: breakfast.StartDateTime,
                    EndDateTime: breakfast.EndDateTime,
                    LastModifiedDateTime: breakfast.LastModifiedDate,
                    Savory: breakfast.Savory,
                    Sweet: breakfast.Sweet);
    }
}
