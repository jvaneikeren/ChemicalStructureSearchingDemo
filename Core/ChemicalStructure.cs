using com.ggasoftware.indigo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalStructureSearchingDemo.Core
{
    public class ChemicalStructure
    {
        /// <summary>
        /// The Molfile contents of the chemical structure.
        /// </summary>
        public string MolfileContents { get; set; }

        /// <summary>
        /// An optional name associated with the chemical structure.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ChemicalStructure() { }

        /// <summary>
        /// Constructor to initial data properties.
        /// </summary>
        /// <param name="name">The name associated with the chemical structure.</param>
        /// <param name="molfileContents">The Molfile contents of the chemical structure.</param>
        public ChemicalStructure(string name, string molfileContents)
            : this()
        {
            this.Name = name;
            this.MolfileContents = molfileContents;
        }

        /// <summary>
        /// Constructor to initialize from a local Molfile.
        /// </summary>
        /// <param name="filePath">The full path to the *.mol file.</param>
        public ChemicalStructure(string filePath)
            : this(System.IO.Path.GetFileNameWithoutExtension(filePath), System.IO.File.ReadAllText(filePath)) { }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>The chemical structure name.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Returns whether the chemical structure contains a specified substructure.
        /// </summary>
        /// <param name="substructureQuery">The specified substructure to search for.</param>
        /// <returns>True if this chemical structure contains the specified substructure.</returns>
        public bool HasSubstructure(ChemicalStructure substructureQuery)
        {
            bool hasSubstructure = false;

            using (Indigo indigo = new Indigo())
            {
                // Load inputs.
                IndigoObject structure = CreateIndigoStructure(indigo);
                IndigoObject substructure = indigo.loadQueryMolecule(substructureQuery.MolfileContents);

                // Perform the match.
                IndigoObject substructureMatcher = indigo.substructureMatcher(structure);
                hasSubstructure = (substructureMatcher.match(substructure) != null);

                // Dispose.
                structure.Dispose();
                substructure.Dispose();
                substructureMatcher.Dispose();
            }

            return hasSubstructure;
        }

        /// <summary>
        /// Returns a depiction of the chemical structure as a bitmap.
        /// </summary>
        /// <param name="width">The desired width in pixels.</param>
        /// <param name="height">The desired height in pixels.</param>
        /// <returns>The bitmap depicting the chemical structure.</returns>
        public Bitmap ToBitmap(int width, int height)
        {
            Bitmap bitmap = null;

            using (Indigo indigo = new Indigo())
            {
                IndigoRenderer indigoRenderer = new IndigoRenderer(indigo);
                indigo.setOption("render-coloring", true);
                indigo.setOption("render-image-size", width, height);
                indigo.setOption("render-label-mode", "hetero");
                indigo.setOption("render-margins", 10, 10);
                indigo.setOption("render-output-format", "png");
                indigo.setOption("render-relative-thickness", 1.6f);
                IndigoObject structure = CreateIndigoStructure(indigo);
                structure.dearomatize();
                structure.layout();
                bitmap = indigoRenderer.renderToBitmap(structure);
                structure.Dispose();
            }

            return bitmap;
        }

        /// <summary>
        /// Returns a depiction of the chemical structure as a byte array comprising the contents of a bitmap.
        /// </summary>
        /// <param name="width">The desired width in pixels.</param>
        /// <param name="height">The desired height in pixels.</param>
        /// <returns>The byte array comprising the bitmap depicting the chemical structure.</returns>
        public byte[] ToBitmapBytes(int width, int height)
        {
            MemoryStream ms = new MemoryStream();
            this.ToBitmap(width, height).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        /// <summary>
        /// Returns a unique key for the chemical structure.
        /// </summary>
        /// <returns>A canonical Smiles string representing the unique key for the chemical structure.</returns>
        public string GetUniqueKey()
        {
            string uniqueKey = String.Empty;

            using (Indigo indigo = new Indigo())
            {
                IndigoObject structure = CreateIndigoStructure(indigo);
                uniqueKey = structure.canonicalSmiles();
                structure.Dispose();
            }

            return uniqueKey;
        }

        /// <summary>
        /// Returns similarity-type fingerprint feature positions.
        /// </summary>
        /// <returns>An array of similarity-type fingerprint feature index positions.</returns>
        public List<int> GetSimilarityFingerprintPositions()
        {
            return GetFingerprintPositions("sim");
        }

        /// <summary>
        /// Returns substructure-type fingerprint feature positions.
        /// </summary>
        /// <returns>An array of substructure-type fingerprint feature index positions.</returns>
        public List<int> GetSubstructureFingerprintPositions()
        {
            return GetFingerprintPositions("sub");
        }

        private List<int> GetFingerprintPositions(string type)
        {
            List<int> fingerprintPositions = new List<int>();

            using (Indigo indigo = new Indigo())
            {
                // Get the fingerprint as a byte array; this array is fixed length.
                IndigoObject structure = CreateIndigoStructure(indigo);
                byte[] fingerprint = structure.fingerprint(type).toBuffer();
                structure.Dispose();

                // Loop through the array and record the identified fingerprint positions.
                for (int i = 0; i < fingerprint.Length; i++)
                    if (Convert.ToBoolean(fingerprint[i]))
                        fingerprintPositions.Add(i);
            }

            return fingerprintPositions;
        }

        private IndigoObject CreateIndigoStructure(Indigo indigo)
        {
            // Load the molfile as an Indigo object and set basic properties.
            IndigoObject structure = indigo.loadMolecule(this.MolfileContents);
            structure.aromatize();
            structure.clearCisTrans();
            structure.clearStereocenters();

            return structure;
        }
    }
}