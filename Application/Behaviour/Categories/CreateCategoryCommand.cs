using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Categories
{
    public class CreateCategoryCommand : IRequest<CategoryEntity>
    {
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }

    public class CreateCategoryHandler(ICategoryRepository repository) : IRequestHandler<CreateCategoryCommand, CategoryEntity>
    {
        public async Task<CategoryEntity> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //CQRS
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId,
            };

            await repository.AddAsync(categoryEntity);

            return categoryEntity;
        }
    }
}


