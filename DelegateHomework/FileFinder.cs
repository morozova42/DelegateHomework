namespace DelegateHomework
{
	public class FileFinder
	{
		public delegate void FileFoundHandler(FileArgs args);
		public event FileFoundHandler? OnFileFound;

		/// <summary>
		/// Ищет файлы, соответствующие условию <paramref name="predicate"/>
		/// </summary>
		/// <exception cref="DirectoryNotFoundException"></exception>
		public void FindFile(string dir, Predicate<FileInfo> predicate, Func<bool>? cancel = null)
		{
			if (!Directory.Exists(dir))
				throw new DirectoryNotFoundException();

			DirectoryInfo directoryInfo = new DirectoryInfo(dir);
			var files = directoryInfo.GetFiles();
			for (int i = 0; cancel?.Invoke() == false && i < files.Length; i++)
			{
				if (predicate(files[i]))
				{
					OnFileFound?.Invoke(new FileArgs { FileName = files[i].Name, FileInfo = files[i] });
				}
			}
		}
	}

	public class FileArgs : EventArgs
	{
		public string FileName { get; set; }
		public FileInfo FileInfo { get; set; }
	}
}