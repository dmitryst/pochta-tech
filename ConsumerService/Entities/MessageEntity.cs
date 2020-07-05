using System;
using System.ComponentModel.DataAnnotations;

namespace ConsumerService.Entities
{
    public class MessageEntity
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер сообщения
        /// </summary>
        public ulong Number { get; set; }

        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime PublishTimestamp { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Хэш базы.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Время доставки сообщения
        /// </summary>
        public DateTime DeliveryTimestamp { get; set; }
    }
}
