using System;

namespace menor_distancia
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = [2, 34, 23, -43, -2, 99];
            int[] arr2 = [7, 39, 54, 11, 9, 4];

            int dist = CalculaDistancia(arr1, arr2);
            Console.WriteLine($"{dist}");
        }

        private static int CalculaDistancia(int[] arr1, int[] arr2)
        {
            SortArray(arr1);
            SortArray(arr2);

            int i = 0, j = 0;

            int menorDistancia = int.MaxValue;


            while (i < arr1.Length && j < arr2.Length)
            {
                int dist = Math.Abs(arr1[i] - arr2[j]);
                if (dist < menorDistancia)
                    menorDistancia = dist;

                if (arr1[i] < arr2[j])
                    i++;
                else
                    j++;
            }

            return menorDistancia;
        }

        private static void SortArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int aux = arr[i];
                        arr[i] = arr[j];
                        arr[j] = aux;
                    }
                }
            }
        }
    }
}
