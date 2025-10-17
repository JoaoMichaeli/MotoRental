using MotoRental.Api.Domain.Entities;
using MotoRental.Common.DTOs.Motorcycle.Response;
using MotoRental.Common.DTOs.Rental.Response;
using MotoRental.Common.DTOs.Rider.Response;

namespace MotoRental.Common.Mapping;

public static class MappingExtensions
{
    public static MotorcycleResponseDto ToDto(this Motorcycle m) =>
        new MotorcycleResponseDto { Id = m.Id, Year = m.Year, Model = m.Model, Plate = m.Plate };

    public static RiderResponseDto ToDto(this Rider r) =>
        new RiderResponseDto { Id = r.Id, Name = r.Name, Cnpj = r.Cnpj, BirthDate = r.BirthDate, CnhNumber = r.CnhNumber, LicenseType = r.LicenseType };

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