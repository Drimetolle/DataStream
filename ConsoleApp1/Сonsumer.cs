using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Сonsumer
    {
        private readonly FileManager _manager;
        private int _size;

        public Сonsumer(int size)
        {
            _size = size;
            _manager = new FileManager();
        }

        public IEnumerable<IEnumerable<KeyValuePair<string, string>>> GetDataBulk()
        {
            var window = new List<KeyValuePair<string, string>>();

            foreach (var data in GetDataOfStreams())
            {
                if (window.Count < _size)
                {
                    window.Add(data);
                }
                else
                {
                    var result = window.ToList();

                    window.Clear();
                    window.Add(data);

                    yield return result;
                }
            }
        }
        private IEnumerable<KeyValuePair<string, string>> GetDataOfStreams()
        {
            var factory = new StreamFactory(_manager.FilesPaths);

            var ids = factory.StartStreams();

            while (true)
            {
                foreach (var id in ids)
                {
                    foreach (var data in factory.GetDataByStreamId(id))
                    {
                        yield return new KeyValuePair<string, string>(id, data);
                    }
                }
            }
        }
    }
}
