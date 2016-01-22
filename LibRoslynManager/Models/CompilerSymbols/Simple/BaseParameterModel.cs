using System;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Simple
{
	/// <summary>
	///		Clase base con los datos de un parámetro / variable
	/// </summary>
	public class BaseParameterModel : Base.LanguageBaseModel
	{
		/// <summary>
		///		Orden del argumento en la lista
		/// </summary>
		public int Order { get; internal set; }

		/// <summary>
		///		Tipo del elemento
		/// </summary>
		public Base.TypedModel Type { get; internal set; } = new Base.TypedModel();
	}
}
