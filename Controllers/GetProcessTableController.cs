using System.Text;
using CliWrap;
using Microsoft.AspNetCore.Mvc;

namespace netcorewa3.Controllers;

[ApiController]
[Route("[controller]")]
public class PsController : ControllerBase
{
    private readonly ILogger<PsController> _logger;

    public PsController(ILogger<PsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetProcessTable")]
    public async Task<IEnumerable<string>> Get()
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        _logger.Log(LogLevel.Information,"Running PS");
        
        var unused = await Cli.Wrap("c:\\windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe")
            .WithArguments("ps")
            .WithWorkingDirectory("c:\\")
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        return stdOut.Split(Environment.NewLine);
    }
}