using BuberBreakfast.Models;
using BuberBreakfast.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast;

public class BreakfastService : IBreakfastService
{
    private readonly BuberBreakfastDbContext _dbContext;

    public BreakfastService(BuberBreakfastDbContext dbContext) {
        _dbContext = dbContext;
    }

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast) {
        _dbContext.Add(breakfast);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id) {
        Console.WriteLine($"********** {id} **********");
        Console.WriteLine($"********** Type: {id.GetType()} **********");
        // var breakfast = _dbContext.Breakfasts.Find(id);
        IQueryable<Breakfast>? breakfastResult = _dbContext.Set<Breakfast>()
            .AsNoTracking()
            .Where(x => x.Id == id);

        if (breakfastResult is null) {
            return Errors.Breakfast.NotFound;
        }

        try {
            Console.WriteLine("===================");
            Console.WriteLine($"{_dbContext.ChangeTracker.DebugView.LongView}");
            Console.WriteLine("===================");
            _dbContext.Remove(id);
            _dbContext.SaveChanges();
        }
        catch (Exception) {
            _dbContext.ChangeTracker.Clear();
            throw;
        }

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id) {
        var breakfast = _dbContext.Breakfasts.Find(id);

        if (breakfast is null) {
            return Errors.Breakfast.NotFound;
        }

        return breakfast;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast) {
        bool isNewlyCreated = false;
        // var breakfastResult = _dbContext.Breakfasts.Find(breakfast.Id);
        IQueryable<Breakfast>? breakfastResult = _dbContext.Set<Breakfast>()
            .AsNoTracking()
            .Where(x => x.Id == breakfast.Id);

        Console.WriteLine($"{DateTime.Now}\tUpsert breakfastResult");
        Console.WriteLine($"{_dbContext.ChangeTracker.DebugView.ShortView}");
        
        if (breakfastResult is null) {
            isNewlyCreated = true;
            _dbContext.Add(breakfast);
        }
        else {
            // _dbContext.Update(breakfast);
            _dbContext.Entry(breakfast).State = EntityState.Detached;
            _dbContext.Set<Breakfast>().Update(breakfast);
        }
        
        _dbContext.SaveChanges();

        return new UpsertedBreakfast(isNewlyCreated);
    }
}
