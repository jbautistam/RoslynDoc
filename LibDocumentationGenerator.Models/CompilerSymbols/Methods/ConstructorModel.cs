﻿using System;

namespace Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Methods
{
	/// <summary>
	///		Clase con los datos de un constructor
	/// </summary>
	public class ConstructorModel : BaseMethodModel
	{
		public ConstructorModel(Base.LanguageStructModel objParent) : base(StructType.Constructor, objParent) {}
	}
}
