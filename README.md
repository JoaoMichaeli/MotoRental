# MotoRental API

API para gerenciamento de aluguel de motocicletas, cadastro de entregadores e controle de locações.

## Descrição

O projeto **MotoRental** permite:

* Cadastrar, consultar, atualizar e remover motocicletas.
* Cadastrar entregadores (riders) e enviar fotos da CNH.
* Realizar locações de motocicletas, calcular valores e registrar devoluções.
* Expor endpoints documentados via **Swagger**.

A aplicação é construída em **.NET 8** com **C#**, utilizando arquitetura **DDD**, seguindo princípios de Clean Code.

## Tecnologias

* .NET 8 / C#
* ASP.NET Core Web API
* PostgreSQL (via Docker para integração)
* xUnit + Moq para testes unitários
* FluentAssertions para asserts mais legíveis
* Swagger para documentação da API

## Estrutura de Diretórios

```
MotoRental/
├── MotoRental.Api/
│   ├── Connected Services/
│   ├── Dependencies/
│   ├── Properties/
│   ├── Controllers/
│   │   ├── MotorcyclesControllers/
│   │   ├── RentalsControllers/
│   │   └── RidersControllers/
│   ├── appsettings.json
│   ├── Dockerfile
│   ├── MotoRental.Api.csproj.Backup.irmp
│   ├── MotoRental.Api.htm
│   └── Program.cs
├── MotoRental.Application/
│   ├── Dependencies/
│   └── Services/
│       ├── MotorcyclesServices.cs
│       ├── RentalsServices.cs
│       └── RiderServices.cs
├── MotoRental.Common/
│   ├── Dependencies/
│   ├── DTOs/
│   │   ├── CreateMotorcyclesRequest.cs
│   │   ├── CreateRentalRequest.cs
│   │   ├── CreateRiderRequest.cs
│   │   ├── MotorcyclesDto.cs
│   │   ├── RentalsResponseDto.cs
│   │   └── RiderDto.cs
│   └── Mapping/
│       └── MappingExtensions.cs
├── MotoRental.Domain/
│   ├── Dependencies/
│   ├── Entities/
│   │   ├── Motorcycles.cs
│   │   ├── Rentals.cs
│   │   └── Rider.cs
│   ├── Enums/
│   │   ├── LicenseTypes.cs
│   │   └── RentalsPlan.cs
│   └── Exceptions/
│       └── BusinessExceptions.cs
├── MotoRental.Infrastructure/
│   ├── Dependencies/
│   ├── Persistence/
│   │   └── MotoRentalDbContext.cs
│   └── Repositories/
│       ├── MotorcyclesRepository.cs
│       ├── RentalsRepository.cs
│       └── RiderRepository.cs
└── MotoRental.Tests/
    ├── Dependencies/
    ├── Controllers/
    │   ├── MotorcyclesControllerTests.cs
    │   ├── RentalsControllerTests.cs
    │   └── RidersControllerTests.cs
    ├── Services/
    │   ├── MotorcyclesServiceTests.cs
    │   ├── RentalsServiceTests.cs
    │   └── RiderServiceTests.cs
    └── MappingExtensionsTests.cs
```

## Endpoints da API

### Motos

| Método | Rota                | Descrição                   |
| ------ | ------------------- | --------------------------- |
| POST   | `/motos`            | Cadastrar nova moto         |
| GET    | `/motos`            | Consultar motos existentes  |
| PUT    | `/motos/{id}/placa` | Modificar placa de uma moto |
| GET    | `/motos/{id}`       | Consultar moto por ID       |
| DELETE | `/motos/{id}`       | Remover moto                |

### Entregadores

| Método | Rota                     | Descrição                   |
| ------ | ------------------------ | --------------------------- |
| POST   | `/entregadores`          | Cadastrar entregador        |
| POST   | `/entregadores/{id}/cnh` | Enviar foto da CNH          |
| GET    | `/entregadores/{id}`     | Consultar entregador por ID |

### Locação

| Método | Rota                      | Descrição                                   |
| ------ | ------------------------- | ------------------------------------------- |
| POST   | `/locacao`                | Criar locação                               |
| GET    | `/locacao/{id}`           | Consultar locação por ID                    |
| PUT    | `/locacao/{id}/devolucao` | Informar data de devolução e calcular valor |

## Exemplos de Request / Response

### Motos

**POST /motos**

Request:

```json
{
  "ano": 2020,
  "modelo": "Fan",
  "placa": "RKLO823"
}
```

Response 201:

```json
{
  "identificador": "a1b2c3d4-e5f6-7890-ab12-cd34ef567890",
  "ano": 2020,
  "modelo": "Fan",
  "placa": "RKLO823"
}
```

Response 400:

```json
{
  "mensagem": "Dados inválidos"
}
```

**GET /motos**

Response 200:

```json
[
  {
    "identificador": "a1b2c3d4-e5f6-7890-ab12-cd34ef567890",
    "ano": 2020,
    "modelo": "Fan",
    "placa": "RKLO823"
  }
]
```

### Entregadores

**POST /entregadores**

Request:

```json
{
  "name": "Joao",
  "cnpj": "123456789",
  "birthDate": "2000-01-01T00:00:00",
  "cnhNumber": "ABC1234",
  "licenseType": "A"
}
```

Response 201:

```json
{
  "id": "uuid",
  "name": "Joao",
  "cnpj": "123456789",
  "birthDate": "2000-01-01T00:00:00",
  "cnhNumber": "ABC1234",
  "licenseType": "A"
}
```

**POST /entregadores/{id}/cnh**

Request: FormData com arquivo `cnh`

Response 200:

```json
{
  "message": "CNH uploaded successfully for rider {id}."
}
```

### Locação

**POST /locacao**

Request:

```json
{
  "motorcycleId": "uuid-moto",
  "riderId": "uuid-rider",
  "plan": 7,
  "expectedEndDate": "2025-10-24T00:00:00"
}
```

Response 201:

```json
{
  "id": "uuid-rental",
  "motorcycleId": "uuid-moto",
  "riderId": "uuid-rider",
  "plan": 7,
  "startDate": "2025-10-17T00:00:00",
  "expectedEndDate": "2025-10-24T00:00:00",
  "actualEndDate": null,
  "pricePerDay": 30.0
}
```

**PUT /locacao/{id}/devolucao**

Request Query:

```
dataDevolucao=2025-10-24
```

Response 200:

```json
{
  "id": "uuid-rental",
  "motorcycleId": "uuid-moto",
  "riderId": "uuid-rider",
  "plan": 7,
  "startDate": "2025-10-17T00:00:00",
  "expectedEndDate": "2025-10-24T00:00:00",
  "actualEndDate": "2025-10-24T00:00:00",
  "pricePerDay": 30.0
}
```

## Testes

* **Unitários**: services e controllers usando `xUnit` e `Moq`.
* **Integração**: PostgreSQL via Docker, validando CRUD real no banco.

Executar testes:

```bash
dotnet test
```

## Executando o projeto

1. Restaurar pacotes:

```bash
dotnet restore
```

2. Rodar API:

```bash
dotnet run --project MotoRental.Api
```

3. Acessar Swagger:

```
http://localhost:8080/swagger
```

4. (Opcional) Banco PostgreSQL via Docker:

```bash
docker-compose up --build
```

## Observações

* Todos os DTOs possuem **JSON Property Names** compatíveis com Swagger.
* Arquitetura **Clean Architecture / DDD**: controllers, services, DTOs, entidades e repositórios separados.
* Testes unitários cobrem **cenários positivos e negativos**.
