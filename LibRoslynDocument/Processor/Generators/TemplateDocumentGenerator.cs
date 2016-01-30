using System;
using System.Collections.Generic;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibRoslynDocument.Models.Documents;
using Bau.Libraries.LibRoslynDocument.Models.Templates;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Methods;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Simple;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Structs;

namespace Bau.Libraries.LibRoslynDocument.Processor.Generators
{
	/// <summary>
	///		Genera la documentación intermedia a partir de una plantilla
	/// </summary>
	internal class TemplateDocumentGenerator
	{ // Constantes privadas
			private const string cnstStrTagIfExists = "IfExists";
			private const string cnstStrTagIfValue = "IfValue";
			private const string cnstStrTagForEach = "ForEach";
			private const string cnstStrTagAttributeParameter = "Parameter";
			private const string cnstStrTagAttributeAddToReferences = "AddToReferences";
			private const string cnstStrTagAttributeStructType = "StructType";
			private const string cnstStrTagAttributeValueType = "ValueType";

		internal TemplateDocumentGenerator(ProgramDocumentationGenerator objDocumentationGenerator, 
																			 TemplateModel objTemplate, DocumentFileModel objDocument,
																			 string strUrlBase)
		{ Generator = objDocumentationGenerator;
			Template = objTemplate;
			Document = objDocument;
			UrlBase = strUrlBase;
		}

		/// <summary>
		///		Procesa el documento
		/// </summary>
		internal void Process()
		{ MLFile objMLFile = new LibMarkupLanguage.Services.XML.XMLParser().Load(Template.FullFileName);

				// Limpia el generador
					Document.MLBuilder.Clear();
				// Añade la estructura a la colección de estructuras generadas
					Document.StructsReferenced.Add(Document.LanguageStruct);
				// Recorre los datos del archivo generando la salida
					foreach (MLNode objMLSource in objMLFile.Nodes)
						if (objMLSource.Name == Document.MLBuilder.Root.Name)
							TreatChilds(objMLSource.Nodes, Document.MLBuilder.Root, Document.LanguageStruct);
		}

		/// <summary>
		///		Trata una serie de nodos
		/// </summary>
		private void TreatChilds(MLNodesCollection objColMLSource, MLNode objMLParentTarget, LanguageStructModel objStruct)
		{ foreach (MLNode objMLSource in objColMLSource)
				TreatNode(objMLSource, objMLParentTarget, objStruct);
		}

		/// <summary>
		///		Trata una serie de nodos para un argumento
		/// </summary>
		private void TreatChilds(MLNodesCollection objColMLSource, MLNode objMLParentTarget, LanguageStructModel objStruct, ArgumentModel objArgument)
		{ foreach (MLNode objMLSource in objColMLSource)
				{ MLNode objMLTarget = CloneNode(objMLSource, objStruct, objArgument);

						// Añade el nodo
							objMLParentTarget.Nodes.Add(objMLTarget);
						// Trata los hijos
							TreatChilds(objMLSource.Nodes, objMLTarget, objStruct, objArgument);
				}
		}

		/// <summary>
		///		Trata el nodo
		/// </summary>
		private void TreatNode(MLNode objMLSource, MLNode objMLParentTarget, LanguageStructModel objStruct)
		{ switch (objMLSource.Name)
				{	case cnstStrTagIfExists:
							if (CheckIfExistsChild(objStruct, objMLSource.Attributes[cnstStrTagAttributeStructType].Value))
								TreatChilds(objMLSource.Nodes, objMLParentTarget, objStruct);
						break;
					case cnstStrTagIfValue:
							if (CheckIfExistsValue(objStruct, objMLSource.Attributes[cnstStrTagAttributeValueType].Value))
								TreatChilds(objMLSource.Nodes, objMLParentTarget, objStruct);
						break;
					case cnstStrTagForEach:
							TreatLoop(objMLSource, objMLParentTarget, objStruct, objMLSource.Attributes[cnstStrTagAttributeStructType].Value);
						break;
					default:
							MLNode objMLTarget = CloneNode(objMLSource, objStruct, null);

								// Añade el nodo
									objMLParentTarget.Nodes.Add(objMLTarget);
								// Trata los hijos
									TreatChilds(objMLSource.Nodes, objMLTarget, objStruct);
						break;
				}
		}

