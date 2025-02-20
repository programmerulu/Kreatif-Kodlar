﻿using System.ComponentModel.DataAnnotations;

namespace BlogProject.Entites
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
