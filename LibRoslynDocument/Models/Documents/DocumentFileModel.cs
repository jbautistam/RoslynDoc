using System;

using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Models.Documents
{
	/// <summary>
	///		Clase con los datos de un archivo de documentación
	/// </summary>
	public class DocumentFileModel
	{ 
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
		///		Archivo padre
		/// </summary>
		public DocumentFileModel Parent { get; private set; }

		/// <summary>
		///		Elementos hijo
		/// </summary>
		public DocumentFileModelCollection Childs { get; } = new DocumentFileModelCollection();

		/// <summary>
		///		Estructura documentada
		/// </summary>
		public LanguageStructModel LanguageStruct { get; set; }

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
		///		Nombre del elemento descrito
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Orden del elemento (para las sobrecargas
		/// </summary>
		public int Order { get; set; }

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
