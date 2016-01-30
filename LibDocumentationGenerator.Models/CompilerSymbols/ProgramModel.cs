using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols
{
	/// <summary>
	///		Clase con el modelo de documentación de un programa
	/// </summary>
	public class ProgramModel
	{
		/// <summary>
		///		Obtiene la cadena de depuración
		/// </summary>
		public string Debug()
		{ string strDebug = "";

				// Añade las cadenas de depuración de las unidades de compilación
					foreach (CompilationUnitModel objCompilation in CompilationUnits)
						strDebug += objCompilation.Debug() + Environment.NewLine;
				// Devuelve la cadena de depuración
					return strDebug;
		}

		/// <summary>
		///		Elementos de la aplicación
		/// </summary>
		public List<CompilationUnitModel> CompilationUnits { get; } = new List<CompilationUnitModel>();
	}
}
