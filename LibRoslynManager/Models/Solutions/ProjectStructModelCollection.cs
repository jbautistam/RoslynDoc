using System;
using System.Linq;

using Bau.Libraries.LibHelper.Extensors;

namespace Bau.Libraries.LibRoslynManager.Models.Solutions
{
	/// <summary>
	///		Colección de <see cref="ProjectStructModel"/>
	/// </summary>
	public class ProjectStructModelCollection : System.Collections.Generic.List<ProjectStructModel>
	{
		/// <summary>
		///		Añade una estructura a la colección
		/// </summary>
		public void Add(CompilerSymbols.Structs.NameSpaceModel objNameSpace, CompilerSymbols.Base.LanguageStructModel objStruct)
		{ ProjectStructModel objProjectStruct = Search(objNameSpace);

				// Crea la estructura si no existía
					if (objProjectStruct == null)
						{ // Crea el objeto
								objProjectStruct = new ProjectStructModel(objNameSpace);
							// Añade el objeto a la colección
								Add(objProjectStruct);
						}
				// Le añade la estructura
					if (!objProjectStruct.Structs.ExistsByName(objStruct))
						objProjectStruct.Structs.Add(objStruct);
		}

		/// <summary>
		///		Obtiene una estructura
		/// </summary>
		public ProjectStructModel Search(CompilerSymbols.Structs.NameSpaceModel objNameSpace)
		{ return this.FirstOrDefault(objStruct => objStruct.NameSpace.Name.EqualsIgnoreCase(objNameSpace.Name));
		}
	}
}
