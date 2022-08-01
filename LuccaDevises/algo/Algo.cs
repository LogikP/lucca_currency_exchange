using System.Text.RegularExpressions;

namespace LuccaDevises.algo
{
    public class Algo
    {
        private readonly string _toConvert;
        private int _conversionsArrayLength;
        private readonly string[] _conversionsArray;

        private readonly List<Tuple<string, string, string>> _edges = new List<Tuple<string, string, string>>();

        private readonly List<string> _nodes = new List<string>();

        private readonly string[] _toConvertArray;

        private enum ToReverse
        {
            NothingToDo,
            Reverse,
            NotFound
        }


        public Algo(string toConvert, int conversionsArrayLength, string[] array)
        {
            this._conversionsArray = array;
            this._toConvert = toConvert;
            this._conversionsArrayLength = conversionsArrayLength;
            this._toConvertArray = SubStr(this._toConvert);
            this.InitGraph();
        }


        public void Exchange()
        {
            Graph<string> graph = new Graph<string>(_nodes, _edges);

            Console.WriteLine(MakeExchange(GetShortestPath(graph, this._toConvertArray[0])(this._toConvertArray[2])
                .ToList<string>()));
        }

        private void InitGraph()
        {
            foreach (var line in this._conversionsArray)
            {
                string[] values = SubStr(line);

                if (values.Length != 3)
                {
                    throw new Exception("Bad line format !");
                }

                Tuple<string, string, string> newTuple =
                    new Tuple<string, string, string>(values[0], values[1], values[2]);
                if (!this._edges.Contains(newTuple))
                    this._edges.Add(newTuple);
                if (!this._nodes.Contains(newTuple.Item1))
                    this._nodes.Add(newTuple.Item1);
                if (!this._nodes.Contains(newTuple.Item2))
                    this._nodes.Add(newTuple.Item2);
            }
        }

        private int MakeExchange(List<string> path)
        {
            float.TryParse(this._toConvertArray[1], out var valueToConvert);

            for (var i = 0; i < path.Count - 1; i++)
            {
                foreach (var values in this._edges)
                {
                    if (IsReverse(values, path[i], path[i + 1]) == ToReverse.NothingToDo)
                    {
                        valueToConvert = CalculConversion(valueToConvert, values.Item3, false);
                    }
                    else if (IsReverse(values, path[i], path[i + 1]) == ToReverse.Reverse)
                    {
                        valueToConvert = CalculConversion(valueToConvert, values.Item3, true);
                    }
                }
            }

            return (int) Math.Round(valueToConvert);
        }

        #region Utilities

        private static ToReverse IsReverse(Tuple<string, string, string> values, string elem, string next)
        {
            if (values.Item1 == elem && values.Item2 == next)
            {
                return ToReverse.NothingToDo;
            }
            else if (values.Item2 == elem && values.Item1 == next)
            {
                return ToReverse.Reverse;
            }

            return ToReverse.NotFound;
        }

        private static float CalculConversion(float valueToConvert, string multiplicator, bool reverse)
        {
            float.TryParse(multiplicator, out var floatMultiplicator);
            if (reverse)
            {
                floatMultiplicator = 1 / floatMultiplicator;
                floatMultiplicator = (float) Math.Round(floatMultiplicator, 4);
            }

            return (float) Math.Round(valueToConvert *= floatMultiplicator, 4);
        }

        private static string[] SubStr(string line)
        {
            return line.Split(";");
        }

        private static Func<T, IEnumerable<T>> GetShortestPath<T>(Graph<T> graph, T start) where T : notnull
        {
            Dictionary<T, T> previous = new Dictionary<T, T>();
            Queue<T> queue = new Queue<T>();

            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                T tmp = queue.Dequeue();
                foreach (T neighbor in graph.AdjacencyList[tmp])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;
                    previous[neighbor] = tmp;
                    queue.Enqueue(neighbor);
                }
            }

            IEnumerable<T> ShortestPath(T v)
            {
                List<T> path = new List<T> { };
                T current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                }

                path.Add(start);
                path.Reverse();
                return path;
            }

            return ShortestPath;
        }

        #endregion
    }
}