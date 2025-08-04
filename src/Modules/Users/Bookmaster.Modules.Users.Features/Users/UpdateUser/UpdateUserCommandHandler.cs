using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Users.Domain.Users;
using Bookmaster.Modules.Users.Features.Abstractions.Data;

namespace Bookmaster.Modules.Users.Features.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(command.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }

        user.Update(command.FirstName, command.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
