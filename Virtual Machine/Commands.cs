using System.IO;
using System.Linq;

namespace Virtual_Machine
{
    public class Commands
    {
        string encr;

        public Commands() // Reading the ecnryption file
        {
            FileInfo encryptionFilePath = new FileInfo("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\q1_encr.txt");
            if (encryptionFilePath.Exists)
            {
                using (StreamReader sr = encryptionFilePath.OpenText())
                {
                    encr = sr.ReadToEnd();
                }
            }
        }

        public void INC(Program program)
        {
            int rightIndex = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            program.reg[rightIndex]++;
        }

        public void DEC(Program program)
        {
            int rightIndex = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            program.reg[rightIndex]--;
        }

        public void MOV(Program program)
        {
            int rightIndex = int.Parse(program.parameters[program.j].ElementAt(1).ToString());
            int rightNumber = int.Parse(program.parameters[program.j].ElementAt(0).ToString());

            program.reg[rightIndex] = rightNumber;
        }

        public void MOVC(Program program)
        {
            int rightNumber = int.Parse(program.parameters[program.j], System.Globalization.NumberStyles.HexNumber);

            program.reg[0] = rightNumber;
        }

        public void LSL(Program program)
        {
            int rightIndex = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            program.reg[rightIndex] = program.reg[rightIndex] << 1;
        }

        public void LSR(Program program)
        {
            int rightIndex = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            program.reg[rightIndex] = program.reg[rightIndex] >> 1;
        }

        public void JMP(Program program)                                                                    // all jump commands works wrong bcs of index (counter) wrong assigning
        {
            program.j = int.Parse(program.parameters[program.j], System.Globalization.NumberStyles.HexNumber);
        }

        public void JZ(Program program)                                                                     // this shouldn't work bcs of the loop
        {
            if (program.flag)
            {
                program.j = int.Parse(program.parameters[program.j], System.Globalization.NumberStyles.HexNumber);
            }
        }

        public void JNZ(Program program)
        {
            if (!program.flag)
            {
                program.j = int.Parse(program.parameters[program.j], System.Globalization.NumberStyles.HexNumber);
            }
        }

        public void JFE(Program program)
        {
            if (program.fileEnd)
            {
                program.j = int.Parse(program.parameters[program.j], System.Globalization.NumberStyles.HexNumber);
            }
        }

        public void RET(Program program)
        {
            program.flag = true;
        }

        public void ADD(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());
            int Ry = int.Parse(program.parameters[program.j].ElementAt(0).ToString());

            program.reg[Rx] = program.reg[Rx] + program.reg[Ry];
        }

        public void SUB(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());
            int Ry = int.Parse(program.parameters[program.j].ElementAt(0).ToString());

            program.reg[Rx] = program.reg[Rx] - program.reg[Ry];
        }

        public void XOR(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());
            int Ry = int.Parse(program.parameters[program.j].ElementAt(0).ToString());

            program.reg[Rx] = program.reg[Rx] ^ program.reg[Ry];
        }

        public void OR(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());
            int Ry = int.Parse(program.parameters[program.j].ElementAt(0).ToString());

            program.reg[Rx] = program.reg[Rx] | program.reg[Ry];
        }

        public void IN(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            program.reg[Rx] = (int)encr.ElementAt(0);
            encr = encr.Remove(0, 1);

            if (encr.Length < 1)
            {
                program.fileEnd = true;
            }
        }

        public void OUT(Program program)
        {
            int Rx = int.Parse(program.parameters[program.j].ElementAt(1).ToString());

            using (StreamWriter sw = new StreamWriter("D:\\C# Training\\Labs\\Virtual Machine\\Virtual Machine\\bin\\Debug\\output.txt", true))
            {
                sw.Write((char)program.reg[Rx]);
            }
        }
    }
}
