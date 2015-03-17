import java.util.Arrays;
import java.util.Iterator;
import java.util.Stack;

public class Algorithm
{
	/*
	 * Префиксы:
	 * _a - алгоритм или арифметика
	 * _g - геометрия
	 * _s - статистика
	 * _m - массивы и матрицы
	 */
		
//--------------------------------------------------------------------------------------//	
	/*Блок алгоритмов и арифметики*/
	
	/*
	 * алгоритм Евклида нахождения наибольшего общего делителя 2 неотрицательных целых чисел
	 */
	
	public static int _aEvklid(int a, int b)
	{
		if(b == 0) 
			return a;
		int r = a % b;
		return _aEvklid(b, r);
	}
	
	/*
	 * Проверка, простое ли число
	 */
	
	public static boolean _aIsPrime(int n)
	{
		if(n < 2)
			return false;
		for(int i = 2; i*i <= n; i++)
			if(n % i == 0) return false;
		return true;	
	}
	
	/*
	 * Гармоническое число
	 */
	
	public static double _aHarmonic(int n)
	{
		double dSum = 0.0;
		for(int i = 1; i < n; i++)
			dSum += 1.0 / i;
		return dSum;
	}
	
	/*
	 * Случайное целое число из диапозона [0, n)
	 */
	
	public static int _aRandomInt(int n)
	{
		return (int)(Math.random()*n);
	}
	
	/*
	 * Случайное целое число из диапозона [a, b)
	 */
	
	public static int _aRandomInt(int a, int b)
	{
		return a + (int)(Math.random()*(b - a));
	}
	
	/*
	 * Случайное число из диапозона [a, b)
	 */
	
	public static double _aRandomDouble(int a, int b)
	{
		return a + Math.random()*(b - a);
	}
	
	/*
	 * Случайное число i из дискретного распределения с вероятностью in[i]
	 */
	
	public static int _aRandomIntFromDiscrete(double[] in)
	{
		double dR = Math.random();
		double dSum = 0.0;
		for(int i = 0; i < in.length; i++)
		{
			dSum += in[i];
			if(dSum >= dR) return i;
 		}
		return -1;
	}
	
	/*
	 * Бинарный поиск - поиск значение в массиве
	 * -1 значит значение не присутствует в списке, иначе вернет индекс где находится элемент
	 * ! in должен быть отсортирован по возрастанию
	 */
	
	public static int _aBinarySearch(int nKey, int[] in)
	{
		int nLeft = 0;
		int nRight = in.length - 1;
		while(nLeft <= nRight)
		{
			int nMiddle = nLeft + (nRight - nLeft)/2;
			if(nKey < in[nMiddle]) 
				nRight = nMiddle - 1;
			else if(nKey > in[nMiddle]) 
				nLeft = nMiddle + 1;
			else
				return nMiddle;
		}
		return -1;
	}
	
	/*
	 * Перевод числа из 10 в двоичную систему
	 * == Integer.toBinaryString(N)
	 */
	
	public static char[] _aIntToBinaryString(int nIn)
	{
		String sResult = "";
		for(int i = nIn; i > 0; i /= 2)
		{
			sResult += (i % 2);
		}
		char[] cResult = sResult.toCharArray();
		return cResult;
	}
	
	/*
	 * Последовательность Фибоначи
	 */
	
	public static long _aFibonachi(int n)
	{
		if(n == 0) return 0;
		if(n == 1) return 1;
		return _aFibonachi(n - 1) + _aFibonachi(n - 2);
	}
	
	/*
	 * Факториал
	 */
	
	public static long _aFaktorial(int n)
	{
		long p = 1;
		for(int i = n; i > 0; i--)
		{
			p *= i;
		}
		return p;
	}
	
	public static long _aFaktorialRecursive(int n)
	{
		if(n == 0)
			return 1;
		return n*_aFaktorialRecursive(n-1);
	}
	
