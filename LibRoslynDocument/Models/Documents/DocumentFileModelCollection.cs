using System;
using System.Collections.Generic;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynDocument.Models.Documents
{
	/// <summary>
	///		Colección de <see cref="DocumentFileModel"/>
	/// </summary>
	public class DocumentFileModelCollection : List<DocumentFileModel>
	{
		/// <summary>
		///		Obtiene la cadena de depuración
		/// </summary>
		public string Debug(int intIndent)
		{ string strDebug = "";

				// Crea la cadena de depuración
					foreach (DocumentFileModel objDocument in this)
						strDebug += objDocument.Debug(intIndent);
				// Devuelve la cadena de depuración
					return strDebug;
		}

		/// <summary>
		///		Comprueba si existen elementos de un tipo determinado
		/// </summary>
		internal bool ExistsItems(LanguageStructModel.StructType intIDType)
		{ // Recorre los elementos
				foreach (DocumentFileModel objDocument in this)
					if (objDocument.StructType == intIDType)
						return true;
			// Si ha llegado hasta aquí es porque no existen elementos
				return false;
		}

		/// <summary>
		///		Comprueba si existe un espacio de nombres real en la colección
		/// </summary>
		internal bool ExistsRealNameSpace()
		{ // Recorre la colección
				foreach (DocumentFileModel objDocument in this)
					if (objDocument.IsRealNameSpace)
						return true;
			// Si ha llegado hasta aquí es porque no ha encontrado un espacio de nombres real
				return false;
		}

		/// <summary>
		///		Ordena los documentos por nombres
		/// </summary>
		internal void SortByName()
		{ // Ordena los elementos hijo
				foreach (DocumentFileModel objDocument in this)
					objDocument.Childs.SortByNameInner();
			// Ordena los elementos
				SortByNameInner();
		}

		/// <summary>
		///		Obtiene el orden de un elemento
		/// </summary>
		internal int SearchOrder(LanguageStructModel objItem)
		{ int intOrder = 0;

				// Cuenta los elementos que tengan el mismo nombre
					foreach (DocumentFileModel objDocument in this)
						if (objItem.Name.EqualsIgnoreCase(objDocument.Name))
							intOrder++;
				// Devuelve el orden
					return intOrder;
		}

		/// <summary>
		///		Ordena los documentos por nombres (rutina interna)
		/// </summary>
		private void SortByNameInner()
		{ Sort((objFirst, objSecond) => objFirst.Name.CompareTo(objSecond.Name));
		}
	}
}
