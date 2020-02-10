﻿using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    
    
   [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "The provided description should be different from the title")]
    public abstract class CourseForManipulationDto
    {

            [Required(ErrorMessage = "You should fill out a title")]
            [MaxLength(100, ErrorMessage = "The title should not have more than 100 characters.")]
            public string Title { get; set; }
            
            [MaxLength(1500, ErrorMessage = "The description should not have more than 1500 characters.")]
            public virtual string Description { get; set; }
        }

    }

