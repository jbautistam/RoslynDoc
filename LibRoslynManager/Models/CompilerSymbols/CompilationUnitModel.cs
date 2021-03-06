﻿using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibRoslynManager.Models.CompilerSymbols
{
	/// <summary>
	///		Clase con los datos de una unidad de compilación
	/// </summary>
	public class CompilationUnitModel
	{
		public CompilationUnitModel(Solutions.ProjectVisualStudioModel objProject, string strFileName)
		{ FileName = strFileName;
			Project = objProject;
			Root = new Base.LanguageStructModel(Base.LanguageStructModel.StructType.CompilationUnit, null, this);
		}

		/// <summary>
		///		Obtiene los espacios de nombres de una unidad de compilación
		/// </summary>
		public List<Structs.NameSpaceModel> SearchNameSpaces()
		{ List<Structs.NameSpaceModel> objColNameSpaces = new List<Structs.NameSpaceModel>();

				// Obtiene los espacios de nombres
					foreach (Base.LanguageStructModel objStruct in Root.Items)
						if (objStruct is Structs.NameSpaceModel)
							objColNameSpaces.Add(objStruct as Structs.NameSpaceModel);
				// Devuelve la colección
					return objColNameSpaces;
		}

		/// <summary>
		///		Obtiene los elementos que no tienen espacios de nombres de una unidad de compilación
		/// </summary>
		public Base.LanguageStructModelCollection SearchNoNameSpaces()
		{ Base.LanguageStructModelCollection objColStructs = new Base.LanguageStructModelCollection();

				// Añade las estructuras que estén fuera del espacio de nombres a la colección
					foreach (Base.LanguageStructModel objStruct in Root.Items)
						if (objStruct != null && !(objStruct is Structs.NameSpaceModel))
							objColStructs.Add(objStruct);
				// Devuelve la estructura
					return objColStructs;
		}

		/// <summary>
		///		Proyecto origen
		/// </summary>
		public Solutions.ProjectVisualStudioModel Project { get; }

		/// <summary>
		///		Nombre del archivo
		/// </summary>
		public string FileName { get; }

		/// <summary>
		///		Elemento principal
		/// </summary>
		public Base.LanguageStructModel Root { get; }

		/// <summary>
		///		Cláusulas Using
		/// </summary>
		public List<string> UsingClauses { get; } = new List<string>();
	}
}