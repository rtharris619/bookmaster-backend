using Bookmaster.Common.Domain;

namespace Bookmaster.Common.Features.Messaging;

public interface ICommand : ICommand<Result>;

public interface ICommand<TResponse> : IBaseCommand;

public interface IBaseCommand;
