using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecureAppQa.Models;

public partial class Ticket
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string AspNetUserId { get; set; } = null!;
    [MinLength(10, ErrorMessage = "Subject must be 10 - 150 characters")]
    [MaxLength(150, ErrorMessage = "Subject must be 10 - 150 characters")]

    public string Subject { get; set; } = null!;
    [MinLength(10, ErrorMessage = "Description must be 10 - 350 characters")]
    [MaxLength(350, ErrorMessage = "Description must be 10 - 350 characters")]

    public string Description { get; set; } = null!;

    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;

    public bool IsActive { get; set; } = true;

    public virtual AspNetUser? AspNetUser { get; set; }
}
