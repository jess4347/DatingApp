using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    //[MaxLength(100)]
    public required string username { get; set; }
    [Required]
    public required string password { get; set; }

}
