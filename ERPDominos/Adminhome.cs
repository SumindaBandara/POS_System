using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ERPDominos
{
    public partial class Adminhome : Form
    {
        public Adminhome()
        {
            InitializeComponent();
        }


        private List<Pizza> GetPizzasFromDatabase()
        {
            List<Pizza> pizzas = new List<Pizza>();
            string connectionString = "Server=localhost;Port=3306;Database=pizzadb;Uid=root;Pwd=;"; // Replace with your connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM pizzamenu";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pizzas.Add(new Pizza
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                RegularPrice = reader.GetInt32("RegularPrice"),
                                MediumPrice = reader.GetInt32("MediumPrice"),
                                LargePrice = reader.GetInt32("LargePrice")
                            });
                        }
                    }
                }

                return pizzas;
            }
        }
                
    

    // Define a class to hold pizza data
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegularPrice { get; set; }
        public int MediumPrice { get; set; }
        public int LargePrice { get; set; }
    }



    private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Adminhome_Load(object sender, EventArgs e)
        {
            List<Pizza> pizzas = GetPizzasFromDatabase();
            int x = 230, y = 140;
            int panelWidth = 170; // Panel width
            int panelHeight = 150; // Panel height
            int spacing = 10; // Spacing between panels
            int itemsPerRow = 3; // Number of items per row

            for (int i = 0; i < pizzas.Count; i++)
            {
                var pizza = pizzas[i];

                // Create a panel for each pizza item
                Panel panel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    BackColor = Color.LightGray,
                    Location = new Point(x, y),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Create label for pizza name
                Label label = new Label
                {
                    Text = pizza.Name,
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                // Create radio buttons for sizes
                RadioButton radioRegular = new RadioButton
                {
                    Text = $"Regular (${pizza.RegularPrice})",
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Tag = pizza.RegularPrice
                };
                RadioButton radioMedium = new RadioButton
                {
                    Text = $"Medium (${pizza.MediumPrice})",
                    Location = new Point(10, 60),
                    AutoSize = true,
                    Tag = pizza.MediumPrice
                };
                RadioButton radioLarge = new RadioButton
                {
                    Text = $"Large (${pizza.LargePrice})",
                    Location = new Point(10, 80),
                    AutoSize = true,
                    Tag = pizza.LargePrice
                };

                // Create a textbox for quantity input
                TextBox quantityBox = new TextBox
                {
                    Location = new Point(10, 110),
                    Size = new Size(50, 20)
                };

                // Create an 'Add' button
                Button addButton = new Button
                {
                    Text = "Add",
                    Location = new Point(70, 110),
                    Tag = pizza.Name
                };

                // Add the click event handler for the button
                addButton.Click += (s, args) =>
                {
                    string size = radioRegular.Checked ? "Regular" :
                                  radioMedium.Checked ? "Medium" :
                                  radioLarge.Checked ? "Large" : null;

                    if (size != null && int.TryParse(quantityBox.Text, out int quantity))
                    {
                        int price = size == "Regular" ? pizza.RegularPrice :
                                    size == "Medium" ? pizza.MediumPrice :
                                    pizza.LargePrice;

                        AddToTable(pizza.Name, size, quantity, price);
                    }
                    else
                    {
                        MessageBox.Show("Please select a size and enter a valid quantity.");
                    }
                };

                // Add components to the panel
                panel.Controls.Add(label);
                panel.Controls.Add(radioRegular);
                panel.Controls.Add(radioMedium);
                panel.Controls.Add(radioLarge);
                panel.Controls.Add(quantityBox);
                panel.Controls.Add(addButton);

                // Add the panel to the form
                this.Controls.Add(panel);

                // Update the position for the next panel
                x += panelWidth + spacing;
                if ((i + 1) % itemsPerRow == 0)
                {
                    x = 230; // Reset to the left margin
                    y += panelHeight + spacing; // Move down to the next row
                }
            }



        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Adminlog adminlog = new Adminlog();
            adminlog.Show();
            this.Hide();
        }
        private void AddToTable(string pizzaName, string size, int quantity, int price)
        {
            // Assuming your DataGridView is named "dataGridView1"
            // Add the selected pizza details to the DataGridView
            int total = price * quantity;
            dataGridView1.Rows.Add(pizzaName, size, price, quantity, total);
            UpdateSubtotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }
        private string GetSelectedSize(RadioButton regular, RadioButton medium, RadioButton large)
        {
            if (regular.Checked)
                return "Regular";
            if (medium.Checked)
                return "Medium";
            if (large.Checked)
                return "Large";
            return null;
        }

        private int GetPrice(string pizzaName, string size)
        {
            // Define your pricing logic here based on pizzaName and size
            
            // Add similar logic for other pizzas

            return 0; // Default price
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

            // Display the subtotal in the corresponding TextBox
            txtSubtotal.Text = subtotal.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Kitchen kitchen = new Kitchen();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow) 
                {
                    
                    string pizzaName = row.Cells[0].Value.ToString();
                    string size = row.Cells[1].Value.ToString();
                    string price = row.Cells[2].Value.ToString();
                    string quantity = row.Cells[3].Value.ToString();
                    string total = row.Cells[4].Value.ToString();

                    
                    kitchen.AddRowToTable(pizzaName, size, price, quantity, total);
                }
            }

            string subtotal = txtSubtotal.Text;
            kitchen.Show();
            this.Hide();
        }

        private void cheese_Click(object sender, EventArgs e)
        {
            
        }

        private void roastchicken_Click(object sender, EventArgs e)
        {
           
        }

        private void mushroom_Click(object sender, EventArgs e)
        {
            
        }

        private void bbq_Click(object sender, EventArgs e)
        {
            
        }

        private void burger_Click(object sender, EventArgs e)
        {
            
        }

        private void sosage_Click(object sender, EventArgs e)
        {
            
        }

        private void egg_Click(object sender, EventArgs e)
        {
           
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void vegetable_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Adminlog adminlog = new Adminlog();
            adminlog.Show();
            this.Hide();
        }
    }
}
