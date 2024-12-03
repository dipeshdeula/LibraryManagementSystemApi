using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Domain.Models
{
    

    public class DateOnlyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            try
            {
                // Assuming value is in ISO 8601 format
                var date = DateOnly.Parse(value, CultureInfo.InvariantCulture);
                bindingContext.Result = ModelBindingResult.Success(date);
            }
            catch (FormatException)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Invalid date format: {value}");
            }

            return Task.CompletedTask;
        }
    }

}