		/// <summary>
		///		Comprueba si existe un tipo de estructura hija
		/// </summary>
		private bool CheckIfExistsChild(LanguageStructModel objStruct, string strStructType)
		{	bool blnExists = false;

				// Comprueba si existe
					if (strStructType.EqualsIgnoreCase(cnstStrTagAttributeParameter))
						{ if (objStruct is BaseMethodModel)
								{ BaseMethodModel objMethod = objStruct as BaseMethodModel;

										if (objMethod.Arguments != null && objMethod.Arguments.Count > 0)
											blnExists = true;
								}
						}
					else
						{ LanguageStructModel.StructType intIDType = GetLanguageStruct(strStructType);
							LanguageStructModelCollection objColStructs = SelectItemsForGeneration(objStruct, intIDType);

								 if (objColStructs != null && objColStructs.Count > 0)
									blnExists = true;
						}
				// Devuelve el valor que indica si existe
					return blnExists;
		}

		/// <summary>
		///		Comprueba si existe algún valor en una estructura
		/// </summary>
		private bool CheckIfExistsValue(LanguageStructModel objStruct, string strName)
		{ MLNode objMLNode = GetStructValue(strName, false, objStruct);
				
				// Obtiene el valor que indica si existe un valor
					return !objMLNode.Value.IsEmpty() || objMLNode.Nodes.Count != 0;
		}

		/// <summary>
		///		Trata un bucle
		/// </summary>
		private void TreatLoop(MLNode objMLSource, MLNode objMLParentTarget, LanguageStructModel objStruct, string strStructType)
		{ if (strStructType.EqualsIgnoreCase(cnstStrTagAttributeParameter))
				{ if (objStruct is BaseMethodModel)
						{ BaseMethodModel objMethod = objStruct as BaseMethodModel;
								
								if (objMethod.Arguments != null && objMethod.Arguments.Count > 0)
									foreach (ArgumentModel objArgument in objMethod.Arguments)
										TreatChilds(objMLSource.Nodes, objMLParentTarget, objStruct, objArgument);
						}
				}
			else
				{ LanguageStructModel.StructType intIDType = GetLanguageStruct(strStructType);
					LanguageStructModelCollection objColStructs = SelectItemsForGeneration(objStruct, intIDType);

						// Crea los nodos hijo
							foreach (LanguageStructModel objChild in objColStructs)
								{ // Si hay que añadirla a la colección de estructuras por referencia, se añade
										if (objMLSource.Attributes[cnstStrTagAttributeAddToReferences].Value.GetBool())
											Document.StructsReferenced.Add(objChild);
									// Ytata los nodos hijo
										TreatChilds(objMLSource.Nodes, objMLParentTarget, objChild);
								}
				}
		}

		/// <summary>
		///		Clona un nodo con los datos de una estructura
		/// </summary>
		private MLNode CloneNode(MLNode objMLSource, LanguageStructModel objStruct, ArgumentModel objArgument)
		{ MLNode objMLTarget = new MLNode(objMLSource.Name);
		
				// Interpreta el contenido
					if (objMLSource.Nodes.Count == 0)
						objMLTarget.Nodes.AddRange(Parse(objMLSource.Value, objStruct, objArgument));
				// Clona los atributos
					foreach (MLAttribute objMLAttribute in objMLSource.Attributes)
						objMLTarget.Attributes.Add(objMLAttribute.Name, objMLAttribute.Value);
				// Devuelve el nodo
					return objMLTarget;
		}

