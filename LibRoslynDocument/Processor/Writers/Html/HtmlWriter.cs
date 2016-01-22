using System;

using Bau.Libraries.LibRoslynDocument.Models.Documents;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers.Html
{
	/// <summary>
	///		Generador de un archivo HTML para documentación
	/// </summary>
	public class HtmlWriter : IDocumentWriter
	{ // Variables privadas
			private HtmlConversor objConversor = new HtmlConversor();

		/// <summary>
		///		Graba la documentación Html en un archivo
		/// </summary>
		public void Save(DocumentFileModel objDocument, MLIntermedialBuilder objMLBuilder, string strPath)
		{ Save(objDocument.Name, ConvertMLNode(objDocument.GetPathLocal(), objDocument.Name, objDocument.Name, objMLBuilder),
					 System.IO.Path.Combine(strPath, objDocument.GetPathLocal()));
		}

		/// <summary>
		///		Graba la documentación Html a partir de los datos pasados como parámetros
		/// </summary>
		public void Save(string strTitle, string strDescription, MLIntermedialBuilder objMLBuilder, string strPath)
		{	Save(strTitle, ConvertMLNode("", strTitle, strDescription, objMLBuilder), strPath);
		}

		/// <summary>
		///		Convierte una estructura XML en HTML
		/// </summary>
		private string ConvertMLNode(string strRootPath, string strTitle, string strDescription, MLIntermedialBuilder objMLBuilder)
		{ return objConversor.Convert(strRootPath, strTitle, strDescription, objMLBuilder);
		}

		/// <summary>
		///		Graba un archivo HTML
		/// </summary>
		private void Save(string strName, string strText, string strPath)
		{	string strFileName = System.IO.Path.Combine(strPath, objConversor.GetFileName(strName));

				// Crea el directorio
					LibHelper.Files.HelperFiles.MakePath(strPath);
				// Graba el archivo de documentación
					LibHelper.Files.HelperFiles.SaveTextFile(strFileName, strText);
		}
	}
}
