using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Virtual_Machine
{
    public class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            program.ReadFromFilesBytes();

            //program.ReadFromFilesString();

            program.Formate();

            Console.WriteLine("Program Ends!");
            Console.ReadLine();
        }

        public string[] commands;
        public string[] parameters;
        public int[] reg = new int[16];
        public bool flag = false;
        public bool fileEnd = false;
        public byte[] decr;
        public int j;
        StringBuilder decryptionString = new StringBuilder();
        public FileInfo encryptionFilePath;
        public FileInfo decryptionFilePath;

        private void ReadFromFilesBytes()
        {
            encryptionFilePath = new FileInfo("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\q1_encr.txt");
            decryptionFilePath = new FileInfo("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\decryptor.bin");
            
            if (encryptionFilePath.Exists && decryptionFilePath.Exists)
            {
                decr = File.ReadAllBytes(decryptionFilePath.FullName);
                foreach (byte b in decr)
                {
                    decryptionString.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
                }
            }
            else
            {
                throw new Exception("Wrong path!");
            }
        }

        private void ReadFromFilesString()
        {
            encryptionFilePath = new FileInfo("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\part 2\\encryption.txt");
            decryptionFilePath = new FileInfo("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\part 2\\decryption.txt");

            if (encryptionFilePath.Exists && decryptionFilePath.Exists)
            {
                using (StreamReader sr = new StreamReader(decryptionFilePath.FullName))
                {
                    decryptionString.Append(sr.ReadToEnd());
                }
            }
            else
            {
                throw new Exception("Wrong path!");
            }
        }

        private void Formate()
        {
            int i = 0;

            commands = new string[decryptionString.Length / 2 / 2];
            parameters = new string[decryptionString.Length / 2 / 2];

            while (decryptionString.Length > 1)
            {
                commands[i] = decryptionString.ToString().Substring(0, 2);
                decryptionString.Remove(0, 2);

                parameters[i] = decryptionString.ToString().Substring(0, 2);
                decryptionString.Remove(0, 2);

                i++;
            }

            Run();
        }

        private void Run()
        {
            Commands cmd = new Commands(this);

            // Creating the list of  all functions from Commands class
            var commandList = new List<System.Reflection.MethodInfo>();
            foreach (var func in typeof(Commands).GetMethods())
            {
                commandList.Add(func);
            }

            // Finding the needed function with commandsIndex array
            int[] commandsIndex = new int[commands.Length];
            bool running = true;
            while (running)
            {
                if (flag)
                {
                    break;
                }

                commandsIndex[j] = Convert.ToInt32(commands[j], 16);

                commandList[commandsIndex[j] - 1].Invoke(cmd, new object[] { this }); // be careful with commandList array, it has spare methods
                //commandList[17 - 1].Invoke(cmd, new object[] { this }); // - for exact method

                j++; // j - as a counter
            }
        }
    }
}
