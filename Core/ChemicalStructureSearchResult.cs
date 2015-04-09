using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalStructureSearchingDemo.Core
{
    public class ChemicalStructureSearchResult
    {
        /// <summary>
        /// The chemical structure associated with the search result.
        /// </summary>
        public ChemicalStructure ChemicalStructure { get; set; }

        /// <summary>
        /// The raw Lucene score associated with the search result.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// A formatted version of the Lucene score.
        /// </summary>
        public string ScoreString
        {
            get { return this.Score.ToString("0.00"); }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>A string containing the formatted score and chemical structure name of the search result.</returns>
        public override string ToString()
        {
            return this.ScoreString + ": " + this.ChemicalStructure.Name;
        }
    }
}