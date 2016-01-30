using System;

using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynDocument.Models.Groups;
using Bau.Libraries.LibRoslynDocument.Models.Templates;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;

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
		{ NameSpaceGroupModelCollection objColGroups = new Prepare.NameSpaceGroupGenerator().Generate(objProgram);
			DocumentFileModelCollection objColDocuments = new DocumentFileModelCollection();
			
				// Carga las plantillas
					Templates = new Repository.Templates.TemplateRepository().Load(DocumentationProcessor.Parameters.TemplateFileName);
				// Crea los documentos
					foreach (NameSpaceGroupModel objGroup in objColGroups)
						if (Templates.MustGenerateFile(objGroup, DocumentationProcessor.Parameters))
							{ DocumentFileModel objDocument = new DocumentFileModel(null, objGroup.NameSpace, 0);

									// Procesa la estructura del lenguaje
										if (objGroup.NameSpace != null)
											Generate(objGroup.NameSpace, objDocument);
										else
											objDocument.Name = objGroup.Name;
									// Añade el documento a la colección
										objColDocuments.Add(objDocument);
							}
				// Graba los documentos
					ProcessDocuments(objColDocuments);
				// Devuelve los documentos
					return objColDocuments;
		}

		/// <summary>
		///		Genera la documentación de una estructura
		/// </summary>
		private void Generate(LanguageStructModel objStruct, DocumentFileModel objParent)
		{ foreach (LanguageStructModel objItem in objStruct.Items)
				if (Templates.MustGenerateFile(objItem, DocumentationProcessor.Parameters))
					{ DocumentFileModel objDocument = new DocumentFileModel(objParent, objItem, objParent.Childs.SearchOrder(objItem));

							// Añade el documento a los hijos
								objParent.Childs.Add(objDocument);
							// Añade los documentos hijo
								Generate(objItem, objDocument);
					}
		}

		/// <summary>
		///		Genera la documentación
		/// </summary>
		private void ProcessDocuments(DocumentFileModelCollection objColDocuments)
		{ // Ordena los archivos por nombres
				objColDocuments.SortByName();
			// Genera el archivo de índice
				DocumentationProcessor.Writer.Save("Indice", "Indice de la documentación", 
																					 new Generators.IndexFileGenerator().CreateIndex(objColDocuments, UrlBaseDocuments),
																					 null, DocumentationProcessor.OutputPath);
			// Genera los archivos de contenido
				GenerateFilesContent(objColDocuments);
			// Transforma los hipervínculos
				objColDocuments.TransformSearchLinks(UrlBaseDocuments);
			// Graba los documentos
				SaveDocuments(objColDocuments);
		}

		/// <summary>
		///		Genera los archivos de contenido
		/// </summary>
		private void GenerateFilesContent(DocumentFileModelCollection objColDocuments)
		{	foreach (DocumentFileModel objDocument in objColDocuments)
				{ TemplateModel objTemplate = Templates.Search(objDocument.StructType);
						
						// Genera la documentación del archivo
							if (objTemplate == null)
								AddError(objDocument, "No se encuentra ninguna plantilla para este tipo de estructura");
							else
								try
									{ Generators.TemplateDocumentGenerator objGenerator = new Generators.TemplateDocumentGenerator(this, objTemplate, objDocument, UrlBaseDocuments);

											objGenerator.Process();
									}
								catch (Exception objException)
									{ AddError(objDocument, "Error al generar el documento. " + objException.Message);
									}
						// Genera los documentos hijo
							GenerateFilesContent(objDocument.Childs);
				}
		}

		/// <summary>
		///		Graba los documentos
		/// </summary>
		private void SaveDocuments(DocumentFileModelCollection objColDocuments)
		{ foreach (DocumentFileModel objDocument in objColDocuments)
				{ // Graba el documento
						DocumentationProcessor.Writer.Save(objDocument, objDocument.MLBuilder, 
																							 Templates.Search(objDocument.StructType)?.FullFileNameRootTemplate, 
																							 DocumentationProcessor.OutputPath);
					// Graba los documentos hijo
						SaveDocuments(objDocument.Childs);
				}
		}

		/// <summary>
		///		Añade un error
		/// </summary>
		private void AddError(DocumentFileModel objDocument, string strMessage)
		{ AddError("Error: " + strMessage + ". Estructura: " + objDocument.Name + " (" + objDocument.StructType + ")");
		}

		/// <summary>
		///		Añade un error
		/// </summary>
		private void AddError(string strError)
		{ DocumentationProcessor.Errors.Add(strError);
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

		/// <summary>
		///		Plantillas
		/// </summary>
		internal TemplateModelCollection Templates { get; private set; }
	}
}
