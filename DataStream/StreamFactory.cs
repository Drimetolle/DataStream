using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataStream
{
    public class StreamFactory
    {
        private readonly List<DataStream> _streams;

        public StreamFactory(IEnumerable<string> paths)
        {
            _streams = new List<DataStream>();

            foreach (var path in paths)
            {
                _streams.Add(new DataStream(Path.GetFileName(path).Replace(".txt", ""), path));
            }
        }

        public IEnumerable<string> StreamsId => _streams.Select(_ => _.Id);

        /// <summary>
        /// Запускает все потоки.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> StartStreams()
        {
            foreach (var stream in _streams)
            {
                stream.StartStream();
            }

            return StreamsId;
        }

        /// <summary>
        /// Возвращает данные из потока по идентификатору потока.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<string> GetDataByStreamId(string id)
        {
            return _streams.Find(_ => _.Id == id).Buffer;
        }
    }
}
