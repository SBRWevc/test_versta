namespace test_versta.ViewModels;

using System.ComponentModel.DataAnnotations;

public class OrderViewModel
{
    public int OrderId { get; set; }
    
    [Required(ErrorMessage = "Укажите город отправителя")]
    [Display(Name = "Город отправителя")]
    public string SenderCity { get; set; } = null!;

    [Required(ErrorMessage = "Укажите адрес отправителя")]
    [Display(Name = "Адрес отправителя")]
    public string SenderAddress { get; set; } = null!;

    [Required(ErrorMessage = "Укажите город получателя")]
    [Display(Name = "Город получателя")]
    public string RecipientCity { get; set; } = null!;

    [Required(ErrorMessage = "Укажите адрес получателя")]
    [Display(Name = "Адрес получателя")]
    public string RecipientAddress { get; set; } = null!;

    [Required(ErrorMessage = "Укажите вес груза")]
    [Display(Name = "Вес груза")]
    public decimal Weight { get; set; }

    [Required(ErrorMessage = "Укажите дату забора груза")]
    [Display(Name = "Дата забора груза")]
    [DataType(DataType.Date)]
    public DateTime PickupDate { get; set; }
    
    public string? ClientFullName { get; set;}
    public string? ClientEmail { get; set;}
}