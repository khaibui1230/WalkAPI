﻿namespace WalkAPI.Models.DTO
{
    public class WalksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }

        public RegionsDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}