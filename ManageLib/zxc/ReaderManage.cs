using System;
using System.Collections.Generic;
using System.Linq;

namespace Management
{
    public class ReaderManage
    {
        private readonly List<Reader> _readers;

        public ReaderManage()
        {
            _readers = new List<Reader>();
        }

        public void AddReader(Reader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "Читатель не может быть null.");

            if (_readers.Any(r => r.Id == reader.Id))
                throw new InvalidOperationException($"Читатель с ID {reader.Id} уже существует.");

            _readers.Add(reader);
        }

        public void RemoveReader(string readerId)
        {
            if (string.IsNullOrWhiteSpace(readerId))
                throw new ArgumentException("ID читателя не может быть пустым или null.", nameof(readerId));

            var reader = _readers.FirstOrDefault(r => r.Id == readerId);
            if (reader == null)
                throw new KeyNotFoundException($"Читатель с ID {readerId} не найден.");

            _readers.Remove(reader);
        }

        public Reader GetReader(string readerId)
        {
            if (string.IsNullOrWhiteSpace(readerId))
                throw new ArgumentException("ID читателя не может быть пустым или null.", nameof(readerId));

            var reader = _readers.FirstOrDefault(r => r.Id == readerId);
            if (reader == null)
                throw new KeyNotFoundException($"Читатель с ID {readerId} не найден.");

            return reader;
        }

        public IReadOnlyList<Reader> GetAllReaders()
        {
            return _readers.AsReadOnly();
        }
    }
}
