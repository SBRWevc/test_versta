using System;
using System.ComponentModel.DataAnnotations;

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
        public string SenderCity { get; set; }

        /// <summary>
        /// Адрес отправителя.
        /// </summary>
        [Required(ErrorMessage = "Укажите адрес отправителя")]
        [Display(Name = "Адрес отправителя")]
        public string SenderAddress { get; set; }

        /// <summary>
        /// Город получателя.
        /// </summary>
        [Required(ErrorMessage = "Укажите город получателя")]
        [Display(Name = "Город получателя")]
        public string RecipientCity { get; set; }

        /// <summary>
        /// Адрес получателя.
        /// </summary>
        [Required(ErrorMessage = "Укажите адрес получателя")]
        [Display(Name = "Адрес получателя")]
        public string RecipientAddress { get; set; }

        /// <summary>
        /// Вес груза (в кг).
        /// </summary>
        [Required(ErrorMessage = "Укажите вес груза")]
        [Display(Name = "Вес груза")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Дата забора груза.
        /// </summary>
        [Required(ErrorMessage = "Укажите дату забора груза")]
        [Display(Name = "Дата забора груза")]
        [DataType(DataType.Date)]
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
