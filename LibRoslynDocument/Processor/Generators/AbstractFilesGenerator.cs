using System;

using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynDocument.Models.Groups;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Processor.Generators
{
	/// <summary>
	///		Clase base para los generadores de archivos
	/// </summary>
	internal abstract class AbstractFilesGenerator
	{
		internal AbstractFilesGenerator(DocumentationParameters objParameters, string strUrlBase)
		{ Parameters = objParameters;
			MLBuilderHelper = new Writers.MLBuilderHelper(MLBuilder, strUrlBase);
		}

		/// <summary>
		///		Comprueba si se debe generar un archivo para un espacio de nombres (es decir, si alguna de las estructuras
		///	de ese espacio de nombres necesita un archivo de documentación)
		/// </summary>
		internal bool MustGenerateFile(NameSpaceGroupModel objGroup)
		{	// Comprueba si se debe generar documentación para alguno de los elementos del espacio de nombres
				foreach (LanguageStructModel objStruct in objGroup.NameSpace.Items)
					if (MustGenerateFile(objStruct))
						return true;
			// Si se ha llegado hasta aquí es porque no se debería generar
				return false;
		}

		/// <summary>
		///		Comprueba si se debe generar un archivo de una estructura por los parámetros seleccionados por el usuario
		/// </summary>
		internal bool MustGenerateFile(LanguageStructModel objItem)
		{ // Comprueba si se debe generar
				if (objItem.IDType == LanguageStructModel.StructType.NameSpace)
					return true;
				else if (MustGenerateDocumentation(objItem))
					{ LanguageStructModel.StructType[] arrTypes = GetTypesDefined();

							// Comprueba si se debe generar un documento para este tipo
								foreach (LanguageStructModel.StructType intType in arrTypes)
									if (objItem.IDType == intType)
										return true;
					}
			// Si ha llegado hasta aquí es porque no se debe generar
				return false;
		}

		/// <summary>
		///		Comprueba si se debe generar el documento de una estructura a partir de los parámetros seleccionados
		/// </summary>
		private bool MustGenerateDocumentation(LanguageStructModel objItem)
		{ switch (objItem.Modifier)
				{	case LanguageStructModel.ModifierType.Internal:
						return Parameters.ShowInternal;
					case LanguageStructModel.ModifierType.Private:
						return Parameters.ShowPrivate;
					case LanguageStructModel.ModifierType.Protected:
						return Parameters.ShowProtected;
					case LanguageStructModel.ModifierType.ProtectedAndInternal:
						return Parameters.ShowProtected && Parameters.ShowInternal;
					case LanguageStructModel.ModifierType.ProtectedOrInternal:
						return Parameters.ShowProtected || Parameters.ShowInternal;
					case LanguageStructModel.ModifierType.Public:
						return Parameters.ShowPublic;
					default:
						return true;
				}
		}

		/// <summary>
		///		Obtiene los elementos de determinada estructura que se deben documentar
		/// </summary>
		protected LanguageStructModelCollection SelectItemsForGeneration(DocumentFileModel objDocument, LanguageStructModel.StructType intIDType)
		{ LanguageStructModelCollection objColStructs = new LanguageStructModelCollection();

				// Obtiene las estructuras
					foreach (LanguageStructModel objStruct in objDocument.LanguageStruct.Items)
						if (objStruct.IDType == intIDType && MustGenerateDocumentation(objStruct))
							objColStructs.Add(objStruct);
				// Devuelve la colección
					return objColStructs;
		}

		/// <summary>
		///		Obtiene los tipos de estructuras para los que se deben generar documentos
		/// </summary>
		protected abstract LanguageStructModel.StructType [] GetTypesDefined();

		/// <summary>
		///		Obtiene el ML del elemento
		/// </summary>
		internal Writers.MLIntermedialBuilder CreateDocument(DocumentFileModel objDocument)
		{ // Limpia el generador
				MLBuilder.Clear();
			// Genera el documento
				CreateInnerDocument(objDocument);
			// Devuelve el generador
				return MLBuilder;
		} 

		/// <summary>
		///		Obtiene el ML del elemento (interno)
		/// </summary>
		protected abstract void CreateInnerDocument(DocumentFileModel objDocument);

		/// <summary>
		///		Parámetros de documentación
		/// </summary>
		protected DocumentationParameters Parameters { get; private set; }

		/// <summary>
		///		Generador de archivos intermedios de documentación
		/// </summary>
		protected Writers.MLIntermedialBuilder MLBuilder { get; } = new Writers.MLIntermedialBuilder();

		/// <summary>
		///		Clase de ayuda para la generación de archivos intermedios
		/// </summary>
		protected Writers.MLBuilderHelper MLBuilderHelper { get; }
	}
}
