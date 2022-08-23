using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class RegisterUser : IHandler
{
    private readonly DataContext _context;

    public RegisterUser(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(string pid, string email)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if (string.IsNullOrEmpty(email))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|cz)$";
        if (Regex.IsMatch(email, regex, RegexOptions.IgnoreCase) == false)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid); //If this pid does not exist than add it to cosmos db
        if (user == null)
            _context.Add(new UserInfo
            {
                Pid = pid,
                ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg",
                Email = email
            });

        await _context.SaveChangesAsync();
    }
}
