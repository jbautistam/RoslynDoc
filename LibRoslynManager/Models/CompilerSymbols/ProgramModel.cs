using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols
{
	/// <summary>
	///		Clase con el modelo de documentación de un programa
	/// </summary>
	public class ProgramModel
	{
		/// <summary>
		///		Elementos de la aplicación
		/// </summary>
		public List<CompilationUnitModel> CompilationUnits { get; } = new List<CompilationUnitModel>();
	}
}
