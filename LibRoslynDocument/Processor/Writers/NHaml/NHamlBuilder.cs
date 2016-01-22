using System;

namespace Bau.Libraries.LibRoslynDocument.Processor.Writers.NHaml
{
	/// <summary>
	///		Builder de NHaml
	/// </summary>
	internal class NHamlBuilder
	{ // Variables privadas
			private System.Text.StringBuilder sbBuilder;
		
		public NHamlBuilder()
		{ Clear();
		}

		/// <summary>
		///		Limpia el contenido
		/// </summary>
		internal void Clear()
		{ sbBuilder = new System.Text.StringBuilder();
			Indent = 0;
		}

		/// <summary>
		///		Incrementa la indentación
		/// </summary>
		internal void AddIndent()
		{ Indent++;
		}

		/// <summary>
		///		Decrementa la indentación
		/// </summary>
		internal void RemoveIndent()
		{ Indent--;
			if (Indent < 0)
				Indent = 0;
		}

		/// <summary>
		///		Añade una etiqueta con su texto
		/// </summary>
		internal void AddTag(string strTag)
		{ AddTag(strTag, null);
		}

		/// <summary>
		///		Añade una etiqueta con su texto
		/// </summary>
		internal void AddTag(string strTag, string strText)
		{ // Añade la indentación
				if (Indent > 0)
					sbBuilder.Append(new string('\t', Indent));
			// Añade la etiqueta
				if (!strTag.StartsWith("%") && !strTag.StartsWith("<%"))
					sbBuilder.Append("%");
				sbBuilder.Append(strTag);
			// Añade el texto
				if (!string.IsNullOrEmpty(strText))
					sbBuilder.Append(" " + strText);
			// Añade un salto de línea
				sbBuilder.Append(Environment.NewLine);
		}
						
		/// <summary>
		///		Obtiene la cadena HTML
		/// </summary>
		public override string ToString()
		{ return sbBuilder.ToString();
		}

		/// <summary>
		///		Indentación
		/// </summary>
		internal int Indent { get; private set; }
	}
}
