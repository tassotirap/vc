namespace Verizon.Connect.QueryService.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static void BindViewModel<T>(this ControllerBase controller, T model)
        {
            if (model == null)
            {
                return;
            }

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, context, results, true))
            {
                controller.ModelState.Clear();
                foreach (var result in results)
                {
                    var key = result.MemberNames.FirstOrDefault() ?? string.Empty;
                    controller.ModelState.AddModelError(key, result.ErrorMessage);
                }
            }
        }
    }
}