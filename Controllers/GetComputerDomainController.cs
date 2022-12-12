using System.Text;
using CliWrap;
using Microsoft.AspNetCore.Mvc;

namespace netcorewa3.Controllers;
[ApiController]
[Route("[controller]")]
public class GetCurrentDomainController : Controller
{
    private readonly ILogger<GetCurrentDomainController> _logger;

    public GetCurrentDomainController(ILogger<GetCurrentDomainController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCurrentDomain")]
    public async Task<IEnumerable<string>> Get()
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        _logger.Log(LogLevel.Information, "Get Current Domain");

        // get the current computer domain details and dump info to the stdOutBuffer
        await Cli.Wrap("powershell.exe")
            .WithArguments("Get-ADDomain")
            .WithValidation(CommandResultValidation.None)
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
            .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        return stdOut.Split(Environment.NewLine);
    }
}