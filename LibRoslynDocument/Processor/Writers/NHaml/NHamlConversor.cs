using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers.NHaml
{
	/// <summary>
	///		Conversor a NHaml
	/// </summary>
	public class NHamlConversor
	{ // Variables privadas
			private NHamlBuilder objBuilder = new NHamlBuilder();

		/// <summary>
		///		Convierte una serie de nodos XML en una cadena NHaml
		/// </summary>
		internal string Convert(MLIntermedialBuilder objMLBuilder)
		{ // Guarda el generador
				MLBuilder = objMLBuilder;
			// Limpia el contenido
				objBuilder.Clear();
			// Convierte los nodos
				foreach (MLNode objMLNode in MLBuilder.Root.Nodes)
					ConvertNode(objMLNode);
			// Devuelve la cadena
				return objBuilder.ToString();
		}

		/// <summary>
		///		Convierte los nodos
		/// </summary>
		private void ConvertNode(MLNode objMLNode)
		{	if (MLBuilder.CheckIsEmpty(objMLNode))
				AddNodeTag(objMLNode, null);
			else if (MLBuilder.CheckContainSpan(objMLNode))
				AddNodeTag(objMLNode, GetSpansText(objMLNode));
			else if (MLBuilder.CheckIsComplex(objMLNode))
				{ // Crea la etiqueta y le añade la indentación
						AddNodeTag(objMLNode, objMLNode.Value);
						objBuilder.AddIndent();
					// Crea los nodos hijo
						foreach (MLNode objMLChild in objMLNode.Nodes)
							ConvertNode(objMLChild);
					// Quita la indentación
						objBuilder.RemoveIndent();
				}
			else
				AddNodeTag(objMLNode, objMLNode.Value);
		}

		/// <summary>
		///		Añade una etiqueta de un nodo con sus atributos
		/// </summary>
		private void AddNodeTag(MLNode objMLNode, string strText)
		{ string strTag = "%" + objMLNode.Name;

				// Añade los atributos
					strTag += GetAttributes(objMLNode);
				// Añade el texto al generador
					objBuilder.AddTag(strTag, strText);
		}

		/// <summary>
		///		Obtiene los atributos de un nodo formateados para NHaml
		/// </summary>
		private string GetAttributes(MLNode objMLNode)
		{ string strAttributes = "";

				// Añade los atributos del nodo
					foreach (MLAttribute objMLAttribute in objMLNode.Attributes)
						strAttributes = strAttributes.AddWithSeparator(objMLAttribute.Name + " = \"" + objMLAttribute.Value + "\"", " ", false);
				// Añade las llaves
					if (!strAttributes.IsEmpty())
						strAttributes = " {" + strAttributes + " }";
				// Devuelve los atributos
					return strAttributes;
		}

		/// <summary>
		///		Obtiene el texto de los spans de un nodo
		/// </summary>
		private string GetSpansText(MLNode objMLNode)
		{ string strText = "";

				// Crea el texto a partir de los nodos span
					foreach (MLNode objMLChild in objMLNode.Nodes)
						if (MLBuilder.CheckIsSpanNode(objMLChild))
							strText = strText.AddWithSeparator(ConvertSpanText(objMLChild), " ", false);
						else if (MLBuilder.CheckIsLinkNode(objMLChild))
							strText = strText.AddWithSeparator(ConvertLink(objMLChild), " ", false);
						else
							strText = strText.AddWithSeparator(objMLChild.Value, " ", false);
				// Devuelve el texto
					return strText;
		}

		/// <summary>
		///		Convierte el texto del span
		/// </summary>
		private string ConvertSpanText(MLNode objMLNode)
		{ string strText = objMLNode.Value;

				// Añade el texto que indica si está en negrita o en cursiva
					if (MLBuilder.CheckIsBold(objMLNode))
						strText = "#b " + strText + "#";
					if (MLBuilder.CheckIsItalic(objMLNode))
						strText = "#em " + strText + "#";
				// Devuelve el texto
					return strText;
		}

		/// <summary>
		///		Convierte el hipervínculo de un nodo
		/// </summary>
		private string ConvertLink(MLNode objMLNode)
		{ return string.Format("#a {{ href = \"{0}\" }} {1} #", MLBuilder.GetHref(objMLNode), objMLNode.Value);
		}

		/// <summary>
		///		Generador
		/// </summary>
		private MLIntermedialBuilder MLBuilder { get; set; }
	}
}
