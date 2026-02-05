using MediatR;
using Microsoft.EntityFrameworkCore;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<string>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ServiceResult<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
            {
                return ServiceResult<string>.Failure("Invalid email or password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return ServiceResult<string>.Success(token);
        }
    }
}
