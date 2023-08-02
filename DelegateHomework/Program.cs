using DelegateHomework;

internal class Program
{
	private static IEnumerable<FileInfo> _files = new List<FileInfo>();
	private static int _stopCount = 0;

	private static void Main(string[] args)
	{
		FileFinder finder = new FileFinder();
		finder.OnFileFound += OnFileFoundHandler;
		Console.WriteLine("Type directory path");
		string dir = Console.ReadLine();
		finder.FindFile(dir, ContainsSubstring, NeedToCancel);
		finder.OnFileFound -= OnFileFoundHandler;

		Console.BackgroundColor = ConsoleColor.Magenta;
		Console.WriteLine($"Max FileInfo is:");
		Console.ResetColor();
		Console.WriteLine(_files.GetMax(ConvertFileOnFileLength)?.Name);
	}

	/// <summary>
	/// Обработчик события нахождения файла
	/// </summary>
	/// <param name="args"></param>
	private static void OnFileFoundHandler(FileArgs args)
	{
		_stopCount++;
		Console.BackgroundColor = ConsoleColor.DarkYellow;
		Console.ForegroundColor = ConsoleColor.DarkRed;
		Console.WriteLine($"I found the file:");
		Console.ResetColor();
		Console.WriteLine(args.FileName);
		_files = _files.Append(args.FileInfo);
	}

	private static bool NeedToCancel()
	{
		if (_stopCount > 2)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("Cancelled!");
			Console.ResetColor();
		}
		return _stopCount > 2;
	}

	#region Функции преобразования файла

	/// <summary>
	/// На основании размера файла
	/// </summary>
	private static float ConvertFileOnFileLength(FileInfo value)
	{
		return value.Length;
	}

	/// <summary>
	/// На основании имени файла
	/// </summary>
	private static float ConvertFileOnNameLength(FileInfo value)
	{
		return value.FullName.Length;
	}

	#endregion

	#region Функции фильтрации файлов

	/// <summary>
	/// По возрасту файла
	/// </summary>
	private static bool IsTwoWeekOld(FileInfo file)
	{
		return file.CreationTime > DateTime.Now.AddDays(-14);
	}

	/// <summary>
	/// По подстроке в имени
	/// </summary>
	private static bool ContainsSubstring(FileInfo file)
	{
		return file.Name.Contains("at", StringComparison.InvariantCultureIgnoreCase);
	}

	#endregion
}