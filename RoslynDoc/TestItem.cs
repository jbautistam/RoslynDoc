using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols;
using Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.Base;

/// <summary>
///		Clase sin espacio de nombres
/// </summary>
public class NoSpaceClass : Bau.Libraries.LibDocumentationGenerator.Models.CompilerSymbols.CompilationUnitModel
{
	public NoSpaceClass(string strFileName) : base(strFileName) {}

	/// <summary>
	///		Método de una clase sin espacio de nombres
	/// </summary>
	public void Method()
	{
	}
}

/// <summary>
///		Estos son los comentarios del espacio de nombres
/// </summary>
namespace RoslynDoc.Classes
{
	/// <summary>
	///		Esta es la documentación de la clase
	/// </summary>
	/// <remarks>
	///		Estos son los comentarios adicionales de la clase.
	///		Separados en párrafos para que se pueda ver
	///		Si realmente el constructor los añade como párrafos independientes.
	/// </remarks>
	public class TestItem : LanguageStructModel, IDisposable
	{
		/// <summary>
		///		Un enumerado
		/// </summary>
		public enum EnumType
			{ 
				/// <summary>
				/// Desconocido
				/// </summary>
				Unknown,
				/// <summary>
				/// Primer elemento
				/// </summary>
				Primero,
				/// <summary>
				/// Segundo elemento
				/// </summary>
				Segundo
			}

		/// <summary>
		///		Estructura
		/// </summary>
		public struct StructSampleType : IDisposable
			{
				/// <summary>
				///		Un elemento entero para las estructuras
				/// </summary>
				public int StrutItem1;

				/// <summary>
				///		Un elemento de cadena para la estructura
				/// </summary>
				public string StructItem2;

				/// <summary>
				///		Un elemento privado de cadena para la estructura
				/// </summary>
				private string StructItem3;

				/// <summary>
				///		Implementa la interface IDisposable
				/// </summary>
				public void Dispose()
				{
				}

				/// <summary>
				///		Método de la estructura
				/// </summary>
				public int Method(int a, string b)
				{ StructItem3 = b;
					return a;
				}

				/// <summary>
				///		Propiedad de la estructura
				/// </summary>
				public int Property { get; set; }
		}

		/// <summary>
		///		Este es el constructor
		/// </summary>
		public TestItem() : base(StructType.Class, null)
		{
		}

		/// <summary>
		///		Método público
		/// </summary>
		/// <remarks>
		///		con sus comentarios adicionales
		///	</remarks>
		public void MethodPublic()
		{
		}

		/// <summary>
		///		Método privado
		/// </summary>
		private int MethodPrivate(int a = 3)
		{ return 0;
		}

		/// <summary>
		///		Método con argumentos
		/// </summary>
		private static int MethodPrivateAndStatic(string[] arrStrStrings, int a = 3, params int[] arrInt)
		{ return 25;
		}

		/// <summary>
		///		Método privado
		/// </summary>
		private LanguageStructModel MethodStruct()
		{ return new LanguageStructModel(StructType.Class, null);
		}

		/// <summary>
		///		Método con argumentos
		/// </summary>
		public int MethodWithArguments(int a, ref bool b, out string c)
		{ c = "";
			return 0;
		}

		/// <summary>
		///		Aquí está el resumen del método
		/// </summary>
		/// <param name="a">Aquí la explicación del parámetro a</param>
		/// <param name="b">Aquí la explicación del parámetro b</param>
		/// <param name="c">Aquí la explicación del parámetro c
		/// con un montón de valores
		/// para ver cómo salta las líneas
		/// </param>
		/// <returns>Y también tenemos el comentario de los elementos devueltos</returns>
		public int MethodWithArgumentsAndRemarks(int a, ref bool b, out string c)
		{ c = "";
			return 0;
		}

		/// <summary>
		///		Un método genérico
		/// </summary>
		/// <typeparam name="TypeData">Tipo del método genérico</typeparam>
		/// <param name="a">Nombre del parámetro a</param>
		/// <param name="b">Nombre del parámetro b</param>
		/// <returns>Valor de retorno</returns>
		public int MethodGeneric<TypeData>(TypeData a, int b) where TypeData : TestItem
		{
			return 3;
		}

		/// <summary>
		///		Un método con varios genéricos
		/// </summary>
		/// <typeparam name="TypeData">Tipo del método genérico</typeparam>
		/// <param name="a">Nombre del parámetro a</param>
		/// <param name="b">Nombre del parámetro b</param>
		/// <returns>Valor de retorno</returns>
		public TypeData MethodGeneric<TypeData, TypeCollection>(TypeData a, TypeCollection b, int c) where TypeData : LanguageBaseModel
		{
			return a;
		}

		/// <summary>
		///		Método genérico pero sin restricciones de tipo
		/// </summary>
		public TypeData MethodGenericNoConstraints<TypeData>(TypeData a)
		{ return a;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		///		Propiedad pública
		/// </summary>
		public int PropertyPublic { get; set; }

		/// <summary>
		///		Propiedad privada
		/// </summary>
		private int PropertyPrivate { get; set; }

		/// <summary>
		///		Propiedad mixta (interna con el set privado y el get público)
		/// </summary>
		internal int PropertyMixed { get; private set; }
	}
}
