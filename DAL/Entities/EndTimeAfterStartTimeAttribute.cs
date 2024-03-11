using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class EndTimeAfterStartTimeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var model = (Exam)validationContext.ObjectInstance;

			if (model.StartTime >= model.EndTime)
			{
				return new ValidationResult("End time must be after start time.");
			}

			return ValidationResult.Success;
		}
	}
}
