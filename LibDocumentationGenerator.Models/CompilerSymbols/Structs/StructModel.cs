using System;

namespace Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Structs
{
	/// <summary>
	///		Clase con los datos de una estructura
	/// </summary>
	public class StructModel : BaseComplexModel
	{
		public StructModel(Base.LanguageStructModel objParent) : base(StructType.Struct, objParent) {}
	}
}
