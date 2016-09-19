using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sarcasmbot.Executable
{
    class SarcasmService
    {
        private readonly string targetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Sarcasm.txt");

        private List<string> sarcasm = new List<string>();

        private Random random = new Random();
        
        public SarcasmService()
        {
            if (!File.Exists(this.targetFile))
            {
                File.Copy("Sarcasm.txt", targetFile);
            }
            
            this.sarcasm = File.ReadAllLines(targetFile).ToList();
        }

        public string Sarcasm()
        {
            return this.sarcasm[this.random.Next(sarcasm.Count)];
        }

        public void Add(string sarcasm)
        {
            this.sarcasm.Add(sarcasm);
            this.Write();
        }

        public void Remove(int index)
        {
            this.sarcasm.RemoveAt(index);
            this.Write();
        }

        public List<string> List()
        {
            return this.sarcasm;
        }

        public void Exit()
        {
            this.Write();   
        }

        private void Write()
        {
            File.WriteAllLines(targetFile, sarcasm.ToArray());
        }
    }
}