	/*
	 * Двухстековый алгоритм Дейкстры вычисления арифметических выражений
	 * (1 + ((2+2)*(4-1))......
	 */
	public static double _aDeixtraStackOperation(String sIn)
	{
		Stack<Double> vOperand = new Stack<Double>();
		Stack<Character> vOperator = new Stack<Character>();
		for(int i = 0; i < sIn.length(); i++)
		{
			if(sIn.charAt(i) == '(')
			{
				continue;
			}
			else if(sIn.charAt(i) == '+' || sIn.charAt(i) == '-' || sIn.charAt(i) == '*' || sIn.charAt(i) == '/')
			{
				vOperator.push(sIn.charAt(i));
			}
			else if(sIn.charAt(i) == ')')
			{
				char sOp = vOperator.pop();
				double dOp = vOperand.pop();
				switch(sOp)
				{
					case '+':
					{
						dOp = vOperand.pop() + dOp;
						break;
					}
					case '-':
					{
						dOp = vOperand.pop() - dOp;
						break;
					}
					case '*':
					{
						dOp = vOperand.pop() * dOp;
						break;
					}
					case '/':
					{
						dOp = vOperand.pop() / dOp;
						break;
					}
				}
				vOperand.push(dOp);
			}
			else
			{
				String sTemp =  sIn.charAt(i) + "" ;
				vOperand.push(Double.parseDouble(sTemp));
			}
		}
		
		return vOperand.pop();
	}
	
	/*
	 * Задача Иосифа: N человек хотят выбрать одного,
	 * для этого они становятся в круг и считая удаляют каждого M-го.
	 * Нужно для заданого M и N показать последовательность исключения, чтобы правильно выбрать где стоять 
	 */
	
	public static int[] _aJosephus(int N, int M)
	{
		MyStack<Integer> s = new MyStack<Integer>();
		int[] result = new int[N];
		
		for(int i = N - 1; i >= 0; i--)
		{
			s.push(i);
		}
		
		// N = 8, M = 2
		// 0 1 2 3 4 5 6 7
		
		int l = 0;
		int j = 0;
		int k = 0;
		
		while(!s.isEmpty())
		{
			j++;
			if(j == M)
			{
				result[k] = s.popAt(l);
				k++;
				l--;
				j = 0;
			}
			if(k == N - 1)
				result[k] = s.pop();
			if(l == s.size() - 1)
				l = 0;
			else
				l++;
		}
		return result;
	}
	
	
//--------------------------------------------------------------------------------------//	
	/*Блок геометриии*/
	
	/*
	 * Вычисление гипотенузы прямоугольного треугольника
	 */
	
	public static double _gHipotenuse(double a, double b)
	{
		return Math.sqrt(Math.pow(a, 2) + Math.pow(b, 2));
	}
	
	/*
	 * Длина окружности
	 */
	
	public static double _gCircleLength(double r)
	{
		return 2*Math.PI*r;
	}
	
	/*
	 * Количество диагоналей у n-угольника
	 */
	
	public static int _gCountDioganal(int n) throws Exception
	{
		if(n < 4)
		{
			throw new Exception("N is less than 4!");
		}
		else
		{
			return (n*(n-3)) / 2;
		}
	}
	
	/*
	 * Площади фигур
	 */
	
	public static double _gSquareS(int a)
	{
		return a*a;
	}
	
	public static double _gRectS(int a, int b)
	{
		return a*b;
	}
	
	public static double _gCircleS(int r)
	{
		return Math.PI*Math.pow(r, 2);
	}
	
	public static double _gTriangleS(int a, int b)
	{
		return 0.5*a*b;
	}
	
	
	
//--------------------------------------------------------------------------------------//	
	/*Блок статистики*/
	
	/*
	 * Сумма значений массива
	 */
	
	public static double _sSum(double[] in)
	{
		double dSum = 0.0;
		for(int i = 0; i < in.length; i++)
			dSum += in[i];
		return dSum;
	}
	
	/*
	 * Среднее значение из массива
	 */
	
	public static double _sAverage(double[] in) throws Exception
	{
		if(in.length != 0)
		{
			double dSum = _sSum(in);
			return dSum / in.length;
		}
		else
		{
			throw new Exception("Zero division! Because array is empty!");
		}
	}
	
	/*
	 * Максимальный элемент
	 */
	
	public static double _sMax(double[] in) throws Exception
	{
		if(in.length != 0)
		{
			double dMax = in[0];
			for(int i = 0; i < in.length; i++)
			{
				if(in[i] > dMax)
					dMax = in[i];
			}
			return dMax;
		}
		else
		{
			throw new Exception("Array is empty!");
		}
	}
	
