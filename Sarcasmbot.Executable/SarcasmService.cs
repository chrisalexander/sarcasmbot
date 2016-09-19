using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sarcasmbot.Executable
{
    class SarcasmService
    {
        private List<string> sarcasm = new List<string>();

        private Random random = new Random();

        public SarcasmService()
        {
            if (File.Exists("Sarcasm.txt"))
            {
                this.sarcasm = File.ReadAllLines("Sarcasm.txt").ToList();
            }
        }

        public string Sarcasm()
        {
            return this.sarcasm[this.random.Next(sarcasm.Count)];
        }
    }
}
