using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography;

namespace criptografia_navio
{
    public class Program
    {
        static void Main(string[] args)
        {
            string message = "10010110 11110111 01010110 00000001 00010111 00100110 01010111 00000001 00010111 01110110 01010111 00110110 11110111 11010111 01010111 00000011";
            int[] bitArray = TransformIntoBitArray(message);


            ByteGroup byteGroup = new ByteGroup(bitArray);
            
            int[] semiGroups = byteGroup.CreateSemiByteGroup(bitArray);


            Decrypt(semiGroups);
        }

        private static void Decrypt(int[] semiGroups)
        {
            for (int i = 0; i < semiGroups.Length; i += 8)
            {
                int byteValue = 0;
                for (int bit = 0; bit < 8; ++bit)
                {
                    byteValue = (byteValue << 1) | semiGroups[i + bit];
                }
                Console.Write((char)byteValue);
            }
            Console.WriteLine();
        }

        private static int[] TransformIntoBitArray(string message)
        {
            message = message.Replace(" ", "");
            int[] bitArray = new int[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                bitArray[i] = message[i] - '0';
            }
            return bitArray;
        }
    }

    public class ByteGroup
    {
        public int[] Bytes { get; set; }

        public ByteGroup(int[] bytes)
        {
            Bytes = bytes;
        }

        public int[] CreateSemiByteGroup(int[] bytes)
        {
            int[] dividedGroup = new int[bytes.Length];
            int dividedGroupIndex = 0;

            for (int i = 0; i < bytes.Length; i += 8)
            {
                int[] bitGroup = new int[8];
                ByteArrayCopy(bytes, i, bitGroup, 0, 8);

                /*foreach (int element in bitGroup)
                {
                    Console.WriteLine($"Array antes dos 2 bits invertidos: {element}");
                }*/

                bitGroup[6] = InvertValues(bitGroup[6]);
                bitGroup[7] = InvertValues(bitGroup[7]);

                /*foreach (int element in bitGroup)
                {
                    Console.WriteLine($"Array com 2 bits invertidos: {element}");
                }*/

                int[] temp = new int[4];
                ByteArrayCopy(bitGroup, 0, temp, 0, 4);
                ByteArrayCopy(bitGroup, 4, bitGroup, 0, 4);
                ByteArrayCopy(temp, 0, bitGroup, 4, 4);

                ByteArrayCopy(bitGroup, 0, dividedGroup, dividedGroupIndex, 8);

                /*foreach (int element in bitGroup)
                {
                    Console.WriteLine($"Array com 4 bits trocados: {element}");
                }*/

                dividedGroupIndex += 8;
            }

            return dividedGroup;
        }

        private static int InvertValues(int a)
        {
            return a == 1 ? 0 : 1;
        }

        private static void ByteArrayCopy(
            int[] source,
            int sourceIndex,
            int[] destination,
            int destinationIndex,
            int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (sourceIndex + i >= source.Length 
                    || destinationIndex + i >= destination.Length)
                {
                    throw new Exception();
                }
                destination[destinationIndex + i] = source[sourceIndex + i];
            }
        }
    }
}