	/*
	 * Минимальный элемент
	 */
	
	public static double _sMin(double[] in) throws Exception
	{
		if(in.length != 0)
		{
			double dMin = in[0];
			for(int i = 0; i < in.length; i++)
			{
				if(in[i] < dMin)
					dMin = in[i];
			}
			return dMin;
		}
		else
		{
			throw new Exception("Array is empty!");
		}
	}
	
	/*
	 * Дисперсия
	 */
	
	public static double _sDispersia(double[] in) throws Exception
	{
		if(in.length > 1)
		{
			double sMean = _sAverage(in);
			int N = in.length;
			double dSum = 0.0;
			
			for(int i = 0; i < N; i++)
			{
				dSum += Math.pow(in[i] - sMean, 2);
			}
			return dSum / (N - 1); 
		}
		else
		{
			throw new Exception("Zero division! Because length of array is less than 2!");
		}
	}
	
	/*
	 * Среднее квадратичное отклонение
	 */
	
	public static double _sStdev(double[] in) throws Exception
	{
		if(in.length > 1)
		{
			double sD = _sDispersia(in);
			return Math.sqrt(sD);
		}
		else
		{
			throw new Exception("Zero division! Because length of array is less than 2!");
		}
	}
	
//--------------------------------------------------------------------------------------//
	/*Блок работы с массивами и матрицами*/
	
	/*
	 * Перемешать случайным образом элементы массива
	 */
	
	public static void _mShuffle(double[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			int r = _aRandomInt(in.length - i);
			double dTemp = in[i];
			in[i] = in[r];
			in[r] = dTemp;
		}
	}
	
	public static void _mShuffle(int[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			int r = _aRandomInt(in.length - i);
			int dTemp = in[i];
			in[i] = in[r];
			in[r] = dTemp;
		}
	}
	
	public static void _mShuffle(char[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			int r = _aRandomInt(in.length - i);
			char dTemp = in[i];
			in[i] = in[r];
			in[r] = dTemp;
		}
	}
	
	/*
	 * Заполнить элементами массив
	 */
	
	public static void _mFillArrayWithRandomDouble(double[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			in[i] = Math.random();
		}
	}
	
	public static void _mFillArrayWithRandomDouble(double[] in, int a, int b)
	{
		for(int i = 0; i < in.length; i++)
		{
			in[i] = _aRandomDouble(a, b);
		}
	}
	
	public static void _mFillArrayWithRandomInt(int[] in, int a, int b)
	{
		for(int i = 0; i < in.length; i++)
		{
			in[i] = _aRandomInt(a, b);
		}
	}
	
	/*
	 * Вывести массив на экран
	 */
	
	public static void _mPrintArray(double[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			System.out.print(in[i] + "\t");
		}
		System.out.println();
	}
	
	public static void _mPrintArray(int[] in)
	{
		for(int i = 0; i < in.length; i++)
		{
			System.out.print(in[i] + "\t");
		}
		System.out.println();
	}
	
	/*
	 * Поменять порядок элементов на обратный
	 */
	
	public static void _mReverse(double[] in)
	{
		int n = in.length;
		for(int i = 0; i < n/2; i++)
		{
			double dTemp = in[i];
			in[i] = in[n - 1 - i];
			in[n - 1 - i] = dTemp;
		}
	}
	
	public static void _mReverse(int[] in)
	{
		int n = in.length;
		for(int i = 0; i < n/2; i++)
		{
			int dTemp = in[i];
			in[i] = in[n - 1 - i];
			in[n - 1 - i] = dTemp;
		}
	}
	
	public static void _mReverse(char[] in)
	{
		int n = in.length;
		for(int i = 0; i < n/2; i++)
		{
			char dTemp = in[i];
			in[i] = in[n - 1 - i];
			in[n - 1 - i] = dTemp;
		}
	}
	
	/*
	 * Является ли строка палиндромом, т.е. символы с начала = символам в конце
	 */
	
	public static boolean _mIsPalindrom(String sIn)
	{
		int n = sIn.length();
		for(int i = 0; i < n/2; i++)
		{
			if(sIn.charAt(i) != sIn.charAt(n - 1 - i))
				return false;
		}
		return true;
	}
	
