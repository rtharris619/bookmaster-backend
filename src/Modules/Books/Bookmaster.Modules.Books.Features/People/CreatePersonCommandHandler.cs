using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Features.Abstractions.Data;

namespace Bookmaster.Modules.Books.Features.People;

internal sealed class CreatePersonCommandHandler(
    IPersonRepository personRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePersonCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        bool person = await personRepository.ExistsAsync(command.PersonId, cancellationToken);

        if (person)
        {
            return Result.Failure<Guid>(PersonErrors.Duplicate(command.PersonId));
        }

        var newPerson = Person.Create(
            id: command.PersonId,
            email: command.Email,
            firstName: command.FirstName,
            lastName: command.LastName,
            identityId: command.IdentityId);

        personRepository.Insert(newPerson);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newPerson.Id);
    }
}
