using System;

using Bau.Libraries.LibRoslynDocument.Models.Documents;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers
{
	/// <summary>
	///		Interface para las clases de grabación de documentación
	/// </summary>
	public interface IDocumentWriter
	{
		/// <summary>
		///		Graba la documentación en un archivo
		/// </summary>
		void Save(DocumentFileModel objDocument, MLIntermedialBuilder objMLBuilder, string strFileNameTemplate, string strPath);

		/// <summary>
		///		Graba la documentación a partir de los datos pasados como parámetros
		/// </summary>
		void Save(string strTitle, string strDescription, MLIntermedialBuilder objMLBuilder, string strFileNameTemplate, string strPath);
	}
}
