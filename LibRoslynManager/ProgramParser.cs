using System;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols;
using Bau.Libraries.LibRoslynManager.Parser.Solutions;

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
							objProgram.CompilationUnits.Add(objParser.ParseFile(objFile.FullFileName));
				// Devuelve el programa interpretado
					return objProgram;
		}

		/// <summary>
		///		Interpreta el contenido de un texto
		/// </summary>
		public ProgramModel ParseText(string strText)
		{ ProgramModel objProgram = new ProgramModel();
				
				// Interpreta el texto
					objProgram.CompilationUnits.Add(new Parser.FileParser().ParseText(strText));
				// Devuelve el programa
					return objProgram;
		}
	}
}