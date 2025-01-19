using System.Windows;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32; // Import pentru SaveFileDialog
using Document = iTextSharp.text.Document; // Specificăm utilizarea `iTextSharp.text.Document`

namespace ProductLifecycleManagement.Views
{
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
        }

        private void GenerateProductReport_Click(object sender, RoutedEventArgs e)
        {
            // Logica pentru generarea raportului de produse
            ReportOutputListBox.Items.Add("Product report generated.");
        }

        private void GenerateStageReport_Click(object sender, RoutedEventArgs e)
        {
            // Logica pentru generarea raportului de etape
            ReportOutputListBox.Items.Add("Stage report generated.");
        }

        private void GenerateMaterialReport_Click(object sender, RoutedEventArgs e)
        {
            // Logica pentru generarea raportului de materiale
            ReportOutputListBox.Items.Add("Material report generated.");
        }

        private void GenerateBOMReport_Click(object sender, RoutedEventArgs e)
        {
            // Logica pentru generarea raportului de BOM-uri
            ReportOutputListBox.Items.Add("BOM report generated.");
        }

        private void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            // Deschide dialogul pentru selectarea locației și a numelui fișierului PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Report.pdf", // Nume implicit pentru fișier
                DefaultExt = ".pdf", // Extensie implicită
                Filter = "PDF files (.pdf)|*.pdf" // Filtru pentru fișiere PDF
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;

                // Logica pentru generarea PDF-ului
                Document pdfDoc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
                pdfDoc.Open();

                foreach (var item in ReportOutputListBox.Items)
                {
                    pdfDoc.Add(new Paragraph(item.ToString()));
                }

                pdfDoc.Close();
                writer.Close();

                MessageBox.Show($"PDF report generated at {filePath}", "Reports", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }
    }
}