		/// <summary>
		///		Interpreta una cadena sustituyendo los parámetros {{xxxx}} por el valor de la estructura o argumento
		/// </summary>
		private MLNodesCollection Parse(string strValue, LanguageStructModel objStruct, ArgumentModel objArgument)
		{ MLNodesCollection objColMLNodes = new MLNodesCollection();

				// Interpreta la cadena
					while (!strValue.IsEmpty())
						{ // Quita hasta el primer {{ que se encuentra
								objColMLNodes.Add(Document.MLBuilder.GetSpan(strValue.Cut("{{", out strValue)));
							// Si queda algo, recoge hasta el siguiente }}
								if (!strValue.IsEmpty())
									{ string strName = strValue.Cut("}}", out strValue).TrimIgnoreNull();
										bool blnIsLink = false;

											// Comprueba si es un vínculo
												if (!strName.IsEmpty() && strName.EndsWith("|Link", StringComparison.CurrentCultureIgnoreCase))
													{ string strFiller;

															// Indica que es un vínculo
																blnIsLink = true;
															// Quita el vínculo (|Link) del nombre
																strName = strName.Cut("|", out strFiller);
													}
											// Procesa el valor
												if (objArgument == null)
													objColMLNodes.Add(GetStructValue(strName, blnIsLink, objStruct as LanguageStructModel));
												else 
													objColMLNodes.Add(GetArgumentValue(strName, blnIsLink, objStruct, objArgument));
									}
						}
				// Devuelve el valor
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene el valor de una estructura
		/// </summary>
		private MLNode GetStructValue(string strName, bool blnIncludeLink, LanguageStructModel objStruct)
		{ switch (strName.TrimIgnoreNull().ToUpper())
				{	case "MODIFIER":
						return Document.MLBuilder.GetSpan(GetModifierText(objStruct.Modifier));
					case "NAME":
						return Document.MLBuilder.GetSpan(objStruct.Name);
					case "NAMESPACE":
						return GetLinkToNameSpace(objStruct, blnIncludeLink);
					case "BASETYPE":
						return GetLinkBaseType(objStruct, blnIncludeLink);
					case "LINKTO":
						return GetLinkToDocument(objStruct, DocumentFileModel.SearchMode.Childs);
					case "STRUCTTYPE":
						return Document.MLBuilder.GetSpan(GetStructTypeName(objStruct.IDType));
					case "SUMMARY":
						return Document.MLBuilder.GetSpan(objStruct.RemarksXml.Summary);
					case "REMARKS":
						return Document.MLBuilder.GetSpan(objStruct.RemarksXml.Remarks);
					case "PROTOTYPE":
						return GetPrototype(objStruct);
					case "RETURNTYPE":
						if (objStruct is PropertyModel && (objStruct as PropertyModel).GetMethod != null)
							return GetLinkTypeName((objStruct as PropertyModel).GetMethod.ReturnType);
						else if (objStruct is MethodModel)
							return GetLinkTypeName((objStruct as MethodModel).ReturnType);
						else
							return null;
					case "RETURNREMARKS":
						return Document.MLBuilder.GetSpan(objStruct.RemarksXml.Returns);
					default:
						return Document.MLBuilder.GetSpan("##No se encuentra " + strName + "##");
				}
		}

		/// <summary>
		///		Obtiene el valor de un argumento
		/// </summary>
		private MLNode GetArgumentValue(string strName, bool blnIncludeLink, LanguageStructModel objStruct, ArgumentModel objArgument)
		{ switch (strName.TrimIgnoreNull().ToUpper())
				{	case "NAME":
						return Document.MLBuilder.GetSpan(objArgument.Name);
					case "SUMMARY":
						return Document.MLBuilder.GetSpan(objStruct.RemarksXml.GetParameterRemarks(objArgument.Name));
					case "TYPE":
						if (blnIncludeLink)
							return GetLinkTypeName(objArgument.Type);
						else
							return Document.MLBuilder.GetSpan(objArgument.Type.Name);
					default:
						return Document.MLBuilder.GetSpan("##No se encuentra " + strName + "##");
				}
		}

		/// <summary>
		///		Obtiene el nodo del tipo base de una clase
		/// </summary>
		private MLNode GetLinkBaseType(LanguageStructModel objStruct, Boolean blnIncludeLink)
		{ MLNode objMLNode = new MLNode();

				// Si realmente es una clase
					if (objStruct is BaseComplexModel)
						{ BaseComplexModel objComplex = objStruct as BaseComplexModel;

								if (objStruct != null)
									{ if (blnIncludeLink)
											objMLNode = GetLinkTypeName(objComplex.BaseClass);
										else
											objMLNode = Document.MLBuilder.GetSpan(objComplex.BaseClass.Name);
									}
						}
				// Devuelve el nodo
					return objMLNode;
		}

		/// <summary>
		///		Obtiene un vínculo a un documento
		/// </summary>
		private MLNode GetLinkToDocument(LanguageStructModel objStruct, DocumentFileModel.SearchMode intMode)
		{	DocumentFileModel objDocument = Document.Childs.Search(objStruct);

				// Si se debe buscar también entre los padres, busca el documento
					objDocument = Document.Search(objStruct, intMode);
				// Devuelve el vínculo
					if (objDocument == null)
						return Document.MLBuilder.GetSpan(objStruct.Name);
					else
						return Document.MLBuilder.GetLink(objDocument.Name, objDocument.GetUrl(UrlBase));
		}

		/// <summary>
		///		Obtiene un vínculo (o no) a un espacio de nombres
		/// </summary>
		private MLNode GetLinkToNameSpace(LanguageStructModel objStruct, bool blnIncludeLink)
		{ LanguageStructModel objNameSpace = objStruct.GetNameSpace();

				// Obtiene el vínculo al espacio de nombres
					if (objNameSpace != null)
						{ if (blnIncludeLink)
								return GetLinkToDocument(objNameSpace, DocumentFileModel.SearchMode.Parent);
							else
								return Document.MLBuilder.GetSpan(objNameSpace.Name);
						}
					else 
						return Document.MLBuilder.GetSpan("");
		}

		/// <summary>
		///		Obtiene el tipo de estructura a partir de una cadena
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

		/// <summary>
		///		Obtiene los elementos de determinada estructura que se deben documentar
		/// </summary>
		private LanguageStructModelCollection SelectItemsForGeneration(LanguageStructModel objStruct, LanguageStructModel.StructType intIDType)
		{ LanguageStructModelCollection objColStructs = new LanguageStructModelCollection();

				// Obtiene las estructuras
					foreach (LanguageStructModel objChild in objStruct.Items)
						if (objChild.IDType == intIDType && Generator.Templates.MustGenerateDocumentation(objChild, Generator.DocumentationProcessor.Parameters))
							objColStructs.Add(objChild);
				// Ordena las estructuras por nombre
					objColStructs.SortByName();
				// Devuelve la colección
					return objColStructs;
		}

		/// <summary>
		///		Obtiene el nombre de un tipo de estructura
		/// </summary>
		private string GetStructTypeName(LanguageStructModel.StructType intIDType)
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
		///		Obtiene el texto del modificador
		/// </summary>
		private string GetModifierText(LanguageStructModel.ModifierType intIDModifier)
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
		///		Obtiene el prototipo de una estructura
		/// </summary>
		private MLNode GetPrototype(LanguageStructModel objStruct)
		{ MLNode objMLNode = Document.MLBuilder.GetSpan("");

				// Obtiene los datos del prototipo
					switch (objStruct.IDType)
						{	case LanguageStructModel.StructType.Property:
									objMLNode.Nodes.AddRange(GetPropertyPrototype(objStruct as PropertyModel));
								break;
							case LanguageStructModel.StructType.Constructor:
									objMLNode.Nodes.AddRange(GetMethodPrototype(objStruct as ConstructorModel));
								break;
							case LanguageStructModel.StructType.Method:
									MethodModel objMethod = objStruct as MethodModel;
										
										if (objMethod != null)
											objMLNode.Nodes.AddRange(GetMethodPrototype(objMethod, objMethod.IsAsync, objMethod.ReturnType));
								break;
						}
				// Devuelve el nodo
					return objMLNode;
		}

		/// <summary>
		///		Obtiene el prototipo de una propiedad
		/// </summary>
		private MLNodesCollection GetPropertyPrototype(PropertyModel objProperty)
		{ MLNodesCollection objColMLNodes = GetMethodPrototypeDefinition(objProperty, false, objProperty.GetMethod.ReturnType);

				// Argumentos
					if (objProperty.Arguments.Count > 0)
						{ objColMLNodes.Add(Document.MLBuilder.GetSpan("["));
							objColMLNodes.AddRange(GetArgumentsPrototype(objProperty));
							objColMLNodes.Add(Document.MLBuilder.GetSpan("]"));
						 }
				// Get y set
					objColMLNodes.Add(Document.MLBuilder.GetSpan("{"));
					objColMLNodes.AddRange(GetPropertyMethodPrototype(objProperty.Modifier, objProperty.GetMethod, "get"));
					objColMLNodes.AddRange(GetPropertyMethodPrototype(objProperty.Modifier, objProperty.SetMethod, "set"));
					objColMLNodes.Add(Document.MLBuilder.GetSpan("}"));
				// Devuelve el prototipo
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene el prototipo de una función
		/// </summary>
		private MLNodesCollection GetMethodPrototype(BaseMethodModel objMethod, bool blnIsAsync = false, TypedModel objReturnType = null)
		{	MLNodesCollection objColMLNodes = GetMethodPrototypeDefinition(objMethod, blnIsAsync, objReturnType);

				// Añade los argumentos
					objColMLNodes.Add(Document.MLBuilder.GetSpan("("));
					objColMLNodes.AddRange(GetArgumentsPrototype(objMethod));
					objColMLNodes.Add(Document.MLBuilder.GetSpan(")"));
				// Devuelve el prototipo
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene la definición (el prefijo) de un prototipo de un método
		/// </summary>
		private MLNodesCollection GetMethodPrototypeDefinition(BaseMethodModel objMethod, bool blnIsAsync, TypedModel objReturnType)
		{ MLNodesCollection objColMLNodes = new MLNodesCollection();
		
				// Añade los modificadores
					objColMLNodes.Add(Document.MLBuilder.GetSpan(GetModifierText(objMethod.Modifier)));
					if (blnIsAsync)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("async"));
					if (objMethod.IsAbstract)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("abstract"));
					if (objMethod.IsOverride)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("override"));
					if (objMethod.IsSealed)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("sealed"));
					if (objMethod.IsStatic)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("static"));
					if (objMethod.IsVirtual)
						objColMLNodes.Add(Document.MLBuilder.GetSpan("virtual"));
				// Añade el valor de retorno
					if (objReturnType != null)
						objColMLNodes.Add(GetLinkTypeName(objReturnType));
				// Añade el nombre del método
					objColMLNodes.Add(Document.MLBuilder.GetSpan(objMethod.Name));
				// Añade los genéricos
					objColMLNodes.AddRange(GetMethodPrototypeGenerics(objMethod.TypeParameters));
				// Devuelve la definición de prototipo
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene el prototipo de un método Get o Set de una propiedad
		/// </summary>
		private MLNodesCollection GetPropertyMethodPrototype(LanguageStructModel.ModifierType intModifier, MethodModel objMethod, string strMethodName)
		{	MLNodesCollection objColMLNodes = new MLNodesCollection();

				// Añade los datos al prototipo
					if (objMethod != null)
						{ // Añade el modificador (si es distinto al de la propiedad, es decir: public Property { private set }
								if (intModifier != objMethod.Modifier)
									objColMLNodes.Add(Document.MLBuilder.GetSpan(GetModifierText(objMethod.Modifier)));
							// Añade el nombre
								objColMLNodes.Add(Document.MLBuilder.GetSpan(strMethodName + ";"));
						}
				// Devuelve el prototipo
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene las definiciones de genéricos
		/// </summary>
		private MLNodesCollection GetMethodPrototypeGenerics(List<TypeParameterModel> objColTypeParameters)
		{ MLNodesCollection objColMLNodes = new MLNodesCollection();

				// Añade las definiciones de parámetros
					if (objColTypeParameters != null && objColTypeParameters.Count > 0)
						{ // Apertura
								objColMLNodes.Add(Document.MLBuilder.GetSpan("<"));
							// Añade los parámetros
								for (int intIndex = 0; intIndex < objColTypeParameters.Count; intIndex++)
									{ // Añade el nombre
											objColMLNodes.Add(Document.MLBuilder.GetSpan(objColTypeParameters[intIndex].Name));
										// Añade el separador
											if (intIndex < objColTypeParameters.Count - 1)
												objColMLNodes.Add(Document.MLBuilder.GetSpan(","));
									}
							// Cierre
								objColMLNodes.Add(Document.MLBuilder.GetSpan(">"));
						}
				// Devuelve los parámetros
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene los argumentos para un prototipo
		/// </summary>
		private MLNodesCollection GetArgumentsPrototype(BaseMethodModel objMethod)
		{ MLNodesCollection objColMLNodes = new MLNodesCollection();

				// Añade la cadena con los argumentos
					for (int intIndex = 0; intIndex < objMethod.Arguments.Count; intIndex++)
						{ // Añade los nodos de los argumentos
								objColMLNodes.AddRange(GetArgumentData(objMethod.Arguments[intIndex]));
							// Añade un nodo de separación
								if (intIndex < objMethod.Arguments.Count - 1)
									objColMLNodes.Add(Document.MLBuilder.GetSpan(","));
						}
				// Devuelve los argumentos
					return objColMLNodes;
		}

		/// <summary>
		///		Obtiene los datos de un argumento
		/// </summary>
		private MLNodesCollection GetArgumentData(ArgumentModel objArgument)
		{ MLNodesCollection objColMLSpans = new MLNodesCollection();

				// Añade los datos del argumento
					if (objArgument.IsThis)
 						objColMLSpans.Add(Document.MLBuilder.GetSpan("this"));
				// Añade el tipo de referencia
					switch (objArgument.RefType)
						{	case ArgumentModel.ArgumentType.ByOut:
									objColMLSpans.Add(Document.MLBuilder.GetSpan("out"));
								break;
							case ArgumentModel.ArgumentType.ByRef:
									objColMLSpans.Add(Document.MLBuilder.GetSpan("ref"));
								break;
						}
				// Añade el valor que indica si es un array de parámetros
					if (objArgument.IsParams)
						objColMLSpans.Add(Document.MLBuilder.GetSpan("params"));
				// Añade el tipo
					objColMLSpans.Add(GetLinkTypeName(objArgument.Type));
				// Añade el nombre del argumento
					objColMLSpans.Add(Document.MLBuilder.GetSpan(objArgument.Name));
				// Si tiene un valor por defecto, lo añade
					if (objArgument.IsOptional)
						{ // Añade el igual
								objColMLSpans.Add(Document.MLBuilder.GetSpan("="));
							// Añade el parámetro
								objColMLSpans.Add(Document.MLBuilder.GetSpan(objArgument.Default));
						}
				// Devuelve la cadena del argumento
					return objColMLSpans;
		}

		/// <summary>
		///		Obtiene el vínculo al nombre nombre del tipo
		/// </summary>
		private MLNode GetLinkTypeName(TypedModel objType)
		{ string strType = objType.Name;

				// Si se trata de un array, añade los datos
					if (objType.IsArray)
						strType = strType + "[]";
				// Devuelve la cadena del tipo
					return Document.MLBuilder.GetSearchLink(strType, objType.Name);
		}

		/// <summary>
		///		Generador
		/// </summary>
		internal ProgramDocumentationGenerator Generator { get; }

		/// <summary>
		///		Plantilla
		/// </summary>
		internal TemplateModel Template { get; }

		/// <summary>
		///		Documento
		/// </summary>
		internal DocumentFileModel Document { get; }

		/// <summary>
		///		Url base
		/// </summary>
		internal string UrlBase { get; }
	}
}
