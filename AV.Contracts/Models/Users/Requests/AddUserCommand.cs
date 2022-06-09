using MediatR;

namespace AV.Contracts.Models.Users.Requests
{
    public class AddUserCommand:IRequest<UserModel>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}
