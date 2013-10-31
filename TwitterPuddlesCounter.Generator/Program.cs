using System;
using System.IO;
using System.IO.Compression;

namespace TwitterPuddlesCounter.Generator
{
	class Program
	{
		static void Main(string[] args)
		{
			int n = 8; // Max count of walls
			int h = 8; // Max wall height
			byte[] answers = GenerateAnswers(n, h);
			File.WriteAllBytes(@"..\..\..\answers.bin", Compress(answers));
		}

		static byte[] GenerateAnswers(int n, int h)
		{
			byte[] levels = new byte[n];
			int hn = (int)Math.Pow(h, n);
			byte[] result = new byte[hn];

			for (int i = 0; i < hn; i++)
			{
				result[i] = (byte)Area(levels, n);
				Increment(levels, n, h);
			}

			return result;
		}

		static int Area(byte[] x, int n)
		{
			int s = 0;
			for (int a = 0, b = n - 1; a < b; )
			{
				int ha = x[a], hb = x[b];
				if (ha < hb)
					while (x[++a] < ha)
						s += ha - x[a];
				else
					while (x[--b] < hb)
						s += hb - x[b];
			}
			return s;
		}

		static int Increment(byte[] arr, int n, int h)
		{
			int flag = 1;
			for (int j = 0; j < n; j++)
			{
				int level1 = arr[j] + flag;
				flag = level1 / h;
				arr[j] = (byte)(level1 % h);
			}
			return flag;
		}

		static byte[] Compress(byte[] raw)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
				{
					gzip.Write(raw, 0, raw.Length);
				}
				return memory.ToArray();
			}
		}
	}
}
