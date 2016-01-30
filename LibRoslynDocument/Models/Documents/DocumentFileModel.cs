using System;
using System.Collections.Generic;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Models.Documents
{
	/// <summary>
	///		Clase con los datos de un archivo de documentación
	/// </summary>
	public class DocumentFileModel
	{ // Enumerados públicos
			/// <summary>
			///		Modo de búsqueda de un documento
			/// </summary>
			public enum SearchMode
				{ 
					/// <summary>Buscar por los elementos hijo</summary>
					Childs,
					/// <summary>Buscar por los elementos padre</summary>
					Parent,
					/// <summary>Buscar en los elementos hijo y padre</summary>
					All
				}

		public DocumentFileModel(DocumentFileModel objParent, LanguageStructModel objStruct, int intOrder)
		{ Parent = objParent;
			LanguageStruct = objStruct;
			if (objStruct != null)
				Name = objStruct.Name;
			Order = intOrder;
		}

		/// <summary>
		///		Obtiene la cadena de depuración
		/// </summary>
		internal string Debug(int intIndent)
		{ string strDebug = new string('\t', intIndent);

				// Añade los parámetros
					strDebug += Name + " (Order: " + Order + " Url: " + GetUrl("Documents") + ")";
				// Añade la documentación de los archivos
					return strDebug + Environment.NewLine + Childs.Debug(intIndent + 1);
		}

		/// <summary>
		///		Busca una estructura entre los documentos hijo o padre
		/// </summary>
		internal DocumentFileModel Search(LanguageStructModel objStruct, SearchMode intMode)
		{ DocumentFileModel objDocument = null;

				// Si estamos en el elemento buscado ...
					if (CheckContains(objStruct))
						objDocument = this;
				// Busca el documento
					if (objDocument == null && (intMode == SearchMode.All || intMode == SearchMode.Childs))
						objDocument = Childs.Search(objStruct);
					if (objDocument == null && (intMode == SearchMode.All || intMode == SearchMode.Parent))
						objDocument = SearchByParent(objStruct);
				// Devuelve el documento
					return objDocument;
		}

		/// <summary>
		///		Busca el documento por el padre
		/// </summary>
		internal DocumentFileModel SearchByParent(LanguageStructModel objStruct)
		{ if (CheckContains(objStruct))
				return this;
			else if (Parent != null)
				return Parent.SearchByParent(objStruct);
			else
				return null;
		}

		/// <summary>
		///		Comprueba si es te documento se refiere a la estructura buscada
		/// </summary>
		private bool CheckContains(LanguageStructModel objStruct)
		{ return StructType == objStruct.IDType && Order == objStruct.Order;
		}

		/// <summary>
		///		Transforma los vínculos de búsqueda de este documento
		/// </summary>
		internal void TransformSearchLinks(Dictionary<String, DocumentFileModel> dctLinks, string strPathBase)
		{ MLBuilder.TransformSeachLinks(this, dctLinks, strPathBase);
		}

		/// <summary>
		///		Obtiene la Url del documento
		/// </summary>
		internal string GetUrl(string strPathBase)
		{ if (Parent == null)
				return System.IO.Path.Combine(strPathBase, GetLastName(Name));
			else
				return System.IO.Path.Combine(Parent.GetUrl(strPathBase), GetLastName(Name));
		}

		/// <summary>
		///		Obtiene el directorio local del documento
		/// </summary>
		internal string GetPathLocal()
		{ if (Parent == null)
				return GetLastName(Name);
			else
				return System.IO.Path.Combine(Parent.GetPathLocal(), GetLastName(Name));
		}

		/// <summary>
		///		Obtiene el último nombre de una cadena del tipo x.y.z
		/// </summary>
		private string GetLastName(string strName)
		{ // Si es un espacio de nombres recoge el nombre completo, si no, recoge el final de la cadena
				if (StructType != LanguageStructModel.StructType.NameSpace)
					{	int intIndex = strName.IndexOf(".");
			
						// Corta a partir del punto
							while (intIndex > 0)
								{ strName = strName.Substring(intIndex + 1);
									intIndex = strName.IndexOf(".");
								}
						// Añade el orden si es necesario
							if (Order > 0)
								strName += "_" + Order.ToString();
					}
			// Devuelve el nombre de archivo
				return strName;
		}

		/// <summary>
		///		Archivo padre
		/// </summary>
		public DocumentFileModel Parent { get; private set; }

		/// <summary>
		///		Elementos hijo
		/// </summary>
		public DocumentFileModelCollection Childs { get; } = new DocumentFileModelCollection();

		/// <summary>
		///		Estructura principal documentada
		/// </summary>
		public LanguageStructModel LanguageStruct { get; set; }

		/// <summary>
		///		Estructuras añadidas al documento
		/// </summary>
		public LanguageStructModelCollection StructsReferenced { get; } = new LanguageStructModelCollection();

		/// <summary>
		///		Generador de archivos intermedios de documentación
		/// </summary>
		public Processor.Writers.MLIntermedialBuilder MLBuilder { get; } = new Processor.Writers.MLIntermedialBuilder();

		/// <summary>
		///		Nombre del elemento descrito
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Orden del elemento (para las sobrecargas)
		/// </summary>
		public int Order 
		{ get
				{ if (LanguageStruct == null)
						return 0;
					else
						return LanguageStruct.Order;
				}
			set
				{ if (LanguageStruct != null)
						LanguageStruct.Order = value;
				}
		}

		/// <summary>
		///		Tipo de estructura almacenada
		/// </summary>
		public LanguageStructModel.StructType StructType 
		{ get
				{ if (LanguageStruct == null)
						return LanguageStructModel.StructType.NameSpace;
					else
						return LanguageStruct.IDType;
				}
		}

		/// <summary>
		///		Indica si el documento representa un espacio de nombres real
		/// </summary>
		public bool IsRealNameSpace 
		{ get { return LanguageStruct != null && LanguageStruct.IDType == LanguageStructModel.StructType.NameSpace; }
		}
	}
}