	/*
	 * Упорядочен ли массив строк по алфавиту
	 */
	
	public static boolean _mIsInAlphabetOrder(String[] sIn)
	{
		int n = sIn.length;
		for(int i = 0; i < n - 1; i++)
		{
			if(sIn[i].compareTo(sIn[i + 1]) > 0)
				return false;
		}
		return true;
	}
	
	/*
	 * Заполнить матрицу случайными числами
	 */
	
	public static void _mFillMatrixWithRandom(double in[][])
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] = Math.random();
			}
		}
	}
	
	public static void _mFillMatrixWithRandom(double in[][], int a, int b)
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] = _aRandomDouble(a, b);
			}
		}
	}
	
	public static void _mFillMatrixWithRandom(int in[][], int a, int b)
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] = _aRandomInt(a, b);
			}
		}
	}
	
	/*
	 * Вывести матрицу на экран
	 */
	
	public static void _mPrintMatrix(double in[][])
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				System.out.print(in[i][j] + " ");
			}
			System.out.println();
		}
	}
	
	public static void _mPrintMatrix(int in[][])
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				System.out.print(in[i][j] + "\t");
			}
			System.out.println();
		}
	}
	
	public static void _mPrintMatrixFormat(int in[][])
	{
		for(int i = 0; i < in.length; i++)
		{
			if(i == 0)
			{
				for(int j = 0; j < in[i].length; j++)
				{
					System.out.print("\t" + "[" + j + "]");
				}
				System.out.println();
			}
			System.out.println();
			for(int j = 0; j < in[i].length; j++)
			{
				if(j == 0)
					System.out.print("[" + i + "]" + "\t");
				System.out.print(" " + in[i][j] + " " + "\t");
			}
			System.out.println();
		}
	}
	
	/*
	 * Перемножить матрицы
	 */
	
	public static int[][] _mMultiply(int A[][], int B[][]) throws Exception
	{
		int m1 = A.length;
		int n1 = A[0].length;
		int m2 = B.length;
		int n2 = B[0].length;
		
		if(n1 == m2)
		{
			int C[][] = new int[m1][n2];
			for(int i = 0; i < m1; i++)
			{
				for(int j = 0; j < n2; j++)
				{
					int nS = 0;
					for(int k = 0; k < m1; k++)
					{
						nS += A[i][k]*B[k][j]; 
					}
					C[i][j] = nS;
				}
			}
			return C;
		}
		else
		{
			throw new Exception("Count of rows of first matrix is not equal to count of columns of second!");
		}
	}
	
	public static double[][] _mMultiply(double A[][], double B[][]) throws Exception
	{
		int m1 = A.length;
		int n1 = A[0].length;
		int m2 = B.length;
		int n2 = B[0].length;
		
		if(n1 == m2)
		{
			double C[][] = new double[m1][n2];
			for(int i = 0; i < m1; i++)
			{
				for(int j = 0; j < n2; j++)
				{
					int nS = 0;
					for(int k = 0; k < m1; k++)
					{
						nS += A[i][k]*B[k][j]; 
					}
					C[i][j] = nS;
				}
			}
			return C;
		}
		else
		{
			throw new Exception("Count of rows of first matrix is not equal to count of columns of second!");
		}
	}
	
	/*
	 * Транспонирование матрицы
	 */
	
	public static int[][] _mT(int A[][])
	{
		int m = A.length;
		int n = A[0].length;
		int temp[][] = new int[n][m];
		
		if(m == n)
		{
			for(int i = 0; i < m; i++)
			{
				for(int j = 0; j < n; j++)
				{
					temp[i][j] = A[j][i];
				}
			}
		}
		else
		{
			for(int i = 0; i < n; i++)
			{
				for(int j = 0; j < m; j++)
				{
					temp[i][j] = A[j][i];
				}
			}
		}
		return temp;
	}
	
	public static double[][] _mT(double A[][])
	{
		int m = A.length;
		int n = A[0].length;
		double temp[][] = new double[n][m];
		
		if(m == n)
		{
			for(int i = 0; i < m; i++)
			{
				for(int j = 0; j < n; j++)
				{
					temp[i][j] = A[j][i];
				}
			}
		}
		else
		{
			for(int i = 0; i < n; i++)
			{
				for(int j = 0; j < m; j++)
				{
					temp[i][j] = A[j][i];
				}
			}
		}
		return temp;
	}
	
	/*
	 * Инициализация единичной матрицы
	 */
	
	public static void _mE(double A[][])
	{
		int m = A.length;
		int n = A[0].length;
		
		for(int i = 0; i < m; i++)
		{
			for(int j = 0; j < n; j++)
			{
				if(i == j)
					A[i][j] = 1.0;
				else
					A[i][j] = 0.0;
			}
		}
	}
	
	public static void _mE(int A[][])
	{
		int m = A.length;
		int n = A[0].length;
		
		for(int i = 0; i < m; i++)
		{
			for(int j = 0; j < n; j++)
			{
				if(i == j)
					A[i][j] = 1;
				else
					A[i][j] = 0;
			}
		}
	}
	
	/*
	 * Матрица умноженная на число
	 */
	
	public static void _mMultiplyMatrixOnConstant(double in[][], int nConst)
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] *= nConst;
			}
		}
	}
	
	public static void _mMultiplyMatrixOnConstant(double in[][], double nConst)
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] *= nConst;
			}
		}
	}
	
	public static void _mMultiplyMatrixOnConstant(int in[][], int nConst)
	{
		for(int i = 0; i < in.length; i++)
		{
			for(int j = 0; j < in[i].length; j++)
			{
				in[i][j] *= nConst;
			}
		}
	}
	
	/*
	 * Перемножение векторов
	 */
	
	public static double[] _mMyltiplyArrays(double[] a, double[] b) throws Exception
	{
		if(a.length == b.length)
		{
			double[] r = new double[a.length];
			for(int i = 0; i < a.length; i++)
			{
				r[i] = a[i]*b[i];
			}
			return r;
		}
		else
		{
			throw new Exception("Not equal lengths");
		}
	}
	
	public static int[] _mMyltiplyArrays(int[] a, int[] b) throws Exception
	{
		if(a.length == b.length)
		{
			int[] r = new int[a.length];
			for(int i = 0; i < a.length; i++)
			{
				r[i] = a[i]*b[i];
			}
			return r;
		}
		else
		{
			throw new Exception("Not equal lengths");
		}
	}
	
	/*
	 * Перемножение матриц и векторов
	 */
	
	public static int[][] _mArrayToMatrix(int[] v, boolean bRowOrCol)
	{
		int[][] r;
		if(bRowOrCol)
		{
			r = new int[v.length][1];
			for(int i = 0; i < v.length; i++)
			{
				r[i][0] = v[i];
			}
		}
		else
		{
			r = new int[1][v.length];
			for(int i = 0; i < v.length; i++)
			{
				r[0][i] = v[i];
			}
		}
		return r;
	}
	
	public static int[][] _mMultiplyMatrixOnArray(int A[][], int B[]) throws Exception
	{
		int n1 = A[0].length;
		int m2 = B.length;
		
		if(n1 == m2)
		{
			int C[][] = _mMultiply(A, _mArrayToMatrix(B, true));
			return C;
		}
		else
		{
			throw new Exception("Count of rows of first matrix is not equal to count of columns of second!");
		}
	}
	
	public static int[][] _mMultiplyMatrixOnArray(int A[], int B[][]) throws Exception
	{
		int n1 = A.length;
		int m2 = B.length;
		
		if(n1 == m2)
		{
			int C[][] = _mMultiply(_mArrayToMatrix(A, false), B);
			return C;
		}
		else
		{
			throw new Exception("Count of rows of first matrix is not equal to count of columns of second!");
		}
	}
	
