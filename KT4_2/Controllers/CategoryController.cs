using System.Runtime.InteropServices;
using Application.Behaviour.Categories;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Category;

namespace KT4_2.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<CategoryDto> CreateCategory(CategoryToCreateDto dto)
        {
            CreateCategoryCommand command = dto.Adapt<CreateCategoryCommand>();

            CategoryEntity category = await mediator.Send(command);

            CategoryDto result = category.Adapt<CategoryDto>();

            return result;
        }
    }
}


