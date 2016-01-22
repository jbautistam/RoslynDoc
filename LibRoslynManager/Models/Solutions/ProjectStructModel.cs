using System;

using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;

namespace Bau.Libraries.LibRoslynManager.Models.Solutions
{
	/// <summary>
	///		Estructura de lenguaje asociada a un proyecto
	/// </summary>
	public class ProjectStructModel
	{
		public ProjectStructModel(CompilerSymbols.Structs.NameSpaceModel objNameSpace)
		{ NameSpace = objNameSpace;
		}

		/// <summary>
		///		Espacio de nombres al que pertenece la estructura
		/// </summary>
		public CompilerSymbols.Structs.NameSpaceModel NameSpace { get; }

		/// <summary>
		///		Estructuras asociadas
		/// </summary>
		public LanguageStructModelCollection Structs { get; } = new LanguageStructModelCollection();
	}
}
