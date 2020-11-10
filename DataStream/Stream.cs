using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DataStream
{
    public class Stream
    {
        private readonly string _path;
        private readonly Stack<string> _buffer;
        private bool _lock;
        private bool _lock2;
        private readonly int _tick;

        /// <summary>
        /// Буфер потока. При обращении очищается. Обращение потокобезопасно переменная _lock указывает на обращение к свойству Buffer,
        /// _lock2 указывает завершена ли запись в буффер и чтение из файла.
        /// </summary>
        public IEnumerable<string> Buffer
        {
            get
            {
                _lock = true;

                while (_lock2)
                {
                    // ждет пока чтение из файла и запись в буфер закончится.
                }

                var clonedBuffer = _buffer.ToList();
                _buffer.Clear();

                _lock = false;
                return clonedBuffer;
            }
        }

        public string Id
        {
            get;
            set;
        }

        public Stream(string name, string path)
        {
            _path = path;
            _buffer = new Stack<string>();
            _lock = false;
            _lock2 = false;
            _tick = 1000;
            Id = name;
        }

        public Stream(string id, string path, int tick)
        {
            _path = path;
            _tick = tick;
            Id = id;
        }

        /// <summary>
        /// Читает из файла и записывает в буфер.
        /// </summary>
        public void ReadStream()
        {
            var trThread = new Thread(() =>
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    while (true)
                    {
                        if (!_lock)
                        {
                            _lock2 = true;
                            var line = sr.ReadLine();

                            if (line == null)
                            {
                                break;
                            }

                            _buffer.Push(line);
                            _lock2 = false;

                            Thread.Sleep(_tick);
                        }
                    }
                }
            });

            trThread.Start();
        }
    }
}
