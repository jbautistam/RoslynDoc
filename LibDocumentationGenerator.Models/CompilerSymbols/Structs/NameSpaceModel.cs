﻿using System;

namespace Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Structs
{
	/// <summary>
	///		Modelo con los datos de un espacio de nombres
	/// </summary>
	public class NameSpaceModel : Base.LanguageStructModel
	{
		public NameSpaceModel(Base.LanguageStructModel objParent) : base(StructType.NameSpace, objParent) {}
	}
}
