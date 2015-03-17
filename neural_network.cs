using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NeuralNetwork
{
    class Program
    {
        static string EffectsEncoding(int index, int n)
        {
            if (n == 2) 
            { 
                if (index == 0) 
                    return "-1"; 
                else if (index == 1) 
                    return "1"; 
            } 
            
            int[] values = new int[n-1]; 
            if (index == n-1)// Last item is all -1s.
            { 
                for (int i = 0; i < values.Length; ++i) 
                    values[i] = -1; 
            } 
            else 
            { 
                values[index] = 1; // 0 values are already there. 
            } string s = values[0].ToString(); 
            
            for (int i = 1; i < values.Length; ++i) 
                s += "," + values[i]; 
            
            return s;
        }

        static string DummyEncoding(int index, int N)
        {
            int[] values = new int[N];
            values[index] = 1;
            string s = values[0].ToString();
            for (int i = 1; i < values.Length; ++i)
                s += "," + values[i];
            return s;
        }

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

        public static void ShowVector(double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i] + "\t");
            }
        }

        static void MinMaxNormalization(double[][] input, int nColumn)
        {
            double dTempMin = input[0][nColumn];
            double dTempMax = input[0][nColumn];

            for (int i = 1; i < input.Length; i++)
            {
                if (dTempMin > input[i][nColumn])
                    dTempMin = input[i][nColumn];
                if (dTempMax < input[i][nColumn])
                    dTempMax = input[i][nColumn];                
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (dTempMax - dTempMin != 0)
                    input[i][nColumn] = (input[i][nColumn] - dTempMin) / (dTempMax - dTempMin);
                else
                    input[i][nColumn] = 0.5;
            }
        }

        static void GaussNormalization(double[][] input, int nColumn)
        {
            double dSum = 0;
            double dMean = 0;
            double dDev = 0;
            double dStdev = 0;

            for (int i = 0; i < input.Length; i++)
            {
                dSum += input[i][nColumn];
            }

            dMean = dSum / input[0].Length;

            for (int i = 0; i < input.Length; i++)
            {
                dDev += Math.Pow(input[i][nColumn] - dMean, 2);
            }

            dDev /= input[0].Length;// or (input[0].Length - 1) - sample standard deviation

            dStdev = Math.Sqrt(dDev);

            for (int i = 0; i < input.Length; i++)
            {
                input[i][nColumn] = (input[i][nColumn] - dMean) / dStdev;
            }
        }

        static void Main(string[] args)
        {
            //input data

            //normalization

            /*double[][] input = new double[4][];
            input[0] = new double[] { -1, 25.0, 1, 0, 63000.00, 1, 0, 0 };
            input[1] = new double[] { 1, 36.0, 0, 1, 55000.00, 0, 1, 0 };
            input[2] = new double[] { -1, 40.0, -1, -1, 74000.00, 0, 0, 1 };
            input[3] = new double[] { -1, 23.0, 1, 0, 28000.00, 0, 1, 0 };

            ShowData(input);

            Console.WriteLine();

            MinMaxNormalization(input, 1);

            ShowData(input);

            Console.WriteLine();

            GaussNormalization(input, 4);

            ShowData(input);*/


            //----------------------------------------------------------------------

            //perceptron

            /*double[][] trainData = new double[8][];
            trainData[0] = new double[] { 1.5, 2.0, -1 }; 
            trainData[1] = new double[] { 2.0, 3.5, -1 }; 
            trainData[2] = new double[] { 3.0, 5.0, -1 }; 
            trainData[3] = new double[] { 3.5, 2.5, -1 }; 
            trainData[4] = new double[] { 4.5, 5.0, 1 }; 
            trainData[5] = new double[] { 5.0, 7.0, 1 };
            trainData[6] = new double[] { 5.5, 8.0, 1 }; 
            trainData[7] = new double[] { 6.0, 6.0, 1 };

            ShowData(trainData);

            int numInput = 2; 
            Perceptron p = new Perceptron(numInput);
            double alpha = 0.001; 
            int maxEpochs = 100;
            double[] weights = p.Train(trainData, alpha, maxEpochs);

            ShowVector(weights);

            double[][] newData = new double[6][]; 
            newData[0] = new double[] { 3.0, 4.0 }; // Should be -1. 
            newData[1] = new double[] { 0.0, 1.0 }; // Should be -1. 
            newData[2] = new double[] { 2.0, 5.0 }; // Should be -1. 
            newData[3] = new double[] { 5.0, 6.0 }; // Should be 1. 
            newData[4] = new double[] { 9.0, 9.0 }; // Should be 1. 
            newData[5] = new double[] { 4.0, 6.0 };

            Console.WriteLine("\nPredictions for new people:\n"); 
            for (int i = 0; i < newData.Length; ++i) 
            { 
                Console.Write("Age, Income = "); 
                ShowVector(newData[i]); 
                int c = p.ComputeOutput(newData[i]); 
                Console.Write(" Prediction is "); 
                if (c == -1) 
                    Console.WriteLine("(-1) liberal"); 
                else if (c == 1) 
                    Console.WriteLine("(+1) conservative"); 
            }*/


            //----------------------------------------------------------------------

            //feed-forward

            /*int nNumInputs = 3;
            int nNumOutputs = 2;
            int nNumHiddenLayers = 4;

            NeuralNetwork n = new NeuralNetwork(nNumInputs, nNumOutputs, nNumHiddenLayers);

            double[] weights = new double[] {0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.10,0.11, 0.12, 0.13, 0.14, 0.15, 0.16, 0.17, 0.18, 0.19, 0.20,0.21, 0.22, 0.23, 0.24, 0.25, 0.26};
            double[] xValues = new double[] { 1.0, 2.0, 3.0 };
            double[] yValues = n.ComputeOutput(xValues);
            ShowVector(xValues);
            Console.WriteLine();
            ShowVector(yValues);*/

            //----------------------------------------------------------------------

            //back-propagation - algorithm for training neural network
            
            /*int nNumInputs = 3;
            int nNumOutputs = 2;
            int nNumHiddenLayers = 4;
            double learnRate = 0.05; 
            double momentum = 0.01; 
            int maxEpochs = 1000;

            BackPropagation n = new BackPropagation(nNumInputs, nNumOutputs, nNumHiddenLayers);

            double[] weights = new double[] { 0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.10, 0.11, 0.12, 0.13, 0.14, 0.15, 0.16, 0.17, 0.18, 0.19, 0.20, 0.21, 0.22, 0.23, 0.24, 0.25, 0.26 };
            double[] xValues = new double[] { 1.0, 2.0, 3.0 };
            double[] tValues = new double[] { 0.2500, 0.7500 };

            n.FindWeights(tValues, xValues, learnRate, momentum, maxEpochs);
            double[] bestWeights = n.GetWeights();

            ShowVector(weights);

            Console.WriteLine();

            ShowVector(bestWeights);*/

            /*DirectoryInfo d = new DirectoryInfo("I:\\");

            double s = 0;

            foreach (var item in d.GetFiles("*", SearchOption.AllDirectories))
            {
                s += item.Length;
            }

            Console.WriteLine(s/1024/1024/1024);*/


            Console.ReadKey(true);
        }
    }

    //----------------------------------------------------------------------

    //full-connected 
    public class NeuralNetwork
    {
        private int nNumInputs;
        private int nNumOutputs;
        private int nNumHiddenLayers;
        private double[] inputs;
        

       
        private double[][] ihWeights;
        
        private double[] hBiases;
        private double[] hOutputs;
        private double[][] hoWeights;
        
        private double[] oBiases;
        private double[] outputs;

        public NeuralNetwork(int a, int b, int c)
        {
            this.nNumInputs = a;
            this.nNumOutputs = b;
            this.nNumHiddenLayers = c;

            this.inputs = new double[nNumInputs];
            this.outputs = new double[nNumOutputs];

            this.hoWeights = MakeMatrix(nNumHiddenLayers, nNumOutputs);

            this.hOutputs = new double[nNumHiddenLayers];
            this.hBiases = new double[nNumHiddenLayers];

            this.oBiases = new double[nNumOutputs];

            this.ihWeights = MakeMatrix(nNumInputs, nNumHiddenLayers);
        }

        public void SetWeights(double[] w)
        {
            //weights = new double[w.Length];
           // Array.Copy(w, weights, w.Length);
            int nNumWeights = (nNumInputs * nNumHiddenLayers) + nNumHiddenLayers + (nNumHiddenLayers * nNumOutputs) + nNumOutputs;
            int k = 0;
            if (w.Length == nNumWeights)
            {
                for (int i = 0; i < nNumInputs; i++)
                {
                    for (int j = 0; j < nNumHiddenLayers; j++)
                    {
                        ihWeights[i][j] = w[k++];
                    }
                }

                for (int i = 0; i < nNumHiddenLayers; i++)
                {
                    hBiases[i] = w[k++];
                }

                for (int i = 0; i < nNumHiddenLayers; i++)
                {
                    for (int j = 0; j < nNumOutputs; j++)
                    {
                        hoWeights[i][j] = w[k++];
                    }
                }

                for (int i = 0; i < nNumOutputs; i++)
                {
                    oBiases[i] = w[k++];
                }


            }
            else
            {
                Console.WriteLine("Not enough weights!");
            }
        }

        public double[] ComputeOutput(double[] xValues)
        {
            if (xValues.Length == nNumInputs)
            {
                double[] hSums = new double[nNumHiddenLayers];
                double[] oSums = new double[nNumOutputs];

                for (int i = 0; i < nNumInputs; i++)
                    inputs[i] = xValues[i];

                for (int i = 0; i < hSums.Length; i++)
                {
                    for (int j = 0; j < nNumInputs; j++)
			        {
                        hSums[i] += inputs[j] * ihWeights[j][i];
			        }
                    hSums[i] += hBiases[i];
                }

                for (int i = 0; i < hOutputs.Length; i++)
                {
                    hOutputs[i] = HyperTan(hSums[i]);
                }

                for (int i = 0; i < oSums.Length; i++)
                {
                    for (int j = 0; j < hSums.Length; j++)
                    {
                        oSums[i] += hSums[j] * hoWeights[j][i];
                    }
                    oSums[i] += oBiases[i];
                }

                outputs = SoftMax(oSums);
            }

            return outputs;
        }

        public double HyperTan(double v)
        {
            if (v < -20.0)
                return -1.0;
            else if (v > 20.0)
                return 1.0;
            else
                return Math.Tanh(v);
        }

        private static double LogSigmoid(double x) 
        { 
            if (x < -45.0) 
                return 0.0; 
            else if (x > 45.0) 
                return 1.0; 
            else return 1.0 / (1.0 + Math.Exp(-x)); 
        }

        public double[] SoftMax(double[] oSums)
        {
            double denom = 0.0;

            for (int i = 0; i < oSums.Length; i++)
            {
                denom += Math.Exp(oSums[i]);
            }

            double[] result = new double[oSums.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Exp(oSums[i]) / denom;
            }

            return result;
        }

        private static double[][] MakeMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }
    }

    //back-propagation
    public class BackPropagation
    {
        private int nNumInputs;
        private int nNumOutputs;
        private int nNumHiddenLayers;

        private double[] inputs;

        private double[][] ihWeights;
        private double[] hBiases;
        private double[] hOutputs;

        private double[][] hoWeights;
        private double[] oBiases;
        private double[] outputs;

        private double[] oGrads;
        private double[] hGrads;

        private double[][] ihPrevWeightsDelta; // For momentum with back-propagation. 
        private double[] hPrevBiasesDelta; 
        private double[][] hoPrevWeightsDelta; 
        private double[] oPrevBiasesDelta;

        public BackPropagation(int a, int b, int c)
        {
            this.nNumInputs = a;
            this.nNumOutputs = b;
            this.nNumHiddenLayers = c;

            this.inputs = new double[nNumInputs];
            this.outputs = new double[nNumOutputs];

            this.hoWeights = MakeMatrix(nNumHiddenLayers, nNumOutputs);

            this.hOutputs = new double[nNumHiddenLayers];
            this.hBiases = new double[nNumHiddenLayers];

            this.oBiases = new double[nNumOutputs];

            this.ihWeights = MakeMatrix(nNumInputs, nNumHiddenLayers);

            this.oGrads = new double[nNumOutputs];
            this.hGrads = new double[nNumHiddenLayers];

            ihPrevWeightsDelta = MakeMatrix(nNumInputs, nNumHiddenLayers);
            hPrevBiasesDelta = new double[nNumHiddenLayers];
            hoPrevWeightsDelta = MakeMatrix(nNumHiddenLayers, nNumOutputs);

            oPrevBiasesDelta = new double[nNumOutputs]; 
            InitMatrix(ihPrevWeightsDelta, 0.011); 
            InitVector(hPrevBiasesDelta, 0.011); 
            InitMatrix(hoPrevWeightsDelta, 0.011); 
            InitVector(oPrevBiasesDelta, 0.011);
        }

        public void SetWeights(double[] w)
        {
            //weights = new double[w.Length];
            // Array.Copy(w, weights, w.Length);
            int nNumWeights = (nNumInputs * nNumHiddenLayers) + nNumHiddenLayers + (nNumHiddenLayers * nNumOutputs) + nNumOutputs;
            int k = 0;
            if (w.Length == nNumWeights)
            {
                for (int i = 0; i < nNumInputs; i++)
                {
                    for (int j = 0; j < nNumHiddenLayers; j++)
                    {
                        ihWeights[i][j] = w[k++];
                    }
                }

                for (int i = 0; i < nNumHiddenLayers; i++)
                {
                    hBiases[i] = w[k++];
                }

                for (int i = 0; i < nNumHiddenLayers; i++)
                {
                    for (int j = 0; j < nNumOutputs; j++)
                    {
                        hoWeights[i][j] = w[k++];
                    }
                }

                for (int i = 0; i < nNumOutputs; i++)
                {
                    oBiases[i] = w[k++];
                }
            }
            else
            {
                Console.WriteLine("Not enough weights!");
            }
        }

        public double[] GetWeights()
        {
            int nNumWeights = (nNumInputs * nNumHiddenLayers) + nNumHiddenLayers + (nNumHiddenLayers * nNumOutputs) + nNumOutputs;
            int k = 0;
            double[] result = new double[nNumWeights];

            for (int i = 0; i < nNumInputs; ++i)
            {
                for (int j = 0; j < nNumHiddenLayers; ++j)
                {
                    result[k++] = ihWeights[i][j];
                }
            }

            for (int i = 0; i < nNumHiddenLayers; i++)
            {
                 result[k++] = hBiases[i];
            }

            for (int i = 0; i < nNumHiddenLayers; i++)
            {
                for (int j = 0; j < nNumOutputs; j++)
                {
                     result[k++] = hoWeights[i][j];
                }
            }

            for (int i = 0; i < nNumOutputs; i++)
            {
                result[k++] = oBiases[i];
            }

            return result;
        }

        public void FindWeights(double[] tValues, double[] xValues, double learnRate, double momentum, int maxEpochs)
        {
            int nEpoch = 0;
            while (nEpoch < maxEpochs)
            {
                double[] yValues = ComputeOutput(xValues);
                UpdateWeights(tValues, learnRate, momentum);

                if (nEpoch % 100 == 0)
                {
                    Console.WriteLine("Current output for epoch#" + nEpoch);
                    Program.ShowVector(yValues);
                }

                ++nEpoch;
            }
        }

        private void UpdateWeights(double[] tValues, double learnRate, double momentum)
        {
            for (int i = 0; i < oGrads.Length; ++i) 
            { 
                double derivative = (1 - outputs[i]) * outputs[i]; // Derivative of softmax is y(1-y). 
                oGrads[i] = derivative * (tValues[i] - outputs[i]); // oGrad = (1 - O)(O) * (T-O) 
            }
            for (int i = 0; i < hGrads.Length; ++i) 
            { 
                double derivative = (1 - hOutputs[i]) * (1 + hOutputs[i]); // f' of tanh is (1-y)(1+y). 
                double sum = 0.0; for (int j = 0; j < nNumOutputs; ++j) // Each hidden delta is the sum of numOutput terms. 
                    sum += oGrads[j] * hoWeights[i][j]; // Each downstream gradient * outgoing weight. 
                hGrads[i] = derivative * sum; // hGrad = (1-O)(1+O) * Sum(oGrads*oWts) 
            }
            for (int i = 0; i < ihWeights.Length; ++i)
            {
                for (int j = 0; j < ihWeights[i].Length; ++j)
                {
                    double delta = learnRate * hGrads[j] * inputs[i]; ihWeights[i][j] += delta; // Update. 
                    ihWeights[i][j] += momentum * ihPrevWeightsDelta[i][j]; // Add momentum factor. 
                    ihPrevWeightsDelta[i][j] = delta; // Save the delta for next time. 
                }
            }
            for (int i = 0; i < hBiases.Length; ++i) 
            { 
                double delta = learnRate * hGrads[i] * 1.0; // The 1.0 is a dummy value; it could be left out. 
                hBiases[i] += delta; hBiases[i] += momentum * hPrevBiasesDelta[i]; hPrevBiasesDelta[i] = delta; // Save delta. 
            }
            for (int i = 0; i < hoWeights.Length; ++i) 
            {
                for (int j = 0; j < hoWeights[i].Length; ++j) 
                { 
                    double delta = learnRate * oGrads[j] * hOutputs[i]; hoWeights[i][j] += delta; 
                    hoWeights[i][j] += momentum * hoPrevWeightsDelta[i][j]; 
                    hoPrevWeightsDelta[i][j] = delta; // Save delta. 
                } 
            }

            for (int i = 0; i < oBiases.Length; ++i) 
            { 
                double delta = learnRate * oGrads[i] * 1.0; 
                oBiases[i] += delta; 
                oBiases[i] += momentum * oPrevBiasesDelta[i]; 
                oPrevBiasesDelta[i] = delta; // Save delta. 
            }
        }

        public double[] ComputeOutput(double[] xValues)
        {
            if (xValues.Length == nNumInputs)
            {
                double[] hSums = new double[nNumHiddenLayers];
                double[] oSums = new double[nNumOutputs];

                for (int i = 0; i < nNumInputs; i++)
                    inputs[i] = xValues[i];

                for (int i = 0; i < hSums.Length; i++)
                {
                    for (int j = 0; j < nNumInputs; j++)
                    {
                        hSums[i] += inputs[j] * ihWeights[j][i];
                    }
                    hSums[i] += hBiases[i];
                }

                for (int i = 0; i < hOutputs.Length; i++)
                {
                    hOutputs[i] = HyperTan(hSums[i]);
                }

                for (int i = 0; i < oSums.Length; i++)
                {
                    for (int j = 0; j < hSums.Length; j++)
                    {
                        oSums[i] += hSums[j] * hoWeights[j][i];
                    }
                    oSums[i] += oBiases[i];
                }

                outputs = SoftMax(oSums);
            }

            return outputs;
        }

        public double HyperTan(double v)
        {
            if (v < -20.0)
                return -1.0;
            else if (v > 20.0)
                return 1.0;
            else
                return Math.Tanh(v);
        }

        private static double LogSigmoid(double x)
        {
            if (x < -45.0)
                return 0.0;
            else if (x > 45.0)
                return 1.0;
            else return 1.0 / (1.0 + Math.Exp(-x));
        }

        public double[] SoftMax(double[] oSums)
        {
            double denom = 0.0;

            for (int i = 0; i < oSums.Length; i++)
            {
                denom += Math.Exp(oSums[i]);
            }

            double[] result = new double[oSums.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Math.Exp(oSums[i]) / denom;
            }

            return result;
        }

        private static double[][] MakeMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        private static void InitMatrix(double[][] values, double value)
        {
            for (int i = 0; i < values.Length; ++i)
                for (int j = 0; j < values[i].Length; ++j)
                    values[i][j] = value;
        }

        private static void InitVector(double[] values, double value)
        {
             for (int i = 0; i < values.Length; ++i)
                 values[i] = value;
        }

        
    }


    //----------------------------------------------------------------------

    public class Perceptron
    {
        private int numInput;
        private double[] input;
        private double[] weights;
        private int output;
        private double bias;
        private Random rnd;

        public Perceptron(int n)
        {
            this.numInput = n;
            this.input = new double[n];
            this.weights = new double[n];
            this.rnd = new Random(0);
            InitializeWeights();
        }

        private void InitializeWeights()
        {
            double lo = -0.01;
            double hi = 0.01;
            for (int i = 0; i < weights.Length; ++i)
            {
                weights[i] = (hi - lo) * rnd.NextDouble() + lo;
            }
            bias = (hi - lo) * rnd.NextDouble() + lo;
        }

        public int ComputeOutput(double[] xValues)
        {
            double dSum = 0;
            for (int i = 0; i < xValues.Length; i++)
            {
                input[i] = xValues[i];
                dSum += input[i] * weights[i];
            }
            dSum += bias;
            int result = Activation(dSum);
            this.output = result;

            return result;
        }

        private static int Activation(double v)
        {
            if (v >= 0.0)
                return +1;
            else 
                return -1;
        }

        public double[] Train(double[][] trainData, double alpha, int maxEpochs)
        {
            int epoch = 0;
            double[] xValues = new double[this.numInput];
            int desired = 0;
            int computed = 0;

            int[] sequence = new int[trainData.Length];

            for (int i = 0; i < sequence.Length; i++)
            {
                sequence[i] = i;
            }

            int idx = 0;

            while (epoch < maxEpochs)
            {
                Shuffle(sequence);
                for (int i = 0; i < trainData.Length; ++i)
                {
                    idx = sequence[i];
                    Array.Copy(trainData[i], xValues, numInput);
                    desired = (int)trainData[idx][numInput];
                    computed = ComputeOutput(xValues);
                    Update(computed, desired, alpha);
                }
                ++epoch;
            }
            double[] result = new double[numInput + 1];
            Array.Copy(this.weights, result, numInput);
            result[result.Length - 1] = bias;
            return result;
        }

        private void Shuffle(int[] sequence)
        {
            int r = 0;
            int t = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                r = rnd.Next(i, sequence.Length);
                t = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = t;
            }
        }

        private void Update(int computed, int desired, double alpha)
        {
            if (computed == desired) return; // We're good. 
            int delta = computed - desired; // If computed > desired, delta is +. 
            for (int i = 0; i < this.weights.Length; ++i) // Each input-weight pair. 
            { 
                if (computed > desired && input[i] >= 0.0) // Need to reduce weights. 
                    weights[i] = weights[i] - (alpha * delta * input[i]); // delta is +, input is + 
                else if (computed > desired && input[i] < 0.0) // Need to reduce weights. 
                    weights[i] = weights[i] + (alpha * delta * input[i]); // delta is +, input is - 
                else if (computed < desired && input[i] >= 0.0) // Need to increase weights. 
                    weights[i] = weights[i] - (alpha * delta * input[i]); // delta is -, input is + 
                else if (computed < desired && input[i] < 0.0) // Need to increase weights. 
                    weights[i] = weights[i] + (alpha * delta * input[i]); // delta is -, input is - 
            } // Each weight.
            if (computed > desired)
                bias = bias - (alpha * delta); // Decrease. 
            else 
                bias = bias + (alpha * delta); // Increase.
        }
    }

}
