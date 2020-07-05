using System;
using System.Linq;

namespace ProducerService.Domain
{
    public class Message
    {
        private static readonly Random random = new Random();
        private const int TextLength = 6;

        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime PublishTimestamp { get; private set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Хэш базы.
        /// </summary>
        public string Hash { get; private set; }

        public Message()
        {
            PublishTimestamp = DateTime.Now;
            Text = RandomString(TextLength);           
            Hash = GetHashCode().ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Message);
        }

        public bool Equals(Message other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Hash == other.Hash;
        }

        public override int GetHashCode()
        {
            int hash = PublishTimestamp.GetHashCode();
            hash = hash * 17 + Text.GetHashCode();
            return hash;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(c => c[random.Next(c.Length)]).ToArray());
        }
    }
}
