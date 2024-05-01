﻿namespace API.DTOs
{
    public class StudentPostDto
    {
        public string? Photo { get; set; }
        public string? FirstName { get; set; }
        public string? FahterName { get; set; }
        public string? GrandFatherName { get; set; }
        public string? SurName { get; set; }
        public short? Gender { get; set; }
        public string? NationalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? BloodType { get; set; }
        public short? YearClassID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RegionId { get; set; }
        public string? Address { get; set; }
        public string? ParentName { get; set; }
        public string? ParentPhone { get; set; }
        public string? ParentEmail { get; set; }
    }
}
