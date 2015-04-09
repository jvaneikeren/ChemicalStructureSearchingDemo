using ChemicalStructureSearchingDemo.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalStructureSearchingDemo.IndexerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ensure valid inputs.
            if (args.Length != 2)
                throw new Exception("Usage: IndexerConsole [IndexPath] [MolFilesPath]");

            // Gather inputs.
            string indexPath = args[0];
            string molFilesPath = args[1];

            // Index all of the *.mol files in the specified input directory.
            var indexer = new ChemicalStructureIndexer(indexPath);
            foreach (string filePath in Directory.GetFiles(molFilesPath, "*.mol"))
            {
                Console.WriteLine("Indexing " + Path.GetFileName(filePath));
                indexer.AddChemicalStructure(new ChemicalStructure(filePath));
            }
            indexer.Close();
        }
    }
}
