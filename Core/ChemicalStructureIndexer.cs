using Lucene.Net.Documents;
using Lucene.Net.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalStructureSearchingDemo.Core
{
    public class ChemicalStructureIndexer
    {
        public const string FIELD_NAME = "Name";
        public const string FIELD_MOLFILE = "Molfile";
        public const string FIELD_EXACTKEY = "ExactKey";
        public const string FIELD_FINGERPRINT_POSITION_SIMILARITY = "SimilarityFingerprintPosition";
        public const string FIELD_FINGERPRINT_POSITION_SUBSTRUCTURE = "SubstructureFingerprintPosition";

        private IndexWriter IndexWriter { get; set; }

        /// <summary>
        /// Constructor to create a chemical structure indexer; creates the Lucene index at the specified path.
        /// </summary>
        /// <param name="indexPath">The full path to the desired folder where the Lucene index will be created.</param>
        public ChemicalStructureIndexer(string indexPath)
        {
            // Create a new Lucene index.
            this.IndexWriter = new IndexWriter(
                Lucene.Net.Store.FSDirectory.Open(indexPath),   // Create in the specified input folder.
                new Lucene.Net.Analysis.SimpleAnalyzer(),       // Use a simple analyzer; no tokenization is necessary.
                true,                                           // We're creating a new index; this will overwrite an existing one.
                IndexWriter.MaxFieldLength.UNLIMITED);          // Use unlimited field lengths.
        }

        /// <summary>
        /// Adds a chemical structure to the index.
        /// </summary>
        /// <param name="chemicalStructure">The chemical structure to add to the index.</param>
        public void AddChemicalStructure(ChemicalStructure chemicalStructure)
        {
            // Create a new Lucene document for the chemical structure.
            Document doc = new Document();

            // Add stored fields for the chemical structure name and molfile; this will
            // allow us to retrieve them later.
            doc.Add(new Field(FIELD_NAME, chemicalStructure.Name, Field.Store.YES, Field.Index.NO));
            doc.Add(new Field(FIELD_MOLFILE, chemicalStructure.MolfileContents, Field.Store.YES, Field.Index.NO));

            // Add searchable fields for exact key and similarity/substructure fingerprints.
            doc.Add(new Field(FIELD_EXACTKEY, chemicalStructure.GetUniqueKey(), Field.Store.NO, Field.Index.NOT_ANALYZED));
            foreach (int fingerprintPosition in chemicalStructure.GetSimilarityFingerprintPositions())
                doc.Add(new Field(FIELD_FINGERPRINT_POSITION_SIMILARITY, fingerprintPosition.ToString(), Field.Store.NO, Field.Index.NOT_ANALYZED));
            foreach (int fingerprintPosition in chemicalStructure.GetSubstructureFingerprintPositions())
                doc.Add(new Field(FIELD_FINGERPRINT_POSITION_SUBSTRUCTURE, fingerprintPosition.ToString(), Field.Store.NO, Field.Index.NOT_ANALYZED));

            // Add the document to the index.
            this.IndexWriter.AddDocument(doc);
        }

        /// <summary>
        /// Closes the index and writes the index to disk.
        /// </summary>
        public void Close()
        {
            // Close the index.
            this.IndexWriter.Dispose();
        }
    }
}