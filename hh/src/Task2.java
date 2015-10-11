import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
 * <b>Task for HH school</b> <br/><br/>
 * 
 * <i>Finding count of ways to split number n on k partitions.</i> <br/><br/>
 * 
 * <p>There following conditions:
 * - 1 <= n <= 150, 1 <= k <= n</p>
 * 
 * @author Volobuev-AN
 * @since 09.10.2015
 * @version 1.0
 * 
 */

public class Task2
{	
	/**
	 * Initial number
	 */
	public static int n;
	
	/**
	 * Parts count
	 */
	public static int k;
	
	/**
	 * Result count of ways to split number on k partitions
	 */
	public static int p_n = 0;
	
	/**
	 * Const top border
	 */
	public static final int N = 150;
	
	/**
	 * Recurent function to implement algorithm of reverse lexographic search (Knut's book, part 4A), example: <br/>
	 * 	<b>n - 6, k - 3</b><br/>
	 * 		<i>step 1: 114</i><br/>
	 *  	<i>step 2: 123</i><br/>
	 *  	<i>step 3: 222</i><br/>
	 *  	<b>result:</b> 3<br/>
	 */
	
	public static void recurentPartition(int nSum, int nNum, int nVal)
	{
		if(nSum <= 0 || nNum <= 0 || nSum < nVal) return;//condition to stop recursion
		if(nNum == 1)//increase counter when part position comes to start point
		{
			p_n++;
			return;
		}
		for(int i = nVal; i < nSum; ++i)//iterate process to decrease one value and increase others or left them untouched: 114 -> 123 -> 222
		{
			recurentPartition(nSum - i, nNum - 1, i);
		}
	}
	
	/**
	 * Entry point to application <br/>
	 * Two possible ways to get initial values:<br/>
	 *  <i>1) From arguments of application: java Task2.class n k</i><br/>
	 *  <i>2) Dialog with a user: Input n and k parameters:6 3 PRESS ENTER</i><br/>
	 */
	
	public static void main(String[] args) 
	{
		/**
		 * Input stream for dialog with a user, if there are no arguments
		 */
		BufferedReader cin = new BufferedReader(new InputStreamReader(System.in));
		try
		{
			/**
			 * if there 2 arguments n and k, then use them
			 * */
			if(args.length == 2)
			{
				n = Integer.parseInt(args[0]);
				k = Integer.parseInt(args[1]);
			}
			else
			{		
				String sInputLine = null;
				System.out.print("Input n and k parameters(like:10 5):");
				/**
				 * Input n and k parameters:6 3 <ENTER>
				 * */
				if((sInputLine = cin.readLine()).length() != 0)
				{
					n = Integer.parseInt(sInputLine.split(" ")[0]);
					k = Integer.parseInt(sInputLine.split(" ")[1]);
				}
				else
				{
					System.out.println("Not valid input!");
					return;
				}
			}
			
			if(n >= 1 && n <= 150 && k <= n && k >= 1)
			{
				recurentPartition(n,k,1);
				System.out.println(p_n);
			}
			else
			{
				System.out.println("Not valid parameters!");
			}
		}
		catch(Exception e)
		{
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		finally//free resources
		{
			try 
			{
				if(cin != null)
					cin.close();
			}
			catch (Exception e) 
			{
				e.printStackTrace();
			}
		}
	}
}