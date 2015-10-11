import java.util.Arrays;
import java.util.Comparator;
import java.util.ArrayList;
import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
 * <b>Task for HH school</b> <br/>
 * 
 * <i>Finding minimal distance (min(d=sqrt((x1-x2)^2+(y1-y2)^2))) between multiple points</i> <br/>
 * 
 * @author Volobuev-AN
 * @since 09.10.2015
 * @version 1.0
 * 
 */

public class Task1 
{
	/**
	 * Structure to save point values
	 */
	private static class Point
	{
		public int x;
		public int y;
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public double getDistance(Point p)
		{
			return Math.sqrt((this.x - p.x)*(this.x - p.x) + (this.y - p.y)*(this.y - p.y));
		}
		@Override
		public String toString()
		{
			return "(" + this.x + ";" + this.y + ")";
		}
	}
	
	/**
	 * Variable to store current minimal distance, we initialize it with very big value at first step
	 */
	public static double dMinDistance;
	
	/**
	 * Method to check if current distance between two points is less than minimal distance <br/>
	 * and update current minimal value if it's true
	 */
	public static void checkDistance(Point p1, Point p2)
	{
		double dCurrentDistance = Math.sqrt((p1.x - p2.x)*(p1.x - p2.x) + (p1.y - p2.y)*(p1.y - p2.y));
		if(dCurrentDistance < dMinDistance)
			dMinDistance = dCurrentDistance;
	}
	
	/**
	 * Method to merge arrays by special rule: compare left and right's parts and move them 
	 */
	public static void merge(Point[] vSrc, Point[] vAux, int nLow, int nMiddle, int nHigh)
	{
		for(int i = nLow; i <= nHigh; ++i)
		{
			vAux[i] = vSrc[i];
		}
		int i = nLow, j = nMiddle + 1;
		for(int k = nLow; k <= nHigh; ++k)
		{
			if(i > nMiddle) vSrc[k] = vAux[j++];
			else if(j > nHigh) vSrc[k] = vAux[i++];
			else if(vAux[j].y < vAux[i].y) vSrc[k] = vAux[j++];
			else vSrc[k] = vAux[i++];
		}
	}
	
	/**
	 * Recurent method to find minimal distance implementing Preparata's algorithm, based on map-reduce technique (like quick sort)
	 */
	public static void findMin(Point[] vSrc, Point[] vAux, int nLow, int nHigh)
	{
		if(nHigh - nLow <= 3)//condition when to stop recursion, when range between left and right border is too small
		{
			for(int i = 0; i < nHigh; ++i)
			{
				for(int j = i + 1; j <= nHigh; ++j)
				{
					checkDistance(vSrc[i],vSrc[j]);//as if we just use O(N*N) algorithm, but only for short range of values
				}
			}
			//sort array for y to increase speed of searching
			Arrays.sort(vSrc, new Comparator<Point>() {
				@Override
				public int compare(Point p1, Point p2) 
				{
					if(p1.y < p2.y)
						return -1;
					else if(p1.y == p2.y)
						return 0;
					else
						return 1;
				}
			});
			return;
		}
		int nMiddle = (nHigh + nLow) / 2;
		int nMiddleX = vSrc[nMiddle].x;
		
		//run function for left part(1..m) and right part(m+1..n) of initial array
		findMin(vSrc, vAux, nLow, nMiddle);
		findMin(vSrc, vAux, nMiddle + 1, nHigh);
		
		merge(vSrc, vAux, nLow, nMiddle, nHigh);
		
		int tsz = 0;
		for(int i = nLow; i <= nHigh; ++i)
		{
			if(Math.abs(vSrc[i].x - nMiddleX) < nMiddleX)
			{
				for(int j = tsz - 1; j >= 0 && vSrc[i].y - vAux[j].y < dMinDistance; --j)
				{
					checkDistance(vSrc[i], vAux[j]);//update dMinDistance if we need
					vAux[tsz++] = vSrc[i];
				}
			}
		}
		
	}
	
	/**
	 * Entry point to application <br/>
	 * Initial values are set by user (by input them in dialog mode):<br/>
	 * <i>Input points:<br/>
	 * 10 10 PRESS ENTER<br/>
	 * 20 10 PRESS ENTER<br/>
	 * 20 15 PRESS ENTER<br/>
	 * PRESS ENTER<br/>
	 * <b>5.0</b></i><br/><br/>
	 * Two ways to count minimal distance:<br/>
	 *  <i>1) Straight one O(N*N)<br/>
	 *  2) Preparata's algorithm (apprx. O(N*logN))</i><br/>
	 */
	public static void main(String[] args) 
	{
		BufferedReader cin = new BufferedReader(new InputStreamReader(System.in));//stream for reading data from user input
		try
		{
			//1):
			
			String sInputLine = null;
			ArrayList<Point> vPoints = new ArrayList<Point>();//collection of points from input stream
			
			while((sInputLine = cin.readLine()).length() != 0)//getting list(because we don't know how many points a user inputs) of points
			{
				vPoints.add(new Point(Integer.parseInt(sInputLine.split(" ")[0]),Integer.parseInt(sInputLine.split(" ")[1])));
			}
			
			if(vPoints.size() > 1)//check if there are two points at least
			{
				//init min distance with distance between two first points, or we just can initialize it with 1E20
				dMinDistance = vPoints.get(0).getDistance(vPoints.get(1));
				double dCurrentDistance = 0.0;
				for(int i = 0; i < vPoints.size() - 1; ++i)
				{
					for(int j = i + 1; j < vPoints.size(); ++j)
					{
						dCurrentDistance = vPoints.get(i).getDistance(vPoints.get(j));
						if(dCurrentDistance < dMinDistance)
						{
							dMinDistance = dCurrentDistance;
						}
					}
				}
				System.out.println(dMinDistance);
			}
			else
			{
				System.out.println("We need at least two point to calculate the distance!");
			}
			
			//2):

			Point[] vPoints2 = (Point[])vPoints.toArray(new Point[vPoints.size()]);//cast list to array
			
			/*sort array for x*/
			Arrays.sort(vPoints2, new Comparator<Point>() {
				@Override
				public int compare(Point p1, Point p2) 
				{
					if(p1.x < p2.x || p1.x == p2.x && p1.y < p2.y)
						return -1;
					else if(p1.x == p2.x && p1.y == p2.y)
						return 0;
					else
						return 1;
				}
			});

			dMinDistance = 1E20;//set min dist to big value
			Point[] vAuxPoints = new Point[vPoints2.length];//auxiliary array to store merged values
			
			findMin(vPoints2, vAuxPoints, 0, vPoints2.length - 1);
			
			System.out.println(dMinDistance);
		}
		catch(Exception e)
		{
			System.out.println(e.getMessage());
			e.printStackTrace();
		}
		finally
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