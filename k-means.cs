using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    class Program
    {

        static void ShowData(double[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    Console.Write(input[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void ShowVector(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i] + "  ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //1. k-means algorithm

            double[][] rawData = new double[10][] {
                new double[2] { 73.0, 72.6 },
                new double[2] { 61.0, 54.4 },
                new double[2] { 67.0, 99.9 },
                new double[2] { 68.0, 97.3 },
                new double[2] { 62.0, 59.0 },
                new double[2] { 75.0, 81.6 },
                new double[2] { 74.0, 77.1 },
                new double[2] { 66.0, 97.3 },
                new double[2] { 68.0, 93.3 },
                new double[2] { 61.0, 59.0 }
            };

            int[] clustering = new int[rawData.Length];

            Console.WriteLine("Raw data:");

            ShowData(rawData);

            Console.WriteLine();

            int nClusters = 3;

            Console.WriteLine("Count of clusters: " + nClusters);

            Console.WriteLine();

            Clusterer c = new Clusterer(nClusters);

            c.SetData(rawData);

            clustering = c.GetResult();

            ShowVector(clustering);

            for (int i = 0; i < nClusters; i++)
            {
                Console.WriteLine("Cluster #" + i + ":");
                for (int j = 0; j < rawData.Length; j++)
                {
                    if (clustering[j] == i)
                    {
                        Console.Write(j + ": (");
                        for (int k = 0; k < rawData[j].Length; k++)
                        {
                            if(k == rawData[j].Length - 1)
                                Console.Write(rawData[j][k]);
                            else
                                Console.Write(rawData[j][k] + ",");
                        }
                        Console.WriteLine(")");
                    }
                }
            }

            Console.ReadLine();
        }
    }

    //k-means
    public class Clusterer
    {
        private int nClusters;
        private double[][] data;
        private double[][] centroids;
        private int[] clustering;
        private int[] CountClustering;


        private Random rnd;

        public Clusterer(int nClusters)
        {
            this.nClusters = nClusters;
            this.centroids = new double[nClusters][];
            this.CountClustering = new int[nClusters];
            this.rnd = new Random(0);
        }

        public void SetData(double[][] input)
        {
            this.data = new double[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                this.data[i] = input[i];
            }
        }

        public int[] GetResult()
        {
            int nRow = data.Length;
            int nCol = data[0].Length;

            int[] result = new int[nCol];

            for (int i = 0; i < centroids.Length; i++)
            {
                centroids[i] = new double[nCol];
            }

            InitRandom();

            bool changed = true;

            int nMax = nCol * 10;
            int k = 0;

            while (changed && k <= nMax)
            {
                ++k;
                UpdateCentroids();
                changed = UpdateClustering();
            }

            return clustering;
        }

        public bool UpdateClustering()
        {
            bool bChanged = false;
            int[] newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);
            double[] distances = new double[nClusters];
            int nClusterId = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < nClusters; j++)
                {
                    distances[j] = Distance(data[i], centroids[j]);
                }
                nClusterId = MinIndex(distances);
                if (nClusterId != newClustering[i])
                {
                    bChanged = true;
                    newClustering[i] = nClusterId;
                }
            }
            for (int i = 0; i < clustering.Length; i++)
            {
                ++CountClustering[newClustering[i]];
            }
            for (int i = 0; i < CountClustering.Length; i++)
            {
                if (CountClustering[i] == 0)
                    bChanged = false;
            }
            Array.Copy(newClustering, clustering, clustering.Length);
            return bChanged;
        }

        private double Distance(double[] a, double[] b)
        {
            double dTemp = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                dTemp += Math.Pow(a[i] - b[i], 2);
            }
            return Math.Sqrt(dTemp);
        }

        private int MinIndex(double[] dists)
        {
            int index = 0;
            double dMin = dists[0];
            for (int i = 1; i < dists.Length; i++)
            {
                if (dists[i] < dMin)
                {
                    dMin = dists[i];
                    index = i;
                }
            }
            return index;
        }

        public void UpdateCentroids()
        {
            int nRow = data.Length;
            int nCol = data[0].Length;

            for (int i = 0; i < CountClustering.Length; i++)
            {
                CountClustering[i] = 0;
            }

            for (int i = 0; i < clustering.Length; i++)
            {
                ++CountClustering[clustering[i]];
            }

            Console.WriteLine("Count clustering: ");

            ShowVector(CountClustering);

            for (int i = 0; i < centroids.Length; ++i)
            {
                for (int j = 0; j < centroids[i].Length; ++j)
                {
                    centroids[i][j] = 0.0;
                }
            }

            int clusterId = 0;

            for (int i = 0; i < nRow; ++i)
            {
                clusterId = clustering[i];
                for (int j = 0; j < nCol; ++j)
                {
                    centroids[clusterId][j] += data[i][j];
                }
            }

            for (int i = 0; i < centroids.Length; ++i)
            {
                for (int j = 0; j < centroids[i].Length; ++j)
                {
                    centroids[i][j] /= CountClustering[i];
                }
            }

            Console.WriteLine("Centroids: ");

            ShowData(centroids);

        }

        public void InitRandom()
        {
            this.clustering = new int[data.Length];
            int n = clustering.Length;
            int clusterId = 0;

            for (int i = 0; i < n; ++i)
            {
                clustering[i] = clusterId++;
                if (clusterId == this.nClusters)
                    clusterId = 0;
            }

            int r = 0;
            int temp = 0;

            for (int i = 0; i < n; ++i)//algorithm Fisher-Yetes of shuffling
            {
                r = rnd.Next(i, n);
                temp = clustering[r];
                clustering[r] = clustering[i];
                clustering[i] = temp;
            }

            Console.WriteLine("Clustering after initialization: ");

            ShowVector(clustering);

        }

        public static void ShowVector(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i] + "  ");
            }
            Console.WriteLine();
        }

        public static void ShowData(double[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    Console.Write(input[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }

    }


}
