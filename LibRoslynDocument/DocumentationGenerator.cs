using System;

using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynDocument.Processor.Writers;

namespace Bau.Libraries.LibRoslynDocument
{
	/// <summary>
	///		Generador de documentación
	/// </summary>
	public class DocumentationGenerator
	{ 
		public DocumentationGenerator(DocumentationParameters objParameters, string strOutputPath)
		{ Parameters = objParameters;
			OutputPath = strOutputPath;
		}

		/// <summary>
		///		Genera la documentación a partir de un archivo de proyecto o solución
		/// </summary>
		public DocumentFileModelCollection Process(string strProjectFileName)
		{ return Process(strProjectFileName, GetDocumentWriter());
		}

		/// <summary>
		///		Obtiene el interface para la generación de la documentación final
		/// </summary>
		private IDocumentWriter GetDocumentWriter()
		{ switch (Parameters.IDType)
				{	case DocumentationParameters.DocumentationType.Nhtml:
						return new Processor.Writers.NHaml.NHamlWriter();
					case DocumentationParameters.DocumentationType.Html:
						return new Processor.Writers.Html.HtmlWriter();
					case DocumentationParameters.DocumentationType.Xml:
						return new Processor.Writers.Xml.XmlWriter();
					default:
						throw new NotImplementedException("No se reconoce el tipo de documentación: " + Parameters.IDType.ToString());
				}
		}

		/// <summary>
		///		Genera la documentación a partir de un archivo de proyecto o solución
		/// </summary>
		public DocumentFileModelCollection Process(string strProjectFileName, IDocumentWriter objWriter)
		{ ProgramModel objProgram = new LibRoslynManager.ProgramParser().ParseSolution(strProjectFileName);

				// Guarda el generador
					Writer = objWriter;
				// Genera la documentación
					return new Processor.ProgramDocumentationGenerator(this).Process(objProgram);
		}

		/// <summary>
		///		Parámetros de documentación
		/// </summary>
		public DocumentationParameters Parameters { get; }

		/// <summary>
		///		Directorio de salida
		/// </summary>
		public string OutputPath { get; }

		/// <summary>
		///		Generador de la documentación
		/// </summary>
		internal IDocumentWriter Writer { get; private set; }
	}
}
