using System.ComponentModel.DataAnnotations;

namespace WasmDemo.Shared
{
    public class BirthdateAttribute : ValidationAttribute
    {
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public override bool IsValid(object? value)
        {
            var inputDate = (DateTime)value;

            if (inputDate >= DateTime.Now.AddYears(-MaxAge) && inputDate <= DateTime.Now.AddYears(-MinAge))
                return true;

            return false;
        }
    }
}
