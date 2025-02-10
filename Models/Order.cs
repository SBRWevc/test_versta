using System;
using System.ComponentModel.DataAnnotations;
using test_versta.Services;

namespace test_versta.Models
{
    /// <summary>
    /// Представляет заказ на доставку.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// Город отправителя.
        /// </summary>
        [Required(ErrorMessage = "Укажите город отправителя")]
        [Display(Name = "Город отправителя")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Город отправителя должен содержать от 2 до 100 символов")]
        public string SenderCity { get; set; }

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        [Required(ErrorMessage = "Укажите адрес отправителя")]
        [Display(Name = "Адрес отправителя")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Адрес отправителя должен содержать от 5 до 200 символов")]
        public string SenderAddress { get; set; }

        /// <summary>
        /// Город получателя.
        /// </summary>
        [Required(ErrorMessage = "Укажите город получателя")]
        [Display(Name = "Город получателя")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Город получателя должен содержать от 2 до 100 символов")]
        public string RecipientCity { get; set; }

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        [Required(ErrorMessage = "Укажите адрес получателя")]
        [Display(Name = "Адрес получателя")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Адрес получателя должен содержать от 5 до 200 символов")]
        public string RecipientAddress { get; set; }

        /// <summary>
        /// Вес груза (в кг).
        /// </summary>
        [Required(ErrorMessage = "Укажите вес груза")]
        [Display(Name = "Вес груза")]
        [Range(0.1, 1000, ErrorMessage = "Вес груза должен быть в пределах от 0.1 до 1000 кг")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        [Required(ErrorMessage = "Укажите дату забора груза")]
        [Display(Name = "Дата забора груза")]
        [DataType(DataType.Date)]
        [FutureOrToday(ErrorMessage = "Дата забора груза должна быть сегодняшней или в будущем")]
        public DateTime PickupDate { get; set; }

        /// <summary>
        /// Идентификатор клиента, оформившего заказ.
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// Клиент, оформивший заказ.
        /// </summary>
        public ApplicationUser? Client { get; set; }
    }
}
