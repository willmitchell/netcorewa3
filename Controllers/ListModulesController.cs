using System.Text;
using CliWrap;
using Microsoft.AspNetCore.Mvc;

namespace netcorewa3.Controllers;

[ApiController]
[Route("[controller]")]
public class ListModulesController : ControllerBase
{
    private readonly ILogger<ListModulesController> _logger;

    public ListModulesController(ILogger<ListModulesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "ListPackages")]
    public async Task<IEnumerable<string>> Get()
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        _logger.Log(LogLevel.Information,"Listing all modules");
        
        var unused = await Cli.Wrap("c:\\windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe")
            .WithArguments("-Command Get-Module -ListAvailable")
            .WithWorkingDirectory("c:\\")
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        return stdOut.Split(Environment.NewLine);
    }
}