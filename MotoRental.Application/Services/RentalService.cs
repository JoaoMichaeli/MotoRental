using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Enums;
using MotoRental.Api.Exceptions;
using MotoRental.Application.Interfaces;
using MotoRental.Common.DTOs.Rental.Request;
using MotoRental.Common.DTOs.Rental.Response;
using MotoRental.Common.Mapping;
using MotoRental.Infrastructure.Interfaces;

namespace MotoRental.Api.Application.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepo;
    private readonly IMotorcycleRepository _motoRepo;
    private readonly IRiderRepository _riderRepo;

    public RentalService(IRentalRepository rentalRepo, IMotorcycleRepository motoRepo, IRiderRepository riderRepo)
    {
        _rentalRepo = rentalRepo;
        _motoRepo = motoRepo;
        _riderRepo = riderRepo;
    }

    private decimal PricePerDayForPlan(RentalPlan plan) => plan switch
    {
        RentalPlan.Days7 => 30m,
        RentalPlan.Days15 => 28m,
        RentalPlan.Days30 => 22m,
        RentalPlan.Days45 => 20m,
        RentalPlan.Days50 => 18m,
        _ => throw new BusinessException("Invalid plan")
    };

    public async Task<RentalResponseDto> CreateRentalAsync(CreateRentalRequest request)
    {
        var moto = await _motoRepo.GetByIdAsync(request.MotorcycleId) ?? throw new BusinessException("Motorcycle not found");
        var rider = await _riderRepo.GetByIdAsync(request.RiderId) ?? throw new BusinessException("Rider not found");

        if (!(rider.LicenseType == LicenseType.A || rider.LicenseType == LicenseType.AB))
            throw new BusinessException("Rider not allowed to rent - license A required");

        var createdAt = DateTime.UtcNow;
        var start = createdAt.Date.AddDays(1);

        var expected = request.ExpectedEndDate.Date;
        var expectedFromPlan = start.AddDays((int)request.Plan - 1).Date;
        if (expected != expectedFromPlan)
            throw new BusinessException($"Expected end date must be {expectedFromPlan:yyyy-MM-dd} for the selected plan");

        var pricePerDay = PricePerDayForPlan(request.Plan);

        var rental = new Rental
        {
            MotorcycleId = moto.Id,
            RiderId = rider.Id,
            Plan = request.Plan,
            CreatedAt = createdAt,
            StartDate = start,
            ExpectedEndDate = expected,
            PricePerDay = pricePerDay
        };

        await _rentalRepo.AddAsync(rental);

        return rental.ToDto();
    }

    public async Task<RentalResponseDto> ReturnRentalAsync(Guid rentalId, DateTime actualReturnDate)
    {
        var rental = await _rentalRepo.GetByIdAsync(rentalId) ?? throw new BusinessException("Rental not found");
        if (rental.ActualEndDate != null) throw new BusinessException("Rental already closed");

        rental.ActualEndDate = actualReturnDate.Date;
        await _rentalRepo.UpdateAsync(rental);

        return rental.ToDto();
    }

    public async Task<RentalResponseDto?> GetByIdAsync(Guid id)
    {
        var r = await _rentalRepo.GetByIdAsync(id);
        return r?.ToDto();
    }
}
