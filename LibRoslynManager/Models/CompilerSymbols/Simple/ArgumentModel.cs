using System;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Simple
{
	/// <summary>
	///		Modelo con los datos de un argumento
	/// </summary>
	public class ArgumentModel : BaseParameterModel
	{
		/// <summary>
		///		Tipo de argumento
		/// </summary>
		public enum  ArgumentType
			{ 
				/// <summary>Por valor</summary>
				ByValue,
				/// <summary>Por referencia</summary>
				ByRef,
				/// <summary>De salida</summary>
				ByOut
			}

		/// <summary>
		///		Cadena de depuración
		/// </summary>
		public override string Debug(int intIndent)
		{ string strDebug = base.Debug(intIndent);

				// Añade los parámetros
					strDebug += " RefType: " + RefType.ToString() + " Default: " + Default;
				// Devuelve la cadena de depuración
					return strDebug + Environment.NewLine;
		}

		/// <summary>
		///		Referencia
		/// </summary>
		public ArgumentType RefType { get; internal set; }

		/// <summary>
		///		Indica si el argumento es opcional
		/// </summary>
		public bool IsOptional { get; internal set; }

		/// <summary>
		///		Indica si el argumento es una lista de parámetros
		/// </summary>
		public bool IsParams { get; internal set; }

		/// <summary>
		///		Indica si el argumento se refiere al objeto this
		/// </summary>
		public bool IsThis { get; internal set; }

		/// <summary>
		///		Valor predeterminado del elemento
		/// </summary>
		public string Default { get; internal set; }
	}
}
