using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPDominos
{
    public partial class Admin : Form
    {
        private string connectionString = "Server=localhost;Port=3306;Database=pizzadb;Uid=root;Pwd=;";
        public Admin()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void searchitem_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            if (string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT RegularPrice, MediumPrice, LargePrice FROM pizzamenu WHERE Name = @Name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", itemName);

                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtRegularPrice.Text = reader["RegularPrice"].ToString();
                        txtMediumPrice.Text = reader["MediumPrice"].ToString();
                        txtLargePrice.Text = reader["LargePrice"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void updateprice_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            if (string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            if (int.TryParse(txtRegularPrice.Text, out int regularPrice) &&
                int.TryParse(txtMediumPrice.Text, out int mediumPrice) &&
                int.TryParse(txtLargePrice.Text, out int largePrice))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE pizzamenu 
                             SET RegularPrice = @RegularPrice, 
                                 MediumPrice = @MediumPrice, 
                                 LargePrice = @LargePrice 
                             WHERE Name = @Name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", itemName);
                    cmd.Parameters.AddWithValue("@RegularPrice", regularPrice);
                    cmd.Parameters.AddWithValue("@MediumPrice", mediumPrice);
                    cmd.Parameters.AddWithValue("@LargePrice", largePrice);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Prices updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update prices. Item may not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter valid prices.");
            }
        }

        private void searchamount_Click(object sender, EventArgs e)
        {
            string stock = stockname.Text.Trim();
            if (string.IsNullOrEmpty(stock))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Regular, Medium, Large FROM kitchen WHERE Name = @Name";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", stock);

                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        regularamount.Text = reader["Regular"].ToString();
                        mediumamount.Text = reader["Medium"].ToString();
                       largeamount.Text = reader["Large"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void updateamount_Click(object sender, EventArgs e)
        {
            string stock = stockname.Text.Trim();
            if (string.IsNullOrEmpty(stock))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            if (int.TryParse(regularamount.Text, out int regularamounti) &&
                int.TryParse(mediumamount.Text, out int mediumamounti) &&
                int.TryParse(largeamount.Text, out int largeamounti))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE kitchen 
                             SET Regular = @Regular, 
                                 Medium = @Medium, 
                                 Large = @Large 
                             WHERE Name = @Name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", stock);
                    cmd.Parameters.AddWithValue("@Regular", regularamounti);
                    cmd.Parameters.AddWithValue("@Medium", mediumamounti);
                    cmd.Parameters.AddWithValue("@Large", largeamounti);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Amount updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update prices. Item may not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter valid prices.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Adminhome adminhome = new Adminhome();
            adminhome.Show();
            this.Hide();
        }
    }
}
    

