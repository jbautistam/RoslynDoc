using System;
using System.Collections.Generic;
using System.Linq;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynDocument.Models.Groups;

namespace Bau.Libraries.LibRoslynDocument.Models.Templates
{
	/// <summary>
	///		Colección de <see cref="TemplateModel"/>
	/// </summary>
	internal class TemplateModelCollection : List<TemplateModel>
	{
		internal TemplateModelCollection(string strPath)
		{ Path = strPath;
		}

		/// <summary>
		///		Añade una plantilla a la colección
		/// </summary>
		internal void Add(LanguageStructModel.StructType intIDStructType, string strRelativeFileName, string strRootTemplate)
		{ Add(new TemplateModel(intIDStructType, Path, strRelativeFileName, strRootTemplate));
		}

		/// <summary>
		///		Busca una plantilla en la colección
		/// </summary>
		internal TemplateModel Search(LanguageStructModel.StructType intIDStructType)
		{ return this.FirstOrDefault(objTemplate => objTemplate.IDStructType == intIDStructType);
		}

		/// <summary>
		///		Comprueba si se debe generar un archivo para un espacio de nombres (es decir, si alguna de las estructuras
		///	de ese espacio de nombres necesita un archivo de documentación)
		/// </summary>
		internal bool MustGenerateFile(NameSpaceGroupModel objGroup, DocumentationParameters objParameters)
		{	// Comprueba si se debe generar documentación para alguno de los elementos del espacio de nombres
				foreach (LanguageStructModel objStruct in objGroup.NameSpace.Items)
					if (MustGenerateFile(objStruct, objParameters))
						return true;
			// Si se ha llegado hasta aquí es porque no se debería generar
				return false;
		}

		/// <summary>
		///		Comprueba si se debe generar un archivo de una estructura por los parámetros seleccionados por el usuario
		/// </summary>
		internal bool MustGenerateFile(LanguageStructModel objItem, DocumentationParameters objParameters)
		{ // Comprueba si se debe generar
				if (objItem.IDType == LanguageStructModel.StructType.NameSpace)
					return true;
				else if (MustGenerateDocumentation(objItem, objParameters))
					{ // Comprueba si hay alguna plantilla definida para este tipo en la colección
							foreach (TemplateModel objTemplate in this)
								if (objTemplate.IDStructType == objItem.IDType)
									return true;
					}
			// Si ha llegado hasta aquí es porque no se debe generar
				return false;
		}

		/// <summary>
		///		Comprueba si se debe generar el documento de una estructura a partir de los parámetros seleccionados
		/// </summary>
		internal bool MustGenerateDocumentation(LanguageStructModel objItem, DocumentationParameters objParameters)
		{ switch (objItem.Modifier)
				{	case LanguageStructModel.ModifierType.Internal:
						return objParameters.ShowInternal;
					case LanguageStructModel.ModifierType.Private:
						return objParameters.ShowPrivate;
					case LanguageStructModel.ModifierType.Protected:
						return objParameters.ShowProtected;
					case LanguageStructModel.ModifierType.ProtectedAndInternal:
						return objParameters.ShowProtected && objParameters.ShowInternal;
					case LanguageStructModel.ModifierType.ProtectedOrInternal:
						return objParameters.ShowProtected || objParameters.ShowInternal;
					case LanguageStructModel.ModifierType.Public:
						return objParameters.ShowPublic;
					default:
						return true;
				}
		}

		/// <summary>
		///		Directorio base para las plantillas
		/// </summary>
		internal string Path { get; }
	}
}
