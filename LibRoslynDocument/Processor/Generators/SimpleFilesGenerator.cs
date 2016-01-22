using System;
using System.Collections.Generic;

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
	///		Generador de la documentación simple de una colección de <see cref="DocumentFileModel"/>
	/// </summary>
	internal class SimpleFilesGenerator : AbstractFilesGenerator
	{	
		internal SimpleFilesGenerator(DocumentationParameters objParameters, string strUrlBase) : base(objParameters, strUrlBase) {}

		/// <summary>
		///		Obtiene los tipos de estructuras para los que se deben generar documentos
		/// </summary>
		protected override LanguageStructModel.StructType [] GetTypesDefined()
		{ return new LanguageStructModel.StructType [] 
										{ LanguageStructModel.StructType.Class,
											LanguageStructModel.StructType.Interface,
											LanguageStructModel.StructType.NameSpace,
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
					// Añade los comentarios
						MLBuilderHelper.GetMLRemarks(MLBuilder.Root, objDocument);
					// Añade el cuerpo
						switch (objDocument.StructType)
							{ case LanguageStructModel.StructType.Class:
								case LanguageStructModel.StructType.Interface:
								case LanguageStructModel.StructType.Struct:
										AddTablesComplexParts(MLBuilder.Root, objDocument);
									break;
							}
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
											// Crea la lista de elementos hijo
												AddTreeNameSpaces(objMLListItem, objChild, blnIncludeClasses);
									}
						// Añade la lista a la raíz si tiene elementos
							if (objMLList.Nodes.Count > 0)
								objMLParent.Nodes.Add(objMLList);
				}
		}

		/// <summary>
		///		Añade las tablas de elementos hijo de una estructura
		/// </summary>
		private void AddTablesComplexParts(MLNode objMLRoot, DocumentFileModel objDocument)
		{	// Obtiene la tabla de estructuras
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Estructuras", LanguageStructModel.StructType.Struct);
			// Obtiene la tabla de clases
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Clases", LanguageStructModel.StructType.Class);
			// Obtiene la tabla de interfaces
				MLBuilderHelper.GetTableStructs(objMLRoot, objDocument, "Interfaces", LanguageStructModel.StructType.Interface);
			// Obtiene la tabla de constructores
				GetTableConstructors(objMLRoot, objDocument);
			// Obtiene la tabla de métodos
				GetTableMethods(objMLRoot, objDocument);
			// Obtiene la tabla de propiedades
				GetTableProperties(objMLRoot, objDocument);
			// Obtiene la tabla de enumerados
				GetTableEnums(objMLRoot, objDocument);
		}

		/// <summary>
		///		Obtiene una tabla con los constructores
		/// </summary>
		private void GetTableConstructors(MLNode objMLRoot, DocumentFileModel objDocument)
		{ LanguageStructModelCollection objColStructs = base.SelectItemsForGeneration(objDocument, LanguageStructModel.StructType.Constructor);

				if (objColStructs != null && objColStructs.Count > 0)
					{ MLNode objMLTable;

							// Cabecera
								objMLRoot.Nodes.Add("br");
								objMLRoot.Nodes.Add("h3", "Constructores");
							// Cabecera de tabla
								objMLTable = MLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
							// Recorre los constructores
								foreach (LanguageStructModel objStruct in objColStructs)
									{	ConstructorModel objConstructor = objStruct as ConstructorModel;

											// Añade los datos del constructor
												if (objConstructor != null)
													{	// Sintaxis
															MLBuilder.AddRowTable(objMLTable, MLBuilderHelper.GetModifierText(objConstructor.Modifier),
																										objConstructor.Name, objConstructor.RemarksXml.Summary);
														// Comentarios
															AddRowRemarks(objMLTable, objConstructor.RemarksXml.Remarks, 1, 2);
														// Prototipo
															AddRowRemarks(objMLTable, MLBuilderHelper.GetMethodPrototype(objConstructor, false, null), 1, 2);
														// Argumentos
															if (objConstructor.Arguments.Count > 0)
																MLBuilder.AddRowNode(objMLTable, GetListArguments(objConstructor), 1, 2);
													}
									}
					}
		}

		/// <summary>
		///		Obtiene una tabla con los métodos
		/// </summary>
		private void GetTableMethods(MLNode objMLRoot, DocumentFileModel objDocument)
		{ LanguageStructModelCollection objColStructs = base.SelectItemsForGeneration(objDocument, LanguageStructModel.StructType.Method);

				if (objColStructs != null && objColStructs.Count > 0)
					{ MLNode objMLTable;

							// Cabecera
								objMLRoot.Nodes.Add("br");
								objMLRoot.Nodes.Add("h3", "Métodos");
							// Cabecera de tabla
								objMLTable = MLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
							// Recorre los métodos
								foreach (LanguageStructModel objStruct in objColStructs)
									{	MethodModel objMethod = objStruct as MethodModel;

											// Añade los datos del método
												if (objMethod != null)
													{	// Cabecera
															MLBuilder.AddRowTable(objMLTable, MLBuilderHelper.GetModifierText(objMethod.Modifier),
																										objMethod.Name, objMethod.RemarksXml.Summary);
														// Comentarios
															AddRowRemarks(objMLTable, objMethod.RemarksXml.Remarks, 1, 2);
														// Prototipo
															AddRowRemarks(objMLTable, MLBuilderHelper.GetMethodPrototype(objMethod, objMethod.IsAsync, objMethod.ReturnType), 1, 2);
														// Argumentos
															if (objMethod.Arguments.Count > 0)
																MLBuilder.AddRowNode(objMLTable, GetListArguments(objMethod), 1, 2);
														// Valor devuelto
															MLBuilder.AddRowNode(objMLTable, GetListReturnData(objMethod, objMethod.ReturnType), 1, 2);
													}
									}
					}
		}

		/// <summary>
		///		Obtiene la tabla de propiedades
		/// </summary>
		private void GetTableProperties(MLNode objMLRoot, DocumentFileModel objDocument)
		{ LanguageStructModelCollection objColStructs = base.SelectItemsForGeneration(objDocument, LanguageStructModel.StructType.Property);

				if (objColStructs != null && objColStructs.Count > 0)
					{ MLNode objMLTable;

							// Cabecera
								objMLRoot.Nodes.Add("br");
								objMLRoot.Nodes.Add("h3", "Propiedades");
							// Cabecera de tabla
								objMLTable = MLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
							// Recorre las propiedades
								foreach (LanguageStructModel objStruct in objColStructs)
									{	PropertyModel objProperty = objStruct as PropertyModel;

											// Añade los datos de la propiedad
												if (objProperty != null)
													{ // Cabecera
															MLBuilder.AddRowTable(objMLTable, MLBuilderHelper.GetModifierText(objProperty.Modifier),
																										objProperty.Name, objProperty.RemarksXml.Summary);
														// Comentarios
															AddRowRemarks(objMLTable, objProperty.RemarksXml.Remarks, 1, 2);
														// Prototipo
															AddRowRemarks(objMLTable, MLBuilderHelper.GetPropertyPrototype(objProperty), 1, 2);
														// Valor devuelto
															if (objProperty.GetMethod != null)
																MLBuilder.AddRowNode(objMLTable, GetListReturnData(objProperty.GetMethod, objProperty.GetMethod.ReturnType), 
																										 1, 2);
													}
									}
						}
		}

		/// <summary>
		///		Obtiene una lista de argumentos
		/// </summary>
		private MLNodesCollection GetListArguments(BaseMethodModel objMethod)
		{	MLNodesCollection objColMLNodes = new MLNodesCollection(); 

				// Añade los elementos a la lista
					if (objMethod.Arguments != null && objMethod.Arguments.Count > 0)
						{ MLNode objMLList;

								// Cabecera
									objColMLNodes.Add("h4", "Argumentos");
								// Lista de elementos
									objMLList = objColMLNodes.Add("ul");
									foreach (ArgumentModel objArgument in objMethod.Arguments)
										objMLList.Nodes.Add(MLBuilder.GetListItem(MLBuilder.GetSpan(objArgument.Name, true),
																															MLBuilder.GetSpan("(" + MLBuilderHelper.GetTypeName(objArgument.Type) + ")", false, true),
																															MLBuilder.GetSpan(":", true),
																															MLBuilder.GetSpan(objMethod.RemarksXml.GetParameterRemarks(objArgument.Name))));
						}
					else
						objColMLNodes.Add("h4", "Sin argumentos");
				// Devuelve la colección de nodos
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene el MLNode del valor devuelto
		/// </summary>
		private MLNode GetListReturnData(BaseMethodModel objMethod, TypedModel objReturnType)
		{	MLNode objMLNode = new MLNode("ul");

				// Añade los datos al nodo
					objMLNode.Nodes.Add(MLBuilder.GetListItem(MLBuilder.GetSpan("Valor de retorno", true), 
																										MLBuilder.GetSpan(MLBuilderHelper.GetTypeName(objReturnType))));
					if (!objMethod.RemarksXml.Returns.IsEmpty())
						objMLNode.Nodes.Add("li").Nodes.AddRange(MLBuilderHelper.GetTagsRemarksXml(objMethod.RemarksXml.Returns));
				// Devuelve el nodo
					return objMLNode;
		}

		/// <summary>
		///		Obtiene una tabla con los datos de los enumerados de un documento
		/// </summary>
		private void GetTableEnums(MLNode objMLRoot, DocumentFileModel objDocument)
		{ LanguageStructModelCollection objColStructs = base.SelectItemsForGeneration(objDocument, LanguageStructModel.StructType.Enum);

				if (objColStructs != null && objColStructs.Count > 0)
					{ MLNode objMLTable;

							// Cabecera
								objMLRoot.Nodes.Add("br");
								objMLRoot.Nodes.Add("h3", "Enumerados");
							// Cabecera de tabla
								objMLTable = MLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
							// Recorre los enumerados
								foreach (LanguageStructModel objStruct in objColStructs)
									{ EnumModel objEnum = objStruct as EnumModel;

											if (objEnum != null)
												{ // Cabecera del enumerado
														MLBuilder.AddRowTable(objMLTable, MLBuilderHelper.GetModifierText(objEnum.Modifier),
																									objEnum.Name, objEnum.RemarksXml.Summary);
													// Comentarios adicionales
														AddRowRemarks(objMLTable, objEnum.RemarksXml.Remarks, 1, 2);
													// Añade los miembros del enumerado
														foreach (LanguageStructModel objMember in objEnum.Items)
															MLBuilder.AddRowTable(objMLTable, "", objMember.Name, objMember.RemarksXml.Summary);
												}
								}
					}
		}

		/// <summary>
		///		Añade una fila con los comentarios
		/// </summary>
		private void AddRowRemarks(MLNode objMLTable, string strSummary, int intEmptyCells, int intColSpan)
		{ if (!strSummary.IsEmpty())
				{ MLNode objMLRow = objMLTable.Nodes.Add("tr");

						// Añade las celdas vacías
							MLBuilder.AddEmptyCells(objMLRow, intEmptyCells);
							MLBuilder.GetCell(objMLRow, strSummary, intColSpan);
				}
		}

		/// <summary>
		///		Obtiene una tabla de estructuras de determinado tipo
		/// </summary>
		private void GetTableStructs(MLNode objMLRoot, LanguageStructModelCollection objColStructs, string strTitle)
		{ if (objColStructs != null && objColStructs.Count > 0)
				{ MLNode objMLTable;

						// Título
							objMLRoot.Nodes.Add("br");
							objMLRoot.Nodes.Add("h3", strTitle);
						// Tabla
							objMLTable = MLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
						// Añade las filas
							foreach (LanguageStructModel objStruct in objColStructs)
								{ MLNode objMLRow = objMLTable.Nodes.Add("tr");

										objMLRow.Nodes.Add("td", MLBuilderHelper.GetModifierText(objStruct.Modifier));
										// objMLRow.Nodes.Add("td").Nodes.Add(MLBuilderHelper.GetLink(objStruct));
										objMLRow.Nodes.Add("td", objStruct.RemarksXml.Summary);
								}
				}
		}
	}
}