using BenchmarkDotNet.Attributes;

namespace DataStructures.Benchmarks
{
    [RankColumn]
    [MemoryDiagnoser]
    public class LinqBenchmark
    {
        private int[] _defaultCycle;
        private int[] _linqWhereNoToArray;
        private int[] _linqWhereWithToArray;
        private int[] _linqSelectNoToArray;
        private int[] _linqSelectWithToArray;
        private int[] _linqFullNoToArray;
        private int[] _linqFullWithToArray;


        public LinqBenchmark()
        {
            _defaultCycle = new int[1000000];
            _linqWhereNoToArray = new int[1000000];
            _linqWhereWithToArray = new int[1000000];
            _linqSelectNoToArray = new int[1000000];
            _linqSelectWithToArray = new int[1000000];
            _linqFullNoToArray = new int[1000000];
            _linqFullWithToArray = new int[1000000];

            for (int i = 0; i < _defaultCycle.Length; i++)
            {
                _defaultCycle[i] = i;
                _linqWhereNoToArray[i] = i;
                _linqWhereWithToArray[i] = i;
                _linqSelectNoToArray[i] = i;
                _linqSelectWithToArray[i] = i;
                _linqFullNoToArray[i] = i;
                _linqFullWithToArray[i] = i;
            }
        }


        [Benchmark]
        public void CreateArray()
        {
            int[] nums = new int[1000000];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = i;
            }
        }

        [Benchmark]
        public void DefaultForCicle()
        {
            for (int i = 0; i < _defaultCycle.Length; i++)
            {
                if (_defaultCycle[i] > 200)
                {
                    _defaultCycle[i] = 2 * i;
                }
            }
        }

        [Benchmark]
        public void LinqWhereToArray()
        {
            _linqWhereWithToArray.Where(x => x > 200).ToArray();
        }

        [Benchmark]
        public void LinqWhere()
        {
            _linqWhereNoToArray.Where(x => x > 200);
        }

        [Benchmark]
        public void LinqSelectToArray()
        {
            _linqSelectWithToArray.Select(x => x * 2).ToArray();
        }

        [Benchmark]
        public void LinqSelect()
        {
            _linqSelectNoToArray.Select(x => x * 2);
        }

        [Benchmark]
        public void LinqWhereSelectToArray()
        {
            _linqFullWithToArray.Where(x => x > 200).Select(x => x * 2).ToArray();
        }

        [Benchmark]
        public void LinqWhereSelectToList()
        {
            _linqFullWithToArray.Where(x => x > 200).Select(x => x * 2).ToList();
        }

        [Benchmark]
        public void LinqWhereSelect()
        {
            _linqFullNoToArray.Where(x => x > 200).Select(x => x * 2);
        }
    }
}