//--------------------------------------------------------------------------------------//	

	/*
	 * Сортировки
	 */
	
	private static boolean _gt(Comparable a, Comparable b)
	{
		return a.compareTo(b) > 0;
	}
	
	private static boolean _lt(Comparable a, Comparable b)
	{
		return a.compareTo(b) < 0;
	}
	
	
	public static void _sBubbleSort(double[] in, boolean bWay)
	{
		double dTemp;
		if(bWay)
		{
			for(int i = 0; i < in.length; i++)
			{
				for(int j = 0; j < in.length - 1; j++)
				{
					if(in[j] > in[j+1])
					{
						dTemp = in[j];
						in[j] = in[j+1];
						in[j+1] = dTemp;
					}
				}
			}
		}
		else
		{
			for(int i = 0; i < in.length; i++)
			{
				for(int j = 0; j < in.length - 1; j++)
				{
					if(in[j] < in[j+1])
					{
						dTemp = in[j];
						in[j] = in[j+1];
						in[j+1] = dTemp;
					}
				}
			}
		}
	}
	
	public static void _sBubbleSort(int[] in, boolean bWay)
	{
		int dTemp;
		if(bWay)
		{
			for(int i = 0; i < in.length; i++)
			{
				for(int j = 0; j < in.length - 1; j++)
				{
					if(in[j] > in[j+1])
					{
						dTemp = in[j];
						in[j] = in[j+1];
						in[j+1] = dTemp;
					}
				}
			}
		}
		else
		{
			for(int i = 0; i < in.length; i++)
			{
				for(int j = 0; j < in.length - 1; j++)
				{
					if(in[j] < in[j+1])
					{
						dTemp = in[j];
						in[j] = in[j+1];
						in[j+1] = dTemp;
					}
				}
			}
		}
	}
	
	public static void _sSelectionSort(int[] in)
	{
		int dTemp;
		int nMin;
		for(int i = 0; i < in.length; i++)
		{
			nMin = i;
			for(int j = i + 1; j < in.length; j++)
			{
				if(in[j] < in[nMin])
				{
					nMin = j;
				}
			}
			dTemp = in[i];
			in[i] = in[nMin];
			in[nMin] = dTemp;
		}
	}

	public static void _sInsertionSort(int[] in)
	{
		int dTemp;
		for(int i = 1; i < in.length; i++)
		{
			for(int j = i; j > 0 && in[j] < in[j-1]; j--)
			{
				dTemp = in[j];
				in[j] = in[j - 1];
				in[j - 1] = dTemp;
			}
		}
	}
	
	public static void _sShellSort(int[] in)
	{
		int dTemp;
		int nH = 1;
		while(nH < in.length/3) nH = 3*nH + 1;
		
		while(nH >= 1)
		{
			for(int i = nH; i < in.length; i++)
			{
				for(int j = i; j >= nH && in[j] < in[j - nH]; j-=nH)
				{
					dTemp = in[j];
					in[j] = in[j - nH];
					in[j - nH] = dTemp;
				}
			}
			nH = nH / 3;
		}
	}
	
	private static void _sMerge(int[] in, int[] vAux, int nStart, int nMiddle, int nEnd)
	{
		int i = nStart, j = nMiddle + 1;
		for(int k = nStart; k <= nEnd; k++)
		{
			vAux[k] = in[k];
		}
		for(int k = nStart; k <= nEnd; k++)
		{
			if(i > nMiddle)
				in[k] = vAux[j++];
			else if(j > nEnd)
				in[k] = vAux[i++];
			else if(vAux[j] < in[i])
				in[k] = vAux[j++];
			else
				in[k] = vAux[i++];
		}
	}
	
	public static void _sMergeSort(int[] in, int[] vAux, int nStart, int nEnd)
	{
		if(nEnd <= nStart) return;
		int nMiddle = nStart + (nEnd - nStart)/2;
		_sMergeSort(in, vAux, nStart, nMiddle);
		_sMergeSort(in, vAux, nMiddle + 1, nEnd);
		_sMerge(in,vAux, nStart, nMiddle, nEnd);
	}
	
	
	
