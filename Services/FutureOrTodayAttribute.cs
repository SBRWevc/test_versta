using System;
using System.ComponentModel.DataAnnotations;

namespace test_versta.Services
{
    /// <summary>
    /// Атрибут валидации, проверяющий, что дата является сегодняшней или будущей.
    /// </summary>
    public class FutureOrTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                // Сравниваем только дату (без времени)
                return date.Date >= DateTime.Today;
            }
            // Если значение не DateTime, считаем его невалидным
            return false;
        }
    }
}