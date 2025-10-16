using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Domain.Entities;

namespace MotoRental.Common.Mapping;

public static class MappingExtensions
{
    public static MotorcycleDto ToDto(this Motorcycle m) =>
        new MotorcycleDto { Id = m.Id, Year = m.Year, Model = m.Model, Plate = m.Plate };

    public static RiderDto ToDto(this Rider r) =>
        new RiderDto { Id = r.Id, Name = r.Name, Cnpj = r.Cnpj, BirthDate = r.BirthDate, CnhNumber = r.CnhNumber, LicenseType = r.LicenseType };

    public static RentalResponseDto ToDto(this Rental r) =>
        new RentalResponseDto
        {
            Id = r.Id,
            MotorcycleId = r.MotorcycleId,
            RiderId = r.RiderId,
            Plan = r.Plan,
            StartDate = r.StartDate,
            ExpectedEndDate = r.ExpectedEndDate,
            ActualEndDate = r.ActualEndDate,
            PricePerDay = r.PricePerDay
        };
}