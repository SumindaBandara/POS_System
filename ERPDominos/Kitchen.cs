using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPDominos
{
    public partial class Kitchen : Form
    {
        public Kitchen()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Kitchen_Load);
        }

        private List<Pizza> GetPizzasFromDatabase()
        {
            List<Pizza> stock = new List<Pizza>();
            string connectionString = "Server=localhost;Port=3306;Database=pizzadb;Uid=root;Pwd=;"; // Replace with your connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM kitchen";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stock.Add(new Pizza
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Regular = reader.GetInt32("Regular"),
                                Medium = reader.GetInt32("Medium"),
                                Large = reader.GetInt32("Large")
                            });
                        }
                    }
                }

                return stock;
            }
        }



        // Define a class to hold pizza data
        public class Pizza
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Regular { get; set; }
            public int Medium { get; set; }
            public int Large { get; set; }
        }
        private void Kitchen_Load(object sender, EventArgs e)
        {
            List<Pizza> stock = GetPizzasFromDatabase();
            int x = 10, y = 10;

            foreach (var item in stock)
            {
                // Panel to hold each pizza's details
                Panel panel = new Panel
                {
                    Size = new Size(250, 150),
                    BackColor = Color.LightGray,// Set a consistent size
                    Location = new Point(x+250, y+130),
                    BorderStyle = BorderStyle.FixedSingle // Optional: Adds a border for better visualization
                };

                // Pizza Name Label
                Label label = new Label
                {
                    Text = item.Name,
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                Label label1 = new Label
                {
                    Text = "Available stock for ",
                    Font = new Font("Arial", 14, FontStyle.Regular),
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                Label label3 = new Label
                {
                    Text = "Regular ",
                    Font = new Font("Arial", 12, FontStyle.Regular),
                    Location = new Point(10, 80),
                    AutoSize = true
                };
                TextBox Box1 = new TextBox
                {
                    Text = item.Regular.ToString(),
                    Location = new Point(10, 110),
                    Size = new Size(60, 30)
                };
                Label label4 = new Label
                {
                    Text = "Medium ",
                    Font = new Font("Arial", 12, FontStyle.Regular),
                    Location = new Point(80, 80),
                    AutoSize = true
                };
                TextBox Box2 = new TextBox
                {
                    Text = item.Medium.ToString(),
                    Location = new Point(80, 110),
                    Size = new Size(60, 30)
                };
                Label label5 = new Label
                {
                    Text = "Large ",
                    Font = new Font("Arial", 12, FontStyle.Regular),
                    Location = new Point(170, 80),
                    AutoSize = true
                };
                TextBox Box3 = new TextBox
                {
                    Text = item.Large.ToString(),
                    Location = new Point(170, 110),
                    Size = new Size(60, 30)
                };










                // Add components to panel
                panel.Controls.Add(label);
                panel.Controls.Add(label1);
                panel.Controls.Add(label3);
                panel.Controls.Add(label4);
                panel.Controls.Add(label5);
                panel.Controls.Add(Box1);
                panel.Controls.Add(Box2);
                panel.Controls.Add(Box3);

                

                // Add panel to the form
                this.Controls.Add(panel);

                // Update position for the next panel
                x += panel.Width + 10; // Add spacing between panels
                if (x + panel.Width > this.Width) // Move to the next row if exceeding form width
                {
                    x = 10; // Reset X position
                    y += panel.Height + 10; // Move Y position for the next row
                }
            }

        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void AddRowToTable(string pizzaName, string size, string price, string quantity, string total)
        {
            // Assuming the DataGridView in the second form is named "dataGridViewAvailability"
            dataGridView1.Rows.Add(pizzaName, size, price, quantity, total);

            UpdateSubtotal();
        }
        private void UpdateSubtotal()
        {
            int subtotal = 0;

            // Loop through each row in the DataGridView and add the "Total" values
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Avoid the new row placeholder
                {
                    subtotal += Convert.ToInt32(row.Cells[4].Value); // Assuming "Total" is in column 4
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintBill printBill = new PrintBill();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) // Avoid adding the empty new row
                {
                    // Extract data from the current form's DataGridView
                    string pizzaName = row.Cells[0].Value.ToString();
                    string size = row.Cells[1].Value.ToString();
                    string price = row.Cells[2].Value.ToString();
                    string quantity = row.Cells[3].Value.ToString();
                    string total = row.Cells[4].Value.ToString();

                    // Add the data to the second form's DataGridView
                    printBill.AddRowToTable(pizzaName, size, price, quantity, total);
                }
            }

            
            printBill.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adminhome adminhome = new Adminhome();
            adminhome.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
