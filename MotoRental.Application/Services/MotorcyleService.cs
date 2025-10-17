using MotoRental.Api.Domain.Entities;
using MotoRental.Common.Mapping;
using MotoRental.Api.Exceptions;
using MotoRental.Application.Interfaces;
using MotoRental.Infrastructure.Interfaces;
using MotoRental.Common.DTOs.Motorcycle.Request;
using MotoRental.Common.DTOs.Motorcycle.Response;

namespace MotoRental.Api.Application.Services;

public class MotorcyleService : IMotorcycleService
{
    private readonly IMotorcycleRepository _repo;

    public MotorcyleService(IMotorcycleRepository repo)
    {
        _repo = repo;
    }

    public async Task<MotorcycleResponseDto> CreateMotorcycleAsync(CreateMotorcycleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Plate)) throw new BusinessException("Plate is required");
        if (string.IsNullOrWhiteSpace(request.Model)) throw new BusinessException("Model is required");
        if (request.Year < 1900) throw new BusinessException("Invalid year");

        var existing = await _repo.GetByPlateAsync(request.Plate);
        if (existing != null) throw new BusinessException("Plate already exists");

        var moto = new Motorcycle
        {
            Year = request.Year,
            Model = request.Model,
            Plate = request.Plate
        };

        await _repo.AddAsync(moto);

        return moto.ToDto();
    }

    public async Task<IEnumerable<MotorcycleResponseDto>> GetMotorcyclesAsync(string? plateFilter = null)
    {
        var list = await _repo.GetAllAsync(plateFilter);
        return list.Select(m => m.ToDto());
    }

    public async Task UpdatePlateAsync(Guid id, string newPlate)
    {
        var moto = await _repo.GetByIdAsync(id);
        if (moto == null) throw new BusinessException("Motorcycle not found");

        var existing = await _repo.GetByPlateAsync(newPlate);
        if (existing != null && existing.Id != id) throw new BusinessException("Plate already in use");

        moto.Plate = newPlate;
        await _repo.UpdateAsync(moto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var moto = await _repo.GetByIdAsync(id);
        if (moto == null) throw new BusinessException("Motorcycle not found");

        var hasRentals = await _repo.HasRentalsAsync(id);
        if (hasRentals) throw new BusinessException("Cannot delete motorcycle with rentals");

        await _repo.DeleteAsync(moto);
    }
}
