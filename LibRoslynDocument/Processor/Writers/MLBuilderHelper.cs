using System;
using System.Collections.Generic;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Methods;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Simple;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Structs;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers
{
	/// <summary>
	///		Clase de ayuda para la generación de cadenas de las estructuras del lenguaje
	/// </summary>
	internal class MLBuilderHelper
	{ // Variables privadas
			private MLIntermedialBuilder objMLBuilder;
			private string strUrlBase;

		internal MLBuilderHelper(MLIntermedialBuilder objMLBuilder, string strUrlBase)
		{ this.objMLBuilder = objMLBuilder;
			this.strUrlBase = strUrlBase;
		}

		/// <summary>
		///		Obtiene la cabecera de un documento
		/// </summary>
		internal void GetMLHeader(MLNode objMLRoot, DocumentFileModel objDocument)
		{ MLNode objMLList;

				// Nombre del elemento
					objMLRoot.Nodes.Add("h1", GetTypeName(objDocument.StructType) + " " + objDocument.Name);
				// Resumen
					objMLRoot.Nodes.AddRange(GetTagsRemarksXml(objDocument.LanguageStruct.RemarksXml.Summary));
				// Espacio de nombres
					objMLList = objMLRoot.Nodes.Add("ul");
					objMLList.Nodes.Add(objMLBuilder.GetListItem(objMLBuilder.GetSpan("Espacio de nombres:", true), 
															GetLinkNameSpace(objDocument)));
				// Si es una clase, interface o estructura (BaseComplexModel), añade el resto de elementos de la lista
					if (objDocument.LanguageStruct is BaseComplexModel)
						{ BaseComplexModel objComplex = objDocument.LanguageStruct as BaseComplexModel;

								if (objComplex != null)
									{ string strInterfaces = "";

											// Añade la clase base
												if (objComplex.BaseClass != null)
													objMLList.Nodes.Add(objMLBuilder.GetListItem(objMLBuilder.GetSpan("Base:", true), 
																																			 objMLBuilder.GetSpan(GetTypeName(objComplex.BaseClass))));
											// Añade los interfaces
												foreach (string strBase in objComplex.Interfaces)
													strInterfaces = strInterfaces.AddWithSeparator(strBase, ",");
											// Añade la cadena de interfaces
												if (!strInterfaces.IsEmpty())
													objMLList.Nodes.Add(objMLBuilder.GetListItem(objMLBuilder.GetSpan("Interfaces:", true), 
																							objMLBuilder.GetSpan(strInterfaces)));
										}
						}
		}


		/// <summary>
		///		Obtiene el nombre de un tipo de estructura
		/// </summary>
		private string GetTypeName(LanguageStructModel.StructType intIDType)
		{ switch (intIDType)
				{ case LanguageStructModel.StructType.CompilationUnit:
						return "Unidad de compilación";
					case LanguageStructModel.StructType.NameSpace:
						return "Espacio de nombres";
					case LanguageStructModel.StructType.Class:
						return "Clase";
					case LanguageStructModel.StructType.Interface:
						return "Interface";
					case LanguageStructModel.StructType.Constructor:
						return "Constructor";
					case LanguageStructModel.StructType.Method:
						return "Método";
					case LanguageStructModel.StructType.Enum:
						return "Enumerado";
					case LanguageStructModel.StructType.EnumMember:
						return "Miembro de enumerado";
					case LanguageStructModel.StructType.Property:
						return "Propiedad";
					case LanguageStructModel.StructType.Struct:
						return "Estructura";
					default:
						return intIDType.ToString();
				}
		}

		/// <summary>
		///		Añade los comentarios finales de un documento
		/// </summary>
		internal void GetMLRemarks(MLNode objMLRoot, DocumentFileModel objDocument)
		{ if (!objDocument.LanguageStruct.RemarksXml.Remarks.IsEmpty())
				{ objMLRoot.Nodes.Add("br");
					objMLRoot.Nodes.Add("h2", "Comentarios");
					objMLRoot.Nodes.AddRange(GetTagsRemarksXml(objDocument.LanguageStruct.RemarksXml.Remarks));
				}
		}

		/// <summary>
		///		Obtiene el prototipo de una propiedad
		/// </summary>
		internal string GetPropertyPrototype(PropertyModel objProperty)
		{ string strPrototype = GetMethodPrototypeDefinition(objProperty, false, objProperty.GetMethod.ReturnType);

				// Argumentos
					if (objProperty.Arguments.Count > 0)
						strPrototype += "[" + GetArgumentsPrototype(objProperty) + "]";
				// Get y set
					strPrototype += " {";
					strPrototype = strPrototype.AddWithSeparator(GetPropertyMethodPrototype(objProperty.Modifier, objProperty.GetMethod, "get"), " ", false);
					strPrototype = strPrototype.AddWithSeparator(GetPropertyMethodPrototype(objProperty.Modifier, objProperty.SetMethod, "set"), " ", false);
					strPrototype += " }";
				// Devuelve el prototipo
					return strPrototype;
		}

		/// <summary>
		///		Obtiene el prototipo de una función
		/// </summary>
		internal string GetMethodPrototype(BaseMethodModel objMethod, bool blnIsAsync = false, TypedModel objReturnType = null)
		{	string strPrototype = GetMethodPrototypeDefinition(objMethod, blnIsAsync, objReturnType);

				// Añade los argumentos
					strPrototype += "(" + GetArgumentsPrototype(objMethod) + ")";
				// Devuelve el prototipo
					return strPrototype;
		}

		/// <summary>
		///		Obtiene la definición (el prefijo) de un prototipo de un método
		/// </summary>
		private string GetMethodPrototypeDefinition(BaseMethodModel objMethod, bool blnIsAsync, TypedModel objReturnType)
		{ string strPrototype = GetModifierText(objMethod.Modifier);

				// Añade los modificadores
					if (blnIsAsync)
						strPrototype = strPrototype.AddWithSeparator("async", " ", false);
					if (objMethod.IsAbstract)
						strPrototype = strPrototype.AddWithSeparator("abstract", " ", false);
					if (objMethod.IsOverride)
						strPrototype = strPrototype.AddWithSeparator("override", " ", false);
					if (objMethod.IsSealed)
						strPrototype = strPrototype.AddWithSeparator("sealed", " ", false);
					if (objMethod.IsStatic)
						strPrototype = strPrototype.AddWithSeparator("static", " ", false);
					if (objMethod.IsVirtual)
						strPrototype = strPrototype.AddWithSeparator("virtual", " ", false);
				// Añade el valor de retorno
					if (objReturnType != null)
						strPrototype = strPrototype.AddWithSeparator(GetTypeName(objReturnType), " ", false);
				// Añade el nombre del método
					strPrototype = strPrototype.AddWithSeparator(objMethod.Name, " ", false);
				// Añade los genéricos
					strPrototype += GetMethodPrototypeGenerics(objMethod.TypeParameters);
				// Devuelve la definición de prototipo
					return strPrototype;
		}

		/// <summary>
		///		Obtiene el prototipo de un método Get o Set de una propiedad
		/// </summary>
		private string GetPropertyMethodPrototype(LanguageStructModel.ModifierType intModifier, MethodModel objMethod, string strMethodName)
		{	string strPrototype = "";

				// Añade los datos al prototipo
					if (objMethod != null)
						{ // Añade el modificador (si es distinto al de la propiedad, es decir: public Property { private set }
								if (intModifier != objMethod.Modifier)
									strPrototype += GetModifierText(objMethod.Modifier);
							// Añade el nombre
								strPrototype = strPrototype.AddWithSeparator(strMethodName + ";", " ", false);
						}
				// Devuelve el prototipo
					return strPrototype;
		}

		/// <summary>
		///		Obtiene las definiciones de genéricos
		/// </summary>
		private string GetMethodPrototypeGenerics(List<TypeParameterModel> objColTypeParameters)
		{ string strParameters = "";

				// Añade las definiciones de parámetros
					if (objColTypeParameters != null && objColTypeParameters.Count > 0)
						{ // Añade los parámetros
								foreach (TypeParameterModel objTypeParameter in objColTypeParameters)
									strParameters = strParameters.AddWithSeparator(objTypeParameter.Name, ",");
							// Añade los caracteres de apertura y cierre
								strParameters = "<" + strParameters + ">";
						}
				// Devuelve los parámetros
					return strParameters;
		}

		/// <summary>
		///		Obtiene los argumentos para un prototipo
		/// </summary>
		private string GetArgumentsPrototype(BaseMethodModel objMethod)
		{ string strArguments = "";

				// Añade la cadena con los argumentos
					for (int intIndex = 0; intIndex < objMethod.Arguments.Count; intIndex++)
						strArguments = strArguments.AddWithSeparator(GetArgumentData(objMethod.Arguments[intIndex]), ",");
				// Devuelve los argumentos
					return strArguments;
		}

		/// <summary>
		///		Obtiene los datos de un argumento
		/// </summary>
		private string GetArgumentData(ArgumentModel objArgument)
		{ string strArgument = "";

				// Añade los datos del argumento
					if (objArgument.IsThis)
						strArgument = strArgument.AddWithSeparator("this", " ", false);
				// Añade el tipo de referencia
					switch (objArgument.RefType)
						{	case ArgumentModel.ArgumentType.ByOut:
									strArgument = strArgument.AddWithSeparator("out", " ", false);
								break;
							case ArgumentModel.ArgumentType.ByRef:
									strArgument = strArgument.AddWithSeparator("ref", " ", false);
								break;
						}
				// Añade el valor que indica si es un array de parámetros
					if (objArgument.IsParams)
						strArgument = strArgument.AddWithSeparator("params", " ", false);
				// Añade el tipo
					strArgument = strArgument.AddWithSeparator(GetTypeName(objArgument.Type), " ", false);
				// Añade el nombre del argumento
					strArgument = strArgument.AddWithSeparator(objArgument.Name, " ", false);
				// Si tiene un valor por defecto, lo añade
					if (objArgument.IsOptional)
						{ // Añade el igual
								strArgument = strArgument.AddWithSeparator("=", " ", false);
							// Añade el parámetro
								strArgument = strArgument.AddWithSeparator(objArgument.Default, " ", false);
						}
				// Devuelve la cadena del argumento
					return strArgument;
		}

		/// <summary>
		///		Obtiene el nombre del tipo
		/// </summary>
		internal string GetTypeName(TypedModel objType)
		{ string strType = objType.Name;

				// Si se trata de un array, añade los datos
					if (objType.IsArray)
						strType = strType + "[]";
				// Devuelve la cadena del tipo
					return strType;
		}

		/// <summary>
		///		Obtiene el texto del modificador
		/// </summary>
		internal string GetModifierText(LanguageStructModel.ModifierType intIDModifier)
		{ switch (intIDModifier)
				{	case LanguageStructModel.ModifierType.Private:
						return "private";
					case LanguageStructModel.ModifierType.Protected:
						return "protected";
					case LanguageStructModel.ModifierType.Internal:
						return "internal";
					case LanguageStructModel.ModifierType.ProtectedAndInternal:
					case LanguageStructModel.ModifierType.ProtectedOrInternal:
						return "internal internal";
					case LanguageStructModel.ModifierType.Public:
						return "public";
					default:
						return "";
				}
		}

		/// <summary>
		///		Obtiene el vínculo a un espacio de nombres
		/// </summary>
		internal MLNode GetLinkNameSpace(DocumentFileModel objDocument)
		{ MLNode objMLNode = null;

				// Obtiene la Url al espacio de nombres
					if (objDocument.StructType == LanguageStructModel.StructType.NameSpace)
						objMLNode = GetLink(objDocument);
					else if (objDocument.Parent != null)
						objMLNode = GetLinkNameSpace(objDocument.Parent);
				// Devuelve la Url
					return objMLNode;
		}

		/// <summary>
		///		Obtiene un vínculo a un documento
		/// </summary>
		internal MLNode GetLink(DocumentFileModel objDocument)
		{ return objMLBuilder.GetLink(objDocument.Name, objDocument.GetUrl(strUrlBase));
		}

		/// <summary>
		///		Obtiene las etiquetas de comentarios XML (un párrafo por cada línea de comentarios)
		/// </summary>
		internal MLNodesCollection GetTagsRemarksXml(string strRemarks)
		{ MLNodesCollection objColMLNodes = new MLNodesCollection();

				// Añade los párrafos como nodos
					if (!strRemarks.IsEmpty())
						{ string [] arrStrRemarks = strRemarks.Split('\n');

								// Añade los párrafos
									foreach (string strParagraph in arrStrRemarks)
										if (!strParagraph.IsEmpty())
											objColMLNodes.Add("p", strParagraph.Trim());
						}
				// Devuelve los nodos
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene una tabla de estructuras de determinado tipo
		/// </summary>
		internal void GetTableStructs(MLNode objMLRoot, DocumentFileModel objDocument, string strTitle, LanguageStructModel.StructType intIDType)
		{ if (objDocument.Childs.ExistsItems(intIDType))
				{ MLNode objMLTable;

						// Título
							objMLRoot.Nodes.Add("br");
							objMLRoot.Nodes.Add("h3", strTitle);
						// Tabla
							objMLTable = objMLBuilder.AddTable(objMLRoot, "Ambito", "Nombre", "Descripción");
						// Añade las filas
							foreach (DocumentFileModel objChild in objDocument.Childs)
								if (objChild.StructType == intIDType)
									{ MLNode objMLRow = objMLTable.Nodes.Add("tr");

											objMLRow.Nodes.Add("td", GetModifierText(objChild.LanguageStruct.Modifier));
											objMLRow.Nodes.Add("td").Nodes.Add(GetLink(objChild));
											objMLRow.Nodes.Add("td", objChild.LanguageStruct.RemarksXml.Summary);
									}
				}
		}
	}
}
