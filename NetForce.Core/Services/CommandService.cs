using CliWrap;
using CliWrap.Buffered;
using NetForce.Core.Contracts.Services;

namespace NetForce.Core.Services;
public class CommandService : ICommandService
{
    BufferedCommandResult _cliCommand;
    BufferedCommandResult ICommandService.CliCommand
    {
        get => _cliCommand;
        set => _cliCommand = value;
    }

    public CommandService()
    {
    }
    async Task<string> ICommandService.ExecuteCommandAsync(string comm, string dir, params string[] args)
    {
        try
        {
            if (dir == null)
            {
                dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            
            _cliCommand = await Cli.Wrap(comm)
                .WithWorkingDirectory(dir)
                .WithArguments(args)
                .ExecuteBufferedAsync();
        }
        catch
        {
            throw;
        }
        return _cliCommand.StandardOutput.ToString();
    }
}
