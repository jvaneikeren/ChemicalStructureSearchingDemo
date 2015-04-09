using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalStructureSearchingDemo.Core
{
    public enum SearchType {Exact, Similarity, Substructure }

    public class ChemicalStructureSearcher
    {
        private IndexSearcher IndexSearcher { get; set; }

        /// <summary>
        /// Constructor to initialize the index searcher.
        /// </summary>
        /// <param name="indexPath">The full path to the Lucene index created by a ChemicalStructureIndexer.</param>
        public ChemicalStructureSearcher(string indexPath)
        {
            // Create the lucene index searcher.
            this.IndexSearcher = new IndexSearcher(Lucene.Net.Store.FSDirectory.Open(indexPath), true);
        }

        /// <summary>
        /// Searches for a specified query structure.
        /// </summary>
        /// <param name="queryStructure">The chemical structure to search for.</param>
        /// <param name="searchType">The desired search type (Exact, Similarity, or Substructure).</param>
        /// <returns></returns>
        public List<ChemicalStructureSearchResult> Search(ChemicalStructure queryStructure, SearchType searchType)
        {
            var results = new List<ChemicalStructureSearchResult>();

            // Form the lucene query.
            Query query = CreateLuceneQuery(queryStructure, searchType);

            // Execute to obtain lucene hit pointers; we're going to artifically cap this out at 100 hits.
            TopDocs hits = this.IndexSearcher.Search(query, 100);

            // Loop through and form the results.
            foreach(ScoreDoc scoreDoc in hits.ScoreDocs)
            {
                // Retrieve the lucene document, and form a chemical structure result.
                Document doc = this.IndexSearcher.Doc(scoreDoc.Doc);
                var result = new ChemicalStructureSearchResult()
                {
                    ChemicalStructure = new ChemicalStructure(doc.Get(ChemicalStructureIndexer.FIELD_NAME), doc.Get(ChemicalStructureIndexer.FIELD_MOLFILE)),
                    Score = scoreDoc.Score,
                };

                // One catch: for Substructure searches, we have actually identified the set of chemical structures that MIGHT be
                // substructure matches; for Substructure searches, we need to perform an actual substructure determination.
                if ((searchType != SearchType.Substructure) || result.ChemicalStructure.HasSubstructure(queryStructure))
                    results.Add(result);
            }

            return results;
        }

        private Query CreateLuceneQuery(ChemicalStructure queryStructure, SearchType searchType)
        {
            BooleanQuery query = new BooleanQuery();

            switch(searchType)
            {
                // For exact searches, the search results MUST match the exact key.
                case SearchType.Exact:
                    query.Add(new TermQuery(new Term(ChemicalStructureIndexer.FIELD_EXACTKEY, queryStructure.GetUniqueKey())), Occur.MUST);
                    break;

                // For similarity searches, the search results SHOULD contain the query similarity fingerprint positions.
                case SearchType.Similarity:
                    foreach (int fingerprintPosition in queryStructure.GetSimilarityFingerprintPositions())
                        query.Add(new TermQuery(new Term(ChemicalStructureIndexer.FIELD_FINGERPRINT_POSITION_SIMILARITY, fingerprintPosition.ToString())), Occur.SHOULD);
                    break;
                   
                // For substructure searches, the search results MUST contain ALL of the query substructure fingerprint positions.
                case SearchType.Substructure:
                    foreach (int fingerprintPosition in queryStructure.GetSubstructureFingerprintPositions())
                        query.Add(new TermQuery(new Term(ChemicalStructureIndexer.FIELD_FINGERPRINT_POSITION_SUBSTRUCTURE, fingerprintPosition.ToString())), Occur.MUST);
                    break;
            }

            return query;
        }
    }
}