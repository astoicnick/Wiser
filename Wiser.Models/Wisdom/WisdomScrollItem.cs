﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Author;

namespace Wiser.Models.Wisdom
{
    public class WisdomScrollItem
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Display(Name ="User")]
        public string UserName { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public AuthorScrollItem Author { get; set; }
        public int? Virtue { get; set; }
    }
}
