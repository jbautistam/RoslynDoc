using System;

using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Base;
using Bau.Libraries.LibRoslynManager.Models.CompilerSymbols.Structs;
using Bau.Libraries.LibRoslynManager.Models.Solutions;

namespace Bau.Libraries.LibRoslynManager
{
	/// <summary>
	///		Intérprete de un programa
	/// </summary>
	public class ProgramParser
	{	
		/// <summary>
		///		Interpreta una solución
		/// </summary>
		public ProgramModel ParseSolution(string strFileName)
		{ SolutionVisualStudioModel objSolution = new SolutionVisualStudioModel(strFileName);
			ProgramModel objProgram = new ProgramModel();
			Parser.FileParser objParser = new Parser.FileParser();

				// Carga la solución
					objSolution.Load();
				// Interpreta los proyectos
					foreach (ProjectVisualStudioModel objProject in objSolution.Projects)
						foreach (FileVisualStudioModel objFile in objProject.Files)
							objProgram.CompilationUnits.Add(objParser.ParseFile(objProject, objFile.FullFileName));
				// Corrige los espacios de nombres
					CorrectNameSpaces(objProgram);
				// Devuelve el programa interpretado
					return objProgram;
		}

		/// <summary>
		///		Interpreta el contenido de un texto
		/// </summary>
		public ProgramModel ParseText(string strText)
		{ ProgramModel objProgram = new ProgramModel();
				
				// Interpreta el texto
					objProgram.CompilationUnits.Add(ParseUnit(strText));
				// Devuelve el programa
					return objProgram;
		}

		/// <summary>
		///		Interpreta una unidad de compilación
		/// </summary>
		private CompilationUnitModel ParseUnit(string strText)
		{ return new Parser.FileParser().ParseText(strText);
		}

		/// <summary>
		///		Corrige los espacios de nombres de las unidades de compilación
		/// </summary>
		/// <remarks>
		///		Al interpretar desde varios proyectos como unidades de compilación por separado en cada fichero, Roslyn
		///	no puede averiguar el espacio de nombres de las clase base, por eso, se buscan los espacios de nombres donde
		///	están definidas las clases, estructuras e interfaces y se buscan esos tipos en todas las clases
		/// </remarks>
		private void CorrectNameSpaces(ProgramModel objProgram)
		{ // Rellena las estructuras de proyecto
				FillProjectStructs(objProgram);
		}

		/// <summary>
		///		Rellena las estructuras de proyecto de una aplicación
		/// </summary>
		private void FillProjectStructs(ProgramModel objProgram)
		{	foreach (CompilationUnitModel objCompilationUnit in objProgram.CompilationUnits)
				foreach (LanguageStructModel objStruct in objCompilationUnit.Root.Items)
					if (objStruct.IDType == LanguageStructModel.StructType.NameSpace)
						{ NameSpaceModel objNameSpace = objStruct as NameSpaceModel;

								if (objNameSpace != null)
									FillProjectStructs(objCompilationUnit.Project, objStruct as NameSpaceModel, objStruct.Items);	
						}
		}

		/// <summary>
		///		Rellena las estructuras de un espacio de nombres de un proyecto
		/// </summary>
		private void FillProjectStructs(ProjectVisualStudioModel objProject, NameSpaceModel objNameSpace, LanguageStructModelCollection objColItems)
		{ foreach (LanguageStructModel objItem in objColItems)
				{ // Añade la estructura al proyecto
						if (objItem.IDType == LanguageStructModel.StructType.Class || objItem.IDType == LanguageStructModel.StructType.Interface ||
								objItem.IDType == LanguageStructModel.StructType.Struct || objItem.IDType == LanguageStructModel.StructType.Enum)
							objProject.Structs.Add(objNameSpace, objItem);
					// Añade las estructuras hija
						FillProjectStructs(objProject, objNameSpace, objItem.Items);
				}
		}
	}
}