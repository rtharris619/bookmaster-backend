using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Features.Abstractions.Data;

namespace Bookmaster.Modules.Books.Features.People.UpdatePerson;

internal sealed class UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePersonCommand>
{
    public async Task<Result> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        Person? person = await personRepository.GetAsync(command.PersonId, cancellationToken);

        if (person is null)
        {
            return Result.Failure(PersonErrors.NotFound(command.PersonId.ToString()));
        }

        person.Update(command.FirstName, command.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
