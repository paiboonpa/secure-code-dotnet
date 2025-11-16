using Microsoft.AspNetCore.Mvc;
using System;
using BCrypt.Net;

[Route("[controller]")]
[ApiController]
public class HashController : ControllerBase
{
    [HttpGet("bcrypt")]
    public IActionResult Hash([FromQuery] string? password)
    {
        string databasePassword = "password";
        string hashedDatabasePassword = BCrypt.Net.BCrypt.HashPassword(databasePassword);
        string result = $"Database password: {hashedDatabasePassword}<br>";

        if (string.IsNullOrEmpty(password))
        {
            result += "Please provide a 'password' query parameter.";
            return Content(result, "text/html");
        }

        string hashedInputPassword = BCrypt.Net.BCrypt.HashPassword(password);
        result += $"New Input password: {hashedInputPassword}";

        // DON'T COMPARE PASSWORD DIRECTLY WHEN USE BCRYPT!!
        if (hashedDatabasePassword == hashedInputPassword)
            result += "<br>Password Matched!!";
        else
            result += "<br>Password NOT MATCH!!";

        if (BCrypt.Net.BCrypt.Verify(password, hashedDatabasePassword))
            result += "<br>Password Matched!!";
        else
            result += "<br>Password NOT MATCH!!";

        return Content(result, "text/html");
    }

    [HttpGet("stupidhash")]
    public IActionResult StupidHash([FromQuery] string? password)
    {
        string databasePassword = "password";
        string StupidHashFunc(string pwd) => pwd + "123";
        string hashedDatabasePassword = StupidHashFunc(databasePassword);
        string result = $"Database password: {hashedDatabasePassword}<br>";

        if (string.IsNullOrEmpty(password))
        {
            result += "Please provide a 'password' query parameter.";
            return Content(result, "text/html");
        }

        string hashedInputPassword = StupidHashFunc(password);
        result += $"New Input password: {hashedInputPassword}";

        if (hashedDatabasePassword == hashedInputPassword)
            result += "<br>Password Matched!!";
        else
            result += "<br>Password NOT MATCH!!";

        return Content(result, "text/html");
    }
}
