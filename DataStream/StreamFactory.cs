using System.Collections.Generic;
using System.Linq;

namespace DataStream
{
    public class StreamFactory
    {
        private readonly List<Stream> _streams;

        public StreamFactory(IEnumerable<string> paths)
        {
            _streams = new List<Stream>();
            var i = 0;

            foreach (var path in paths)
            {
                _streams.Add(new Stream(i.ToString(), path));
                i++;
            }
        }

        public IEnumerable<string> StreamsId => _streams.Select(_ => _.Id);

        public IEnumerable<string> StartStreams()
        {
            foreach (var stream in _streams)
            {
                stream.ReadStream();
            }

            return StreamsId;
        }

        public IEnumerable<string> GetDataByStreamId(string id)
        {
            return _streams.Find(_ => _.Id == id).Buffer;
        }
    }
}
