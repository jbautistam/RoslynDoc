using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols;
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
		public DocumentFileModelCollection Process(ProgramModel objProgram, IDocumentWriter objWriter = null)
		{ // Obtiene el generador
				if (objWriter == null)
					objWriter = GetDocumentWriter();
			// Guarda el generador
				Writer = objWriter;
			// Copia el contenido del directorio de plantillas
				CopyTemplates();
			// Genera la documentación
				return new Processor.ProgramDocumentationGenerator(this).Process(objProgram);
		}

		/// <summary>
		///		Copia los archivos del directorio donde se encuentran las plantillas
		/// </summary>
		private void CopyTemplates()
		{ string strPathTemplate = Parameters.TemplateFileName;

				if (!strPathTemplate.IsEmpty())
					{ // Obtiene el directorio
							strPathTemplate = System.IO.Path.GetDirectoryName(strPathTemplate);
						// Si existe el directorio
							if (System.IO.Directory.Exists(strPathTemplate))
								{ string [] arrStrPath = System.IO.Directory.GetDirectories(strPathTemplate);
									string [] arrStrFiles = System.IO.Directory.GetFiles(strPathTemplate);

										// Copia los directorios
											foreach (string strPath in arrStrPath)
												LibHelper.Files.HelperFiles.CopyPath(strPath, System.IO.Path.Combine(OutputPath, System.IO.Path.GetFileName(strPath)));
										// Copia los archivos (excepto las plantillas)
											foreach (string strFile in arrStrFiles)
												if (!strFile.EndsWith(".tpt", StringComparison.CurrentCultureIgnoreCase))
													LibHelper.Files.HelperFiles.CopyFile(strFile, System.IO.Path.Combine(OutputPath, System.IO.Path.GetFileName(strFile)));
								}
					}
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
		///		Parámetros de documentación
		/// </summary>
		public DocumentationParameters Parameters { get; }

		/// <summary>
		///		Directorio de salida
		/// </summary>
		public string OutputPath { get; }

		/// <summary>
		///		Errores de la documentación
		/// </summary>
		public System.Collections.Generic.List<string> Errors { get; } = new System.Collections.Generic.List<string>();

		/// <summary>
		///		Generador de la documentación
		/// </summary>
		internal IDocumentWriter Writer { get; private set; }
	}
}
