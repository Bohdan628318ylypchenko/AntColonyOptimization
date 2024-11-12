using System;
using System.IO;

namespace ant
{
    public class FileRandomNumbersGenerator
    {
        private static double[] _rndSource;

        private int pos;

        public FileRandomNumbersGenerator(int pos)
        {
            this.pos = pos;
        }

        public static void InitializeRandomNumbersSource(string fileName)
        {
            using (var br1 = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                ulong ucount = br1.ReadUInt64();
                if (ucount > int.MaxValue)
                {
                    throw new Exception(
                        $"Error: files with count more than {int.MaxValue} are not supported.\n"
                    );
                }

                int count = (int)ucount;

                using (var br2 = new BinaryReader(new MemoryStream(br1.ReadBytes(count * sizeof(double)))))
                {
                    _rndSource = new double[count];
                    for (var i = 0; i < count; i++)
                    {
                        _rndSource[i] = br2.ReadDouble();
                    }
                }
            }
        }

        public static int GetRandomNumbersCount()
        {
            return _rndSource.Length;
        }

        public double NextUniformDouble()
        {
            double result = _rndSource[pos];
            pos = (pos + 1) % _rndSource.Length;
            return result;
        }
    }
}
