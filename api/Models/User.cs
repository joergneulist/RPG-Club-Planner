using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? ExternalId { get; set; }
        public string? Provider { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string RoleName { get; set; } = "pending";
        public DateTimeOffset CreatedAt { get; set; }
    }
}