//--------------------------------------------------------------------------------------//	
	
	/*
	 * Классы и структуры
	 */
	
	public static class MyStack<T> implements Iterable<T>
	{
		private Node head;
		private int N;
		private class Node
		{
			T item;
			Node next;
		}
		
		public boolean isEmpty()
		{
			return N == 0;
		}

		public int size()
		{
			return N;
		}
		
		public void push(T newValue)
		{
			Node old = head;
			head = new Node();
			head.item = newValue;
			head.next = old;
			N++;
		}
		
		public T pop()
		{
			T ret = head.item;
			head = head.next;
			N--;
			return ret;
		}
		
		public T popAt(int nPos)
		{
			int i = 0;
			Node pCurr = head;
			Node pPrev = head;
			T ret = null;
			if(nPos == 0)
			{
				ret = pop();
			}
			else
			{
				while(pCurr != null)
				{
					if(i == nPos)
					{
						ret = pCurr.item;
						pPrev.next = pCurr.next;
						N--;
						break;
					}
					i++;
					pPrev = pCurr;
					pCurr = pCurr.next;
				}
			}
			return ret;
		}
		
		public T peek()
		{
			Node pTemp = head;
			T item = pTemp.item;
			int j = 0;
			while(pTemp != null)
			{
				j++;
				if(j == N)
				{
					item = pTemp.item;
				}
				pTemp = pTemp.next;
			}
			return item;
		}
		
		public MyStack<T> copy()
		{
			MyStack<T> newStack = new MyStack<T>();
			MyStack<T> tempStack = new MyStack<T>();
			
			for(Node i = head; i != null; i = i.next)
			{
				tempStack.push(i.item);
			}
			while(!tempStack.isEmpty())
			{
				newStack.push(tempStack.pop());
			}
			return newStack;
		}
		
		public MyStack<T> reverse()
		{
			MyStack<T> newStack = new MyStack<T>();
			
			for(Node i = head; i != null; i = i.next)
			{
				newStack.push(i.item);
			}
			return newStack;
		}
		
		@Override
		public Iterator<T> iterator() 
		{
			return new MyStackIterator();
		}
		
		private class MyStackIterator implements Iterator<T>
		{
			private Node curr = head;
			
			@Override
			public boolean hasNext() 
			{
				return curr != null;
			}

			@Override
			public T next() 
			{
				T ret = curr.item;
				curr = curr.next;
				return ret;
			}

			@Override
			public void remove() {	}
			
		}
	}
	
	public static class MyBag<T> implements Iterable<T>
	{
		private Node head;
		private int N;
		private class Node
		{
			T item;
			Node next;
		}
		
		public boolean isEmpty()
		{
			return N == 0;
		}

		public int size()
		{
			return N;
		}
		
		public void add(T newValue)
		{
			Node old = head;
			head = new Node();
			head.item = newValue;
			head.next = old;
			N++;
		}
		
		@Override
		public Iterator<T> iterator() 
		{
			return new MyBagIterator();
		}
		
		private class MyBagIterator implements Iterator<T>
		{
			private Node curr = head;
			
			@Override
			public boolean hasNext() 
			{
				return curr != null;
			}

			@Override
			public T next() 
			{
				T ret = curr.item;
				curr = curr.next;
				return ret;
			}

			@Override
			public void remove() {	}
			
		}
	}
	
	public static class MyQueue<T> implements Iterable<T>
	{
		private Node head;
		private Node tail;
		private int N;
		private class Node
		{
			T item;
			Node next;
		}
		
		public boolean isEmpty()
		{
			return N == 0;
		}

		public int size()
		{
			return N;
		}
		
		public void enqueue(T newValue)
		{
			Node oldTail = tail;
			
			tail = new Node();
			tail.item = newValue;
			tail.next = null;
			if(isEmpty())
				head = tail;
			else
				oldTail.next = tail;
			N++;
		}
		
		public T dequeue()
		{
			T ret = head.item;
			head = head.next;
			if(isEmpty())
				tail = null;
			N--;
			return ret;
		}
		
		@Override
		public Iterator<T> iterator() 
		{
			return new MyQueueIterator();
		}
		
		private class MyQueueIterator implements Iterator<T>
		{
			private Node curr = head;
			
			@Override
			public boolean hasNext() 
			{
				return curr != null;
			}

			@Override
			public T next() 
			{
				T ret = curr.item;
				curr = curr.next;
				return ret;
			}

			@Override
			public void remove() {	}
		}

	}
	
	public static class MyStopWatch
	{
		private long dStartTime;
		private long dEndTime;
		public void start()
		{
			dStartTime = System.currentTimeMillis();
		}
		public void stop()
		{
			dEndTime = System.currentTimeMillis();
		}
		public double getElapsedSeconds()
		{
			return (dEndTime - dStartTime)/1000;
		}
		public double getElapsedMinutes()
		{
			return getElapsedSeconds()/60;
		}
		public void reset()
		{
			this.dStartTime = 0;
			this.dEndTime = 0;
		}
	}






}
