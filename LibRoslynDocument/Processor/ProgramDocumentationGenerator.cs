using System;

using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynDocument.Models.Groups;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Processor
{
	/// <summary>
	///		Generador de documentación de un programa
	/// </summary>
	internal class ProgramDocumentationGenerator
	{ 
		internal ProgramDocumentationGenerator(DocumentationGenerator objGenerator)
		{ DocumentationProcessor = objGenerator;
		}

		/// <summary>
		///		Procesa la generación de documentación de un programa
		/// </summary>
		internal DocumentFileModelCollection Process(ProgramModel objProgram)
		{	Generators.AbstractFilesGenerator objFilesGenerator = GetFilesGenerator();
			NameSpaceGroupModelCollection objColGroups = new Prepare.NameSpaceGroupGenerator().Generate(objProgram);
			DocumentFileModelCollection objColDocuments = new DocumentFileModelCollection();
				
				// Crea los documentos
					foreach (NameSpaceGroupModel objGroup in objColGroups)
						if (objFilesGenerator.MustGenerateFile(objGroup))
							{ DocumentFileModel objDocument = new DocumentFileModel(null, objGroup.NameSpace, 0);

									// Procesa la estructura del lenguaje
										if (objGroup.NameSpace != null)
											Generate(objFilesGenerator, objGroup.NameSpace, objDocument);
										else
											objDocument.Name = objGroup.Name;
									// Añade el documento a la colección
										objColDocuments.Add(objDocument);
							}
				// Graba los documentos
					SaveDocumentation(objFilesGenerator, objColDocuments);
				// Devuelve los documentos
					return objColDocuments;
		}

		/// <summary>
		///		Genera la documentación de una estructura
		/// </summary>
		private void Generate(Generators.AbstractFilesGenerator objFilesGenerator, LanguageStructModel objStruct, DocumentFileModel objParent)
		{ foreach (LanguageStructModel objItem in objStruct.Items)
				if (objFilesGenerator.MustGenerateFile(objItem))
					{ DocumentFileModel objDocument = new DocumentFileModel(objParent, objItem, objParent.Childs.SearchOrder(objItem));

							// Añade el documento a los hijos
								objParent.Childs.Add(objDocument);
							// Añade los documentos hijo
								Generate(objFilesGenerator, objItem, objDocument);
					}
		}

		/// <summary>
		///		Genera la documentación
		/// </summary>
		private void SaveDocumentation(Generators.AbstractFilesGenerator objFilesGenerator, DocumentFileModelCollection objColDocuments)
		{ // Ordena los archivos por nombres
				objColDocuments.SortByName();
			// Genera el archivo de índice
				DocumentationProcessor.Writer.Save("Indice", "Indice de la documentación", 
																					 new Generators.IndexFileGenerator(UrlBaseDocuments).CreateIndex(objColDocuments), 
																					 DocumentationProcessor.OutputPath);
			// Genera los archivos de contenido
				GenerateFilesContent(objColDocuments, objFilesGenerator);
		}

		/// <summary>
		///		Genera los archivos de contenido
		/// </summary>
		private void GenerateFilesContent(DocumentFileModelCollection objColDocuments, Generators.AbstractFilesGenerator objFilesGenerator)
		{	foreach (DocumentFileModel objDocument in objColDocuments)
				{ Writers.MLIntermedialBuilder objBuilder = objFilesGenerator.CreateDocument(objDocument);

						// Graba el documento
							DocumentationProcessor.Writer.Save(objDocument, objBuilder, DocumentationProcessor.OutputPath);
						// Graba los documentos hijo
							GenerateFilesContent(objDocument.Childs, objFilesGenerator);
				}
		}

		/// <summary>
		///		Obtiene el generador de archivos
		/// </summary>
		private Generators.AbstractFilesGenerator GetFilesGenerator()
		{ switch (DocumentationProcessor.Parameters.Mode)
				{	case DocumentationParameters.DocumentationMode.SimpleStructs:
						return new Generators.SimpleFilesGenerator(DocumentationProcessor.Parameters, UrlBaseDocuments);
					case DocumentationParameters.DocumentationMode.ComplexStructs:
						return new Generators.ComplexFilesGenerator(DocumentationProcessor.Parameters, UrlBaseDocuments);
					default:
						throw new NotImplementedException("No se reconoce ningún generador para " + DocumentationProcessor.Parameters.Mode);
				}
		}

		/// <summary>
		///		Directorio base para las URL de los documentos
		/// </summary>
		protected string UrlBaseDocuments
		{ get { return System.IO.Path.GetFileName(DocumentationProcessor.OutputPath); }
		}

		/// <summary>
		///		Procesador de la documentación
		/// </summary>
		internal DocumentationGenerator DocumentationProcessor { get; private set; }
	}
}
