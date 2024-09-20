
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.Extensions.Logging;


global using Mapster;
global using MediatR;
global using Carter;
global using FluentValidation;


global using Shared.Pagination;
global using Shared.Data;
global using Shared.Data.Seed;
global using Shared.DDD;
global using Shared.Contracts.CQRS;


global using Catalog.Products.Models;
global using Catalog.Products.Events;
global using Catalog.Data;
global using Catalog.Data.Seed;
global using Catalog.Contracts.Products.Dtos;
global using Catalog.Products.Exceptions;
global using Catalog.Contracts.Products.Features.GetProductById;
