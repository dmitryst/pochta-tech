using System;
using System.ComponentModel.DataAnnotations;

namespace ProducerService.Entities
{
    public class MessageEntity
    {
        /// <summary>
        /// Идентификатор (номер) сообщения
        /// </summary>
        [Key]
        public int Id { get; set; }

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
    }
}
