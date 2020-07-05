using System;

namespace ConsumerService.Domain
{
    public class Message
    {
        /// <summary>
        /// ИД сообщения
        /// </summary>
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
        public DateTime DeliveryTimestamp => DateTime.Now;
    }
}
