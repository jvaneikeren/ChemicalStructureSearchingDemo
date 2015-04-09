using ChemicalStructureSearchingDemo.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemicalStructureSearchingDemo.SearcherClient
{
    public partial class MainForm : Form
    {
        private ChemicalStructure QueryStructure { get; set; }

        public MainForm()
        {
            InitializeComponent();

            // Allow drop.
            ((Control)pictureBoxQuery).AllowDrop = true; 

            // Preset the index path.
            textBoxIndexPath.Text = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\Files\\Index"));
        }

        private SearchType GetSearchType()
        {
            if (radioButtonSimilarity.Checked)
                return SearchType.Similarity;
            else if (radioButtonSubstructure.Checked)
                return SearchType.Substructure;
            else
                return SearchType.Exact;
        }

        private void pictureBoxQuery_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void pictureBoxQuery_DragDrop(object sender, DragEventArgs e)
        {
            // Read the first dropped molfile as the query structure.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if ((files.Length > 0) && (Path.GetExtension(files[0]).ToLower() == ".mol"))
                {
                    // Hide the instructions.
                    labelInstructions.Visible = false;

                    // Record and set the image.
                    this.QueryStructure = new ChemicalStructure(files[0]);
                    pictureBoxQuery.Image = this.QueryStructure.ToBitmap(pictureBoxQuery.Width, pictureBoxQuery.Height);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear.
                listBoxResults.Items.Clear();
                pictureBoxSelectedResult.Image = null;

                // Search.
                var searcher = new ChemicalStructureSearcher(textBoxIndexPath.Text);
                var results = searcher.Search(this.QueryStructure, GetSearchType());

                // Show results.
                labelResults.Text = "Results (" + results.Count + "):";
                foreach (var result in results)
                    listBoxResults.Items.Add(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedItem != null)
                pictureBoxSelectedResult.Image = ((ChemicalStructureSearchResult)listBoxResults.SelectedItem).ChemicalStructure.ToBitmap(
                    pictureBoxSelectedResult.Width, pictureBoxSelectedResult.Height);
            else
                pictureBoxSelectedResult.Image = null;
        }
    }
}
