using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class LoginResponseDto
    { 
    public required string Email { get; set;}
    public required string Token { get; set; }
    }
}