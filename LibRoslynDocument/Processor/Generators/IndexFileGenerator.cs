using System;

using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibRoslynDocument.Models.Documents;

namespace Bau.Libraries.LibRoslynDocument.Processor.Generators
{
	/// <summary>
	///		Generador del archivo de índices
	/// </summary>
	internal class IndexFileGenerator
	{ // Variables privadas
			private Writers.MLIntermedialBuilder objMLBuilder = new Writers.MLIntermedialBuilder();
			private Writers.MLBuilderHelper objMLBuilderHelper;

		internal IndexFileGenerator(string strUrlBase)
		{ objMLBuilderHelper = new Writers.MLBuilderHelper(objMLBuilder, strUrlBase);
		}

		/// <summary>
		///		Crea el índice
		/// </summary>
		internal Writers.MLIntermedialBuilder CreateIndex(DocumentFileModelCollection objColDocuments)
		{	// Limpia el generador
				objMLBuilder.Clear();
			// Crea el índice
				CreateIndex(objMLBuilder.Root, objColDocuments);
			// Devuelve el generador
				return objMLBuilder;
		}

		/// <summary>
		///		Crea el índice de una colección de documentos
		/// </summary>
		private void CreateIndex(MLNode objMLParent, DocumentFileModelCollection objColDocuments)
		{ bool blnIsAddedUl = false;

				// Crea los elementos de la lista
					foreach (DocumentFileModel objDocument in objColDocuments)
						{ // Si es un espacio de nombres, añade el elemento de la lista
								if (objDocument.IsRealNameSpace)
									{ // Añade la etiqueta ul
											if (!blnIsAddedUl)
												{ // Añade la etiqueta
														objMLParent = objMLParent.Nodes.Add("ul");
													// Indica que se ha añadido
														blnIsAddedUl = true;
												}
										// Crea el elemento de la lista
											objMLParent.Nodes.Add("li").Nodes.Add(objMLBuilderHelper.GetLink(objDocument));
									}
							// En cualquier caso, crea los elementos hijo
								CreateIndex(objMLParent, objDocument.Childs);
						}
		}
	}
}
