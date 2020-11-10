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

        public IEnumerable<string> Buffer
        {
            get
            {
                _lock = true;

                while (_lock2)
                {
                    // await
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
