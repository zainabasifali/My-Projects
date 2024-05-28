using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
namespace MyProject.Models;

public partial class Register
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter your Name.")]

    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your Date of birth.")]

    public DateOnly? Dob { get; set; }

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Please enter your password.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least 6 characters long.")]
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
