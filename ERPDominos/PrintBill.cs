using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPDominos
{
    public partial class PrintBill : Form
    {
        public PrintBill()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        public void AddRowToTable(string pizzaName, string size, string price, string quantity, string total)
        {
            // Add the new row to the DataGridView
            dataGridView1.Rows.Add(pizzaName, size, price, quantity, total);

            // Update the subtotal
            UpdateSubtotal();
        }

        private void subtotal1_TextChanged(object sender, EventArgs e)
        {
        }

        private void UpdateSubtotal()
        {
            int subtotal = 0;

            // Loop through each row in the DataGridView and add the "Total" values
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Avoid the new row placeholder
                {
                    // Check if the cell value is not null before accessing it
                    if (row.Cells[4].Value != null)
                    {
                        subtotal += Convert.ToInt32(row.Cells[4].Value); // Assuming "Total" is in column 4
                    }
                }
            }

            // Display the subtotal in the corresponding TextBox
            subtotal1.Text = subtotal.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trigger the PrintDocument event
            printDocument1.PrintPage += new PrintPageEventHandler(item);

            //  Show the print preview dialog
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument1;
            previewDialog.ShowDialog();

            // Start the print process
            printDocument1.Print();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal amountPaid))
            {
                // Parse the subtotal (assumed to be already displayed)
                decimal subTotal = decimal.Parse(subtotal1.Text);

                // Calculate the balance
                decimal balance = amountPaid - subTotal;

                // Display the balance in the Balance textbox
                balance1.Text = balance.ToString();
            }
            else
            {
                // Clear the balance if the input is invalid
                balance1.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Adminhome ad1 = new Adminhome();
            ad1.Show();
            this.Hide();
        }

        private void item(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Set fonts
            Font fontTitle = new Font("Arial", 24, FontStyle.Bold);
            Font fontSubTitle = new Font("Arial", 16, FontStyle.Regular);
            Font fontContent = new Font("Arial", 14, FontStyle.Regular);

            // Set up margins
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            // Draw Brand Name
            e.Graphics.DrawString("Domino's Pizza", fontTitle, Brushes.Black, x, y);
            y += 40; // Move down

            // Draw Bill Title
            e.Graphics.DrawString("Customer Bill", fontSubTitle, Brushes.Black, x, y);
            y += 30; // Move down

            // Draw separator line
            e.Graphics.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y);
            y += 50;

            // Draw Items Table Header
            e.Graphics.DrawString("Item Name    Size    Price    Quantity    Total", fontContent, Brushes.Black, x, y);
            y += 50; // Move down

            // Draw each item in the bill
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // Check if the cell values are not null before accessing them
                string pizzaName = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "N/A";
                string size = dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "N/A";
                string price = dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? "0";
                string quantity = dataGridView1.Rows[i].Cells[3].Value?.ToString() ?? "0";
                string total = dataGridView1.Rows[i].Cells[4].Value?.ToString() ?? "0";

                string itemLine = $"{pizzaName}    {size}    {price}    {quantity}    {total}";
                e.Graphics.DrawString(itemLine, fontContent, Brushes.Black, x, y);
                y += 50; // Move down for the next item
            }

            // Draw separator line
            y += 20;
            e.Graphics.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y);
            y += 20;

            // Draw Total Amounts (Subtotal, Amount Paid, Balance)
            e.Graphics.DrawString("Sub Total: " + subtotal1.Text, fontContent, Brushes.Black, x, y);
            y += 20;
            e.Graphics.DrawString("Amount Paid: " + textBox2.Text, fontContent, Brushes.Black, x, y);
            y += 20;
            e.Graphics.DrawString("Balance: " + balance1.Text, fontContent, Brushes.Black, x, y);
            y += 30;

            // Draw Thank You Note
            e.Graphics.DrawString("Thank you for your purchase!", fontSubTitle, Brushes.Black, x, y);
        }
    }
}



