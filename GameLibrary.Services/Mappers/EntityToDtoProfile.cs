using AutoMapper;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Mappers;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<DeveloperEntity, DeveloperDto>();
        CreateMap<GameEntity, GameDto>();
        CreateMap<GenreEntity, GenreDto>();
    }
}