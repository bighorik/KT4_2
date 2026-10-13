using Domain.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Fto.UserSkill;

public static class MapsterConfiguration
{
    /// <summary>
    /// Регистрирует кастомные маппинги Mapster
    /// </summary>
    public static IServiceCollection AddCustomMapsterMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<UserSkillEntity, UserSkillFto>()
              .Map(dest => dest.SkillName, src => src.Skill != null ? src.Skill.Name : null);

        config.Compile();


        return services;
    }
}