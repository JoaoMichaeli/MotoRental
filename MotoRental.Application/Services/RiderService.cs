using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Exceptions;
using MotoRental.Api.Infrastructure.Repositories;
using MotoRental.Common.Mapping;

namespace MotoRental.Api.Application.Services;

public class RiderService : IRiderService
{
    private readonly IRiderRepository _repo;
    public RiderService(IRiderRepository repo) { _repo = repo; }

    public async Task<RiderDto> CreateRiderAsync(CreateRiderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Cnpj)) throw new BusinessException("CNPJ required");
        if (string.IsNullOrWhiteSpace(request.CnhNumber)) throw new BusinessException("CNH number required");

        var byCnpj = await _repo.GetByCnpjAsync(request.Cnpj);
        if (byCnpj != null) throw new BusinessException("CNPJ already exists");

        var byCnh = await _repo.GetByCnhNumberAsync(request.CnhNumber);
        if (byCnh != null) throw new BusinessException("CNH number already exists");

        var rider = new Rider
        {
            Name = request.Name,
            Cnpj = request.Cnpj,
            BirthDate = request.BirthDate,
            CnhNumber = request.CnhNumber,
            LicenseType = request.LicenseType
        };

        await _repo.AddAsync(rider);
        return rider.ToDto();
    }

    public async Task<RiderDto?> GetByIdAsync(Guid id)
    {
        var r = await _repo.GetByIdAsync(id);
        return r?.ToDto();
    }
}
