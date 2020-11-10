using System.Collections.Generic;
using System.Linq;

namespace DataStream
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

        /// <summary>
        /// Возвращает данные из потока пачкой заданного размера (_size).
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получает данные из всех потоков и возвращает первую полученную порцию данных.
        /// </summary>
        /// <returns></returns>
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
