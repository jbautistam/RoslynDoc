using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Methods;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Simple;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Structs;

namespace Bau.Libraries.LibRoslynDocument.Processor.Generators
{
	/// <summary>
	///		Generador de la documentación completa de un <see cref="DocumentFileModel"/>
	/// </summary>
	internal class ComplexFilesGenerator : AbstractFilesGenerator
	{	
		internal ComplexFilesGenerator(DocumentationParameters objParameters, string strUrlBase) : base(objParameters, strUrlBase) {}

		/// <summary>
		///		Obtiene los tipos de estructuras para los que se deben generar documentos
		/// </summary>
		protected override LanguageStructModel.StructType [] GetTypesDefined()
		{ return new LanguageStructModel.StructType [] 
										{ LanguageStructModel.StructType.Class,
											LanguageStructModel.StructType.Constructor,
											LanguageStructModel.StructType.Enum,
											LanguageStructModel.StructType.Interface,
											LanguageStructModel.StructType.Method,
											LanguageStructModel.StructType.NameSpace,
											LanguageStructModel.StructType.Property,
											LanguageStructModel.StructType.Struct
										};
		}

		/// <summary>
		///		Obtiene el ML del elemento
		/// </summary>
		protected override void CreateInnerDocument(DocumentFileModel objDocument)
		{ if (objDocument.StructType == LanguageStructModel.StructType.NameSpace)
				GetMLNameSpace(MLBuilder.Root, objDocument);
			else
				{ // Añade la cabecera
						MLBuilderHelper.GetMLHeader(MLBuilder.Root, objDocument);
					// Añade el cuerpo
						switch (objDocument.StructType)
							{ case LanguageStructModel.StructType.Class:
										GetMLClass(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Interface:
										GetMLInterface(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Struct:
										GetMLStruct(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Constructor:
										GetMLConstructor(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Method:
										GetMLMethod(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Property:
										GetMLProperty(MLBuilder.Root, objDocument);
									break;
								case LanguageStructModel.StructType.Enum:
										GetMLEnum(MLBuilder.Root, objDocument);
									break;
							}
					// Añade los comentarios
						MLBuilderHelper.GetMLRemarks(MLBuilder.Root, objDocument);
				}
		}

		/// <summary>
		///		Obtiene el Nhaml de un espacio de nombres
		/// </summary>
		private void GetMLNameSpace(MLNode objMLRoot, DocumentFileModel objDocument)
		{ // Cabecera
				objMLRoot.Nodes.Add("h2", "Espacios de nombres");
			// Lista de espacios de nombre hijos
				AddTreeNameSpaces(objMLRoot, objDocument, false);
			// Tablas
				AddTablesComplexParts(objMLRoot, objDocument);
		}

		/// <summary>
		///		Añade el árbol de espacios de nombres hijo
		/// </summary>
		private void AddTreeNameSpaces(MLNode objMLParent, DocumentFileModel objDocument, bool blnIncludeClasses)
		{	if (objDocument.Childs.ExistsItems(LanguageStructModel.StructType.NameSpace))
				{ MLNode objMLList = new MLNode("ul");

						// Añade los elementos hijo
							foreach (DocumentFileModel objChild in objDocument.Childs)
								if (objChild.StructType == LanguageStructModel.StructType.NameSpace)
									{ MLNode objMLListItem = objMLParent.Nodes.Add("li");

											// Crea el enlace al espacio de nombres
												objMLListItem.Nodes.Add(MLBuilderHelper.GetLink(objChild));
											// Crea el enlace a las clases
												AddListChildClasses(objMLListItem, objChild.Childs);
											// Crea la lista de elementos hijo
												AddTreeNameSpaces(objMLListItem, objChild, blnIncludeClasses);
									}
						// Añade la lista a la raíz si tiene elementos
							if (objMLList.Nodes.Count > 0)
								objMLParent.Nodes.Add(objMLList);
				}
		}

		/// <summary>
		///		Añade una lista de clases
		/// </summary>
		private void AddListChildClasses(MLNode objMLRoot, DocumentFileModelCollection objColDocuments)
		{ if (objColDocuments.ExistsItems(LanguageStructModel.StructType.Class) ||
					objColDocuments.ExistsItems(LanguageStructModel.StructType.Interface))
				{ MLNode objMLList = objMLRoot.Nodes.Add("ul");

						// Añade las clases
							foreach (DocumentFileModel objDocument in objColDocuments)
								if (objDocument.StructType == LanguageStructModel.StructType.Class)
									objMLList.Nodes.Add(MLBuilder.GetListItem(MLBuilder.GetSpan("Clase:", true), 
																			MLBuilderHelper.GetLink(objDocument)));
						// Añade las interfaces
							foreach (DocumentFileModel objDocument in objColDocuments)
								if (objDocument.StructType == LanguageStructModel.StructType.Interface)
									objMLList.Nodes.Add(MLBuilder.GetListItem(MLBuilder.GetSpan("Interface:", true), 
																			MLBuilderHelper.GetLink(objDocument)));
				}
		}

		/// <summary>
		///		Obtiene el MLNode de una clase
		/// </summary>
		private void GetMLClass(MLNode objMLRoot, DocumentFileModel objDocument)
		{ ClassModel objClass = objDocument.LanguageStruct as ClassModel;

				if (objClass != null)
					AddTablesComplexParts(objMLRoot, objDocument);
		}

		/// <summary>
		///		Obtiene el MLNode de una estructura
		/// </summary>
		private void GetMLStruct(MLNode objMLRoot, DocumentFileModel objDocument)
		{ StructModel objStruct = objDocument.LanguageStruct as StructModel;

				if (objStruct != null)
					AddTablesComplexParts(objMLRoot, objDocument);
		}

		/// <summary>
		///		Obtiene el MLNode de un interface
		/// </summary>
		private void GetMLInterface(MLNode objMLRoot, DocumentFileModel objDocument)
		{ InterfaceModel objInterface = objDocument.LanguageStruct as InterfaceModel;

				if (objInterface != null)
					AddTablesComplexParts(objMLRoot, objDocument);
		}

		/// <summary>
		///		Añade las tablas de elementos hijo de una estructura
		/// </summary>
		private void AddTablesComplexParts(MLNode objMLRoot, DocumentFileModel objDocument)
		{	// Obtiene la tabla de constructores
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Constructores", LanguageStructModel.StructType.Constructor);
			// Obtiene la tabla de métodos
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Métodos", LanguageStructModel.StructType.Method);
			// Obtiene la tabla de propiedades
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Propiedades", LanguageStructModel.StructType.Property);
			// Obtiene la tabla de enumerados
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Enumerados", LanguageStructModel.StructType.Enum);
			// Obtiene la tabla de estructuras
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Estructuras", LanguageStructModel.StructType.Struct);
			// Obtiene la tabla de clases
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Clases", LanguageStructModel.StructType.Class);
			// Obtiene la tabla de interfaces
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Interfaces", LanguageStructModel.StructType.Interface);
		}

		/// <summary>
		///		Obtiene el MLNode de un constructor
		/// </summary>
		private void GetMLConstructor(MLNode objMLRoot, DocumentFileModel objDocument)
		{ ConstructorModel objConstructor = objDocument.LanguageStruct as ConstructorModel;

				// Añade los datos del constructor
					if (objConstructor != null)
						{	// Sintaxis
								objMLRoot.Nodes.Add("h3", "Sintaxis");
								objMLRoot.Nodes.Add("p", MLBuilderHelper.GetMethodPrototype(objConstructor));
							// Argumentos
								AddTableArguments(objMLRoot, objConstructor);
						}
		}

		/// <summary>
		///		Obtiene el MLNode de un método
		/// </summary>
		private void GetMLMethod(MLNode objMLRoot, DocumentFileModel objDocument)
		{ MethodModel objMethod = objDocument.LanguageStruct as MethodModel;

				// Añade los datos del método
					if (objMethod != null)
						{	// Sintaxis
								objMLRoot.Nodes.Add("h3", "Sintaxis");
								objMLRoot.Nodes.Add("p", MLBuilderHelper.GetMethodPrototype(objMethod, objMethod.IsAsync, objMethod.ReturnType));
								objMLRoot.Nodes.Add("br");
							// Argumentos
								AddTableArguments(objMLRoot, objMethod);
							// Valor devuelto
								GetMLReturn(objMLRoot, objMethod, objMethod.ReturnType);
						}
		}

		/// <summary>
		///		Obtiene el MLNode de una propiedad
		/// </summary>
		private void GetMLProperty(MLNode objMLRoot, DocumentFileModel objDocument)
		{ PropertyModel objProperty = objDocument.LanguageStruct as PropertyModel;

				// Añade los datos del método
					if (objProperty != null)
						{ // Sintaxis
								objMLRoot.Nodes.Add("h3", "Sintaxis");
								objMLRoot.Nodes.Add("p", MLBuilderHelper.GetPropertyPrototype(objProperty));
							// Valor devuelto
								if (objProperty.GetMethod != null)
									GetMLReturn(objMLRoot, objProperty, objProperty.GetMethod.ReturnType);
						}
		}

		/// <summary>
		///		Obtiene el MLNode del valor devuelto
		/// </summary>
		private void GetMLReturn(MLNode objMLRoot, BaseMethodModel objMethod, TypedModel objReturnType)
		{	objMLRoot.Nodes.Add("h3", "Valor devuelto");
			objMLRoot.Nodes.Add("p", MLBuilderHelper.GetTypeName(objReturnType));
			objMLRoot.Nodes.AddRange(MLBuilderHelper.GetTagsRemarksXml(objMethod.RemarksXml.Returns));
		}

		/// <summary>
		///		Añade una tabla de argumentos
		/// </summary>
		private void AddTableArguments(MLNode objMLRoot, BaseMethodModel objMethod)
		{	if (objMethod.Arguments != null && objMethod.Arguments.Count > 0)
				{ MLNode objMLTable;

						// Cabecera
							objMLRoot.Nodes.Add("h3", "Parámetros");
						// Cabecera de tabla
							objMLTable = MLBuilder.AddTable(objMLRoot, "Nombre", "Tipo", "Descripción");
						// Añade los argumentos
							foreach (ArgumentModel objArgument in objMethod.Arguments)
								MLBuilder.AddRowTable(objMLTable, objArgument.Name, 
																			MLBuilderHelper.GetTypeName(objArgument.Type), 
																			objMethod.RemarksXml.GetParameterRemarks(objArgument.Name));
				}
		}

		/// <summary>
		///		Obtiene el MLNode de un enumerado
		/// </summary>
		private void GetMLEnum(MLNode objMLRoot, DocumentFileModel objDocument)
		{ EnumModel objEnum = objDocument.LanguageStruct as EnumModel;

				if (objEnum != null)
					{ MLNode objMLTable;

							// Cabecera
								objMLRoot.Nodes.Add("h3", "Miembros");
							// Cabecera de tabla
								objMLTable = MLBuilder.AddTable(objMLRoot, "Nombre", "Descripción");
							// Añade los miembros del enumerado
								foreach (EnumMemberModel objMember in objEnum.Items)
									MLBuilder.AddRowTable(objMLTable, objMember.Name, objMember.RemarksXml.Summary);
					}
		}
	}
}