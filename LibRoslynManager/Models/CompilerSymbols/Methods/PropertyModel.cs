using System;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Methods
{
	/// <summary>
	///		Clase con los datos de una propiedad
	/// </summary>
	public class PropertyModel : BaseMethodModel
	{
		public PropertyModel(Base.LanguageStructModel objParent) : base(StructType.Property, objParent) {}

		/// <summary>
		///		Método Get de la propiedad
		/// </summary>
		public MethodModel GetMethod { get; internal set; }

		/// <summary>
		///		Método Set de la propiedad
		/// </summary>
		public MethodModel SetMethod { get; internal set; }

		/// <summary>
		///		Indica si es un indizador
		/// </summary>
		public bool IsIndexer { get; internal set; }

		/// <summary>
		///		Indica si es sólo lectura
		/// </summary>
		public bool IsReadOnly { get; internal set; }

		/// <summary>
		///		Indica si tiene eventos
		/// </summary>
		public bool IsWithEvents { get; internal set; }

		/// <summary>
		///		Indica si es sólo escritura
		/// </summary>
		public bool IsWriteOnly { get; internal set; }
	}
}
