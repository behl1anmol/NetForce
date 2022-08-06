using CliWrap.Buffered;

namespace NetForce.Core.Contracts.Services;
public interface ICommandService
{
    BufferedCommandResult CliCommand
    {
        get; set;
    }
    Task<string> ExecuteCommandAsync(string comm, string dir, params string[] args);
}
