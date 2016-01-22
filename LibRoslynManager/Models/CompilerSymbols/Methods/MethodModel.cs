using System;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Methods
{
	/// <summary>
	///		Clase con los datos de un método
	/// </summary>
	public class MethodModel : BaseMethodModel
	{
		public MethodModel(Base.LanguageStructModel objParent) : base(StructType.Method, objParent) {}

		/// <summary>
		///		Obtiene la cadena de depuración
		/// </summary>
		public override string Debug(int intIndent)
		{ string strDebug = base.Debug(intIndent);

				// Añade los datos del tipo de retorno
					return ReturnType.Debug() + Environment.NewLine + base.DebugArguments(intIndent);
		}

		/// <summary>
		///		Indica si el método es asíncrono
		/// </summary>
		public bool IsAsync { get; internal set; }

		/// <summary>
		///		Indica si es un método checked
		/// </summary>
		public bool IsCheckedBuiltin { get; internal set; }

		/// <summary>
		///		Indica si es una definición
		/// </summary>
		public bool IsDefinition { get; internal set; }

		/// <summary>
		///		Indica si es un método de extensión
		/// </summary>
		public bool IsExtensionMethod { get; internal set; }

		/// <summary>
		///		Indica si es externo
		/// </summary>
		public bool IsExtern { get; internal set; }

		/// <summary>
		///		Indica si es un método genérico
		/// </summary>
		public bool IsGenericMethod { get; internal set; }

		/// <summary>
		///		Indica si se declara implícitamente
		/// </summary>
		public bool IsImplicitlyDeclared { get; internal set; }

		/// <summary>
		///		Tipo de retorno
		/// </summary>
		public Base.TypedModel ReturnType { get; internal set; } = new Base.TypedModel();
	}
}
