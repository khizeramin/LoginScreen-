using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace LoginScreen
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public int count;
        public login()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection DBCon = new SqlConnection("Data Source=(local); Initial Catalog=TacticalDB; Integrated Security=True;");
            try
            {
                if (DBCon.State == ConnectionState.Closed)
                    DBCon.Open();
                    
                string query = "SELECT COUNT(2) FROM LoginTB WHERE Username=@Username AND Password=@Password";
                SqlCommand command = new SqlCommand(query, DBCon);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Username", UserTxt.Text);
                command.Parameters.AddWithValue("@Password", PassTxt.Password);
                count = Convert.ToInt32(command.ExecuteScalar());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                if (count == 1)
                {   
                    MainWindow window = new MainWindow();
                    window.Show();
                    Console.WriteLine(dt.Columns[0].ColumnName.ToString());
                    Console.WriteLine(dt.Rows[1].ItemArray.ToString());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password.");                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBCon.Close();
            }
        }
    }
}
