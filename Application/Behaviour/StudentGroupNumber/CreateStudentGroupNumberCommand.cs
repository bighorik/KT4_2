using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class CreateStudentGroupNumberCommand : IRequest<ResponseContract<StudentGroupNumberEntity>>
    {
        public required string Name { get; set; }
    }

    public class CreateStudentGroupNumberCommandHandler(
        IStudentGroupNumberRepository groupREpository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStudentGroupNumberCommand, ResponseContract<StudentGroupNumberEntity>>
    {
        public async Task<ResponseContract<StudentGroupNumberEntity>> Handle(CreateStudentGroupNumberCommand request, CancellationToken cancellationToken)
        {
            StudentGroupNumberEntity group = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsArchived = false
            };

            await groupREpository.CreateAsync(group);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<StudentGroupNumberEntity>(group);
        }
    }

    public class CreateStudentGroupNumberCommandValidator : AbstractValidator<CreateStudentGroupNumberCommand>
    {
        public CreateStudentGroupNumberCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
