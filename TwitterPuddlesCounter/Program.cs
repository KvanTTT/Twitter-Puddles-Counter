using System;
using System.IO;
using System.IO.Compression;

namespace TwitterPuddlesCounter
{
	class Program
	{
		static void Main(string[] args)
		{
			int n = 8; // Max count of walls
			int h = 8; // Max wall height

			byte[] ansewers = Decompress(File.ReadAllBytes(@"..\..\..\answers.bin"));

			Console.WriteLine(1 == GetVolume(new byte[] { 1, 0, 1 }, ansewers, n, h));
			Console.WriteLine(5 == GetVolume(new byte[] { 5, 0, 5 }, ansewers, n, h));
			Console.WriteLine(1 == GetVolume(new byte[] { 0, 1, 0, 1, 0 }, ansewers, n, h));
			Console.WriteLine(1 == GetVolume(new byte[] { 1, 0, 1, 0 }, ansewers, n, h));
			Console.WriteLine(3 == GetVolume(new byte[] { 1, 0, 1, 2, 0, 2 }, ansewers, n, h));
			Console.WriteLine(1 == GetVolume(new byte[] { 5, 1, 0, 1 }, ansewers, n, h));

			Console.ReadLine();
		}

		static int GetVolume(byte[] levels, byte[] answers, int n, int h)
		{
			int ind = 0;
			int a = 1;
			for (int i = 0; i < Math.Min(n, levels.Length); i++)
			{
				ind += levels[i] * a;
				a *= h;
			}

			return answers[ind];
		}

		static byte[] Decompress(byte[] gzip)
		{
			using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
			{
				const int size = 4096;
				byte[] buffer = new byte[size];
				using (MemoryStream memory = new MemoryStream())
				{
					int count = 0;
					do
					{
						count = stream.Read(buffer, 0, size);
						if (count > 0)
						{
							memory.Write(buffer, 0, count);
						}
					}
					while (count > 0);
					return memory.ToArray();
				}
			}
		}
	}
}
