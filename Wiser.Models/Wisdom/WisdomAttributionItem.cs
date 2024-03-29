﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Wisdom
{
    public class WisdomAttributionItem
    {
        [Required]
        public int WisdomId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        [Display(Name = "Created At")]

        public DateTime CreatedAt { get; set; }
        [UIHint("Upvoted")]
        public bool IsUpvoted { get; set; }
    }
}
