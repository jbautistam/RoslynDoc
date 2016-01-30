using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynDocument.Models.Templates;

namespace Bau.Libraries.LibRoslynDocument.Repository.Templates
{
	/// <summary>
	///		Repository de <see cref="TemplateModel"/>
	/// </summary>
	internal class TemplateRepository
	{
		/// <summary>
		///		Carga el archivo de definición de plantillas
		/// </summary>
		internal TemplateModelCollection Load(string strFileName)
		{ TemplateModelCollection objColTemplates = new TemplateModelCollection(System.IO.Path.GetDirectoryName(strFileName));

				// Carga las plantillas
					if (System.IO.File.Exists(strFileName))
						{ MLFile objMLFile = new LibMarkupLanguage.Services.XML.XMLParser().Load(strFileName);

								foreach (MLNode objMLRoot in objMLFile.Nodes)
									if (objMLRoot.Name == "Templates")
										foreach (MLNode objMLTemplate in objMLRoot.Nodes)
											if (objMLTemplate.Name == "Page")
												{ LanguageStructModel.StructType intIDType = GetLanguageStruct(objMLTemplate.Attributes["StructType"].Value);
													string strRelativeFileName = objMLTemplate.Attributes["File"].Value;
													string strRootTemplate = objMLTemplate.Attributes["RootTemplate"].Value;

														if (intIDType != LanguageStructModel.StructType.Unknown && !strRelativeFileName.IsEmpty())
															objColTemplates.Add(intIDType, strRelativeFileName, strRootTemplate);
												}
						}
				// Devuelve la colección de plantillas
					return objColTemplates;
		}

		/// <summary>
		///		Carga el texto de una plantilla raíz
		/// </summary>
		internal string LoadTextRootTemplate(string strFileName)
		{ string strText = "";

				// Carga los datos de la plantilla
					if (System.IO.File.Exists(strFileName))
						try
							{ MLFile objMLFile = new LibMarkupLanguage.Services.XML.XMLParser().Load(strFileName);

									foreach (MLNode objMLNode in objMLFile.Nodes)
										if (objMLNode.Name == "Page")
											strText = objMLNode.Value;
							}
						catch {}
				// Devuelve el texto
					return strText;
		}

		/// <summary>
		///		Obtiene el tipo de estructura
		/// </summary>
		private LanguageStructModel.StructType GetLanguageStruct(string strStruct)
		{ LanguageStructModel.StructType intIDType = LanguageStructModel.StructType.Unknown;
			string [] arrStrToken = Enum.GetNames(typeof(LanguageStructModel.StructType));
			Array arrIntValues = Enum.GetValues(typeof(LanguageStructModel.StructType));

				// Obtiene el tipo del enumerado
					if (!strStruct.IsEmpty())
						for (int intIndex = 0; intIndex < arrStrToken.Length; intIndex++)
							if (arrStrToken[intIndex].EqualsIgnoreCase(strStruct))
								intIDType = (LanguageStructModel.StructType) arrIntValues.GetValue(intIndex);
				// Devuelve el tipo
					return intIDType;
		}
	}
}
