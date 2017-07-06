﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Note.Model
{
    [ExcludeFromCodeCoverage]
    public class AddNoteRequest
    {
        public string UserFullName { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string AccountIdentifier { get; set; }
    }
}
