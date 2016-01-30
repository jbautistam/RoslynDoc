using System;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Models.Templates
{
	/// <summary>
	///		Modelo para los datos de una plantilla
	/// </summary>
	internal class TemplateModel
	{
		internal TemplateModel(LanguageStructModel.StructType intIDStructType, string strPath, 
													 string strRelativeFileName, string strRootTemplate)
		{ IDStructType = intIDStructType;
			Path = strPath;
			RelativeFileName = strRelativeFileName;
			RootTemplate = strRootTemplate;
		}

		/// <summary>
		///		Tipo de estructura que se trata en la plantilla
		/// </summary>
		public LanguageStructModel.StructType IDStructType { get; }

		/// <summary>
		///		Directorio base del archivo
		/// </summary>
		internal string Path { get; }

		/// <summary>
		///		Nombre de archivo relativo a la raíz
		/// </summary>
		public string RelativeFileName { get; }

		/// <summary>
		///		Nombre completo del archivo
		/// </summary>
		public string FullFileName
		{ get { return System.IO.Path.Combine(Path, RelativeFileName); }
		}

		/// <summary>
		///		Nombre de la plantilla raíz
		/// </summary>
		public string RootTemplate { get; }

		/// <summary>
		///		Nombre completo de la plantilla raíz
		/// </summary>
		public string FullFileNameRootTemplate
		{ get { return System.IO.Path.Combine(Path, RootTemplate); }
		}
	}
}
