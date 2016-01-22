using System;
using System.IO;

namespace Bau.Libraries.LibHelper.Files 
{
	/// <summary>
	///		Clase para tratamiento de nombres de archivo
	/// </summary>
	public class FileUri 
	{
		public FileUri(string strPath, string strFileName) : this(Path.Combine(strPath, strFileName)) {}

		public FileUri(string strFileName)
		{ FullFileName = strFileName;
		}

		/// <summary>
		///		Nombre completo del archivo
		/// </summary>
		public string FullFileName { get; set; }
	}
}
