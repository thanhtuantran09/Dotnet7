﻿    namespace WebApplication2.Models
    {
        public class Product
        {
       
                public int id { get; set; }
                public string? code { get; set; }
                public string? name { get; set; }
          
                public string? brand { get; set; }
                public string? type { get; set; }
                public string? description { get; set; }
                public DateTimeOffset created_at { get; set; }
                public DateTimeOffset updated_at { get; set; }

            // Foreign key
            public int CategoryId { get; set; }

            // Navigation property
            public Category? Category { get; set; }
        }
    }
