using CleanArchitecture.Blazor.Infrastructure.Extensions;
namespace CleanArchitecture.Blazor.Infrastructure.Services.Authentication;

public class ProfileService 
{
    public  Func<UserModel,Task>? OnChange;
    public UserModel Profile { get; private set; } = default!;



    public async Task<UserModel> Set(Task<AuthenticationState> state)
    {
        var user = (await state).User;
        Profile =  new UserModel()
        {
            Avatar = user.GetProfilePictureDataUrl(),
            DisplayName = user.GetDisplayName(),
            Email = user.GetEmail(),
            PhoneNumber = user.GetPhoneNumber(),
            Site=user.GetSite(),
            Role = user.GetRoles().FirstOrDefault()
        };
        return Profile;
    }

    public Task Update(UserModel profile)
    {
        Profile = profile;
        OnChange?.Invoke(Profile);
        return Task.CompletedTask;
    }
}