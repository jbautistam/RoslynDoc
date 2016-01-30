using System;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Structs;

namespace Bau.Libraries.LibRoslynDocument.Models.Groups
{
	/// <summary>
	///		Clase con los datos de un elemento documentado
	/// </summary>
	public class NameSpaceGroupModel
	{
		public NameSpaceGroupModel(NameSpaceModel objNameSpace, string strName)
		{ NameSpace = objNameSpace;
			Name = strName;
		}

		/// <summary>
		///		Nombre del espacio de nombres
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		///		Espacio de nombres asociado
		/// </summary>
		public NameSpaceModel NameSpace { get; internal set; }
	}
}
