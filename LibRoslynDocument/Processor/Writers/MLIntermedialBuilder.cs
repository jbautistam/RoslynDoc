using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers
{
	/// <summary>
	///		Clase interna de generación en nodos ML
	/// </summary>
	public class MLIntermedialBuilder
	{ // Constantes privadas
			private const string cnstStrTagRoot = "Root";
			private const string cnstStrTagSpan = "Span";
			private const string cnstStrTagBold = "IsBold";
			private const string cnstStrTagItalic = "IsItalic";
			private const string cnstStrTagLink = "Link";
			private const string cnstStrTagHref = "Ref";

		/// <summary>
		///		Limpia el constructor
		/// </summary>
		public void Clear()
		{ Root = new MLNode();
		}

		/// <summary>
		///		Comprueba si un nodo tiene hijos "span"
		/// </summary>
		public bool CheckContainSpan(MLNode objMLNode)
		{ // Comprueba los hijos
				foreach (MLNode objMLChild in objMLNode.Nodes)
					if (CheckIsSpanNode(objMLChild) || CheckIsLinkNode(objMLChild))
						return true;
			// Si ha llegado hasta aquí, no es span
				return false;
		}

		/// <summary>
		///		Comprueba si un nodo está vacío
		/// </summary>
		public bool CheckIsEmpty(MLNode objMLNode)
		{ return objMLNode.Nodes.Count == 0 && objMLNode.Value.IsEmpty();
		}

		/// <summary>
		///		Comprueba si un nodo es complejo
		/// </summary>
		public bool CheckIsComplex(MLNode objMLNode)
		{ return objMLNode.Nodes.Count > 0;
			//string [] arrStrComplex = { "ul", "table", "tr", "li" };

			//	// Comprueba si es complejo
			//		foreach (string strComplex in arrStrComplex)
			//			if (strComplex.EqualsIgnoreCase(objMLNode.Name))
			//				return true;
			//	// Si ha llegado hasta aquí es porque no era complejo
			//		return false;
		}

		/// <summary>
		///		Comprueba si es un nodo de span
		/// </summary>
		public bool CheckIsSpanNode(MLNode objMLNode)
		{ return objMLNode.Name == MLIntermedialBuilder.cnstStrTagSpan;
		}

		/// <summary>
		///		Comprueba si es un nodo de span
		/// </summary>
		public bool CheckIsLinkNode(MLNode objMLNode)
		{ return objMLNode.Name == MLIntermedialBuilder.cnstStrTagLink;
		}

		/// <summary>
		///		Comprueba si un nodo de span está definido como negrita
		/// </summary>
		public bool CheckIsBold(MLNode objMLNode)
		{ return objMLNode.Attributes[cnstStrTagBold].Value.GetBool();
		}

		/// <summary>
		///		Comprueba si un nodo de span está definido como cursiva
		/// </summary>
		public bool CheckIsItalic(MLNode objMLNode)
		{ return objMLNode.Attributes[cnstStrTagItalic].Value.GetBool();
		}

		/// <summary>
		///		Obtiene un span
		/// </summary>
		public MLNode GetSpan(string strText, bool blnBold = false, bool blnItalic = false)
		{ MLNode objSpan = new MLNode(cnstStrTagSpan, strText);

				// Añade los atributos
					objSpan.Attributes.Add(cnstStrTagBold, blnBold);
					objSpan.Attributes.Add(cnstStrTagItalic, blnItalic);
				// Devuelve el nodo
					return objSpan;
		}

		/// <summary>
		///		Obtiene un nodo para un listItem con una serie de span
		/// </summary>
		public MLNode GetListItem(params MLNode[] arrMLSpan)
		{ MLNode objMLNode = new MLNode("li");

				// Añade los elementos
					foreach (MLNode objMLSpan in arrMLSpan)
						objMLNode.Nodes.Add(objMLSpan);
				// Devuelve el nodo
					return objMLNode;
		}

		/// <summary>
		///		Obtiene un vínculo
		/// </summary>
		public MLNode GetLink(string strTitle, string strUrl)
		{	MLNode objMLNode = new MLNode(cnstStrTagLink, strTitle);

				// Añade la referencia
					objMLNode.Attributes.Add(cnstStrTagHref, strUrl);
				// Devuelve el nodo
					return objMLNode;
		}

		/// <summary>
		///		Obtiene el valor del atributo href de un hipervínculo
		/// </summary>
		public string GetHref(MLNode objMLNode)
		{ return objMLNode.Attributes[cnstStrTagHref].Value;
		}

		/// <summary>
		///		Añade una cabecera a la tabla
		/// </summary>
		public MLNode AddTable(MLNode objMLRoot, params string[] arrStrHeaders)
		{ MLNode objMLTable = objMLRoot.Nodes.Add("table");

				// Añade la fila
					AddRowTable(objMLTable, true, arrStrHeaders);
				// Devuelve el nodo de tabla
					return objMLTable;
		}

		/// <summary>
		///		Añade las celdas de una fila a la tabla
		/// </summary>
		public void AddRowTable(MLNode objMLTable, params string[] arrStrCells)
		{ AddRowTable(objMLTable, false, arrStrCells);
		}

		/// <summary>
		///		Añade las celdas de una fila a la tabla
		/// </summary>
		private void AddRowTable(MLNode objMLTable, bool blnIsHeader, params string[] arrStrCells)
		{ MLNode objMLRow = objMLTable.Nodes.Add("tr");

				// Añade las celdas
					foreach (string strCell in arrStrCells)
						if (blnIsHeader)
							objMLRow.Nodes.Add("th", strCell);
						else
							objMLRow.Nodes.Add("td", strCell);
		}

		/// <summary>
		///		Añade una celda a una fila
		/// </summary>
		internal MLNode GetCell(MLNode objMLRow, string strText, int intColSpan = 1)
		{ MLNode objMLCell = objMLRow.Nodes.Add("td", strText);

				// Le añade el ColSpan
					AddColSpan(objMLCell, intColSpan);
				// Devuelve el nodo
					return objMLCell;
		}

		/// <summary>
		///		Añade una celda con un nodo a una fila
		/// </summary>
		internal MLNode GetCell(MLNode objMLRow, MLNode objMLNode, int intColSpan = 1)
		{ MLNode objMLCell = objMLRow.Nodes.Add("td");

				// Añade el nodo
					objMLCell.Nodes.Add(objMLNode);
				// Añade el ColSpan
					AddColSpan(objMLCell, intColSpan);
				// Devuelve el nodo
					return objMLCell;
		}

		/// <summary>
		///		Añade una fila con un nodo a una tabla
		/// </summary>
		internal void AddRowNode(MLNode objMLTable, MLNode objMLNode, int intEmptyCells, int intColSpan)
		{ MLNode objMLRow = objMLTable.Nodes.Add("tr");

				// Añade las celdas vacías
					AddEmptyCells(objMLRow, intEmptyCells);
				// Añade una celda con un nodo
					GetCell(objMLRow, objMLNode, intColSpan);
		}

		/// <summary>
		///		Añade una fila con una serie de nodos a una tabla
		/// </summary>
		internal void AddRowNode(MLNode objMLTable, MLNodesCollection objColMLNodes, int intEmptyCells, int intColSpan)
		{ MLNode objMLRow = objMLTable.Nodes.Add("tr");
			MLNode objMLCell;

				// Añade las celdas vacías
					AddEmptyCells(objMLRow, intEmptyCells);
				// Añade la celda con los nodos
					objMLCell = GetCell(objMLRow, "", intColSpan);
					foreach (MLNode objMLNode in objColMLNodes)
						objMLCell.Nodes.Add(objMLNode);
		}

		/// <summary>
		///		Añade celdas vacías a un nodo
		/// </summary>
		internal void AddEmptyCells(MLNode objMLRow, int intEmptyCells)
		{	for (int intIndex = 0; intIndex < intEmptyCells; intIndex++)
				GetCell(objMLRow, "");
		}

		/// <summary>
		///		Añade un ColSpan a una celda
		/// </summary>
		private void AddColSpan(MLNode objMLCell, int intColSpan)
		{	if (intColSpan != 1)
				objMLCell.Attributes.Add("colspan", intColSpan);
		}

		/// <summary>
		///		Nodo raíz
		/// </summary>
		public MLNode Root { get; private set; } = new MLNode();
	}
}
