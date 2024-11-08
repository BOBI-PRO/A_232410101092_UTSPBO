using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using UTS_PBO;

namespace ContactApp
{
    // Model
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    // Controller
    public class ContactController
    {
        private List<Contact> contacts = new List<Contact>();
        private int nextId = 1;

        public List<Contact> GetContacts()
        {
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            contact.Id = nextId++;
            contacts.Add(contact);
        }

        public void UpdateContact(Contact contact)
        {
            var existingContact = contacts.Find(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
            }
        }

        public void DeleteContact(int id)
        {
            var contact = contacts.Find(c => c.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
            }
        }
    }

    // Welcome Form
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            var contactForm = new ContactForm();
            contactForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.btnContact = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnContact
            // 
            this.btnContact.Location = new System.Drawing.Point(12, 12);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(100, 23);
            this.btnContact.TabIndex = 0;
            this.btnContact.Text = "Halaman Kontak";
            this.btnContact.UseVisualStyleBackColor = true;
            this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 41);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Halaman Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // WelcomeForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnContact);
            this.Name = "WelcomeForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnContact;
        private System.Windows.Forms.Button btnLogin;
    }

    // Contact Form
    public partial class ContactForm : Form
    {
        private ContactController contactController;

        public ContactForm()
        {
            InitializeComponent();
            contactController = new ContactController();
            LoadContacts();
        }

        private void LoadContacts()
        {
            listViewContacts.Items.Clear();
            foreach (var contact in contactController.GetContacts())
            {
                var listViewItem = new ListViewItem(contact.Id.ToString());
                listViewItem.SubItems.Add(contact.Name);
                listViewItem.SubItems.Add(contact.Email);
                listViewItem.SubItems.Add(contact.PhoneNumber);
                listViewContacts.Items.Add(listViewItem);
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            var contact = new Contact
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhone.Text
            };
            contactController.AddContact(contact);
            LoadContacts();
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.listViewContacts = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 0;
            this.txtName.PlaceholderText = "Nama";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(12, 38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.PlaceholderText = "Email";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(12, 64);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 2;
            this.txtPhone.PlaceholderText = "Nomor Telepon";
            // 
            // btnAddContact
            // 
            this.btnAddContact.Location = new System.Drawing.Point(12, 90);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(100, 23);
            this.btnAddContact.TabIndex = 3;
            this.btnAddContact.Text = "Tambah Kontak";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // listViewContacts
            // 
            this.listViewContacts.Location = new System.Drawing.Point(12, 119);
            this.listViewContacts.Name = "listViewContacts";
            this.listViewContacts.Size = new System.Drawing.Size(260, 130);
            this.listViewContacts.TabIndex = 4;
            this.listViewContacts.View = System.Windows.Forms.View.Details;
            this.listViewContacts.Columns.Add("ID", 30);
            this.listViewContacts.Columns.Add("Nama", 100);
            this.listViewContacts.Columns.Add("Email", 100);
            this.listViewContacts.Columns.Add("Telepon", 100);
            // 
            // ContactForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listViewContacts);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtName);
            this.Name = "ContactForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.ListView listViewContacts;
    }

    // Login Form
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "admin" && txtPassword.Text == "password")
            {
                var adminDashboard = new AdminDashboardForm();
                adminDashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau password salah.");
            }
        }

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(12, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.PlaceholderText = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.PlaceholderText = "Password";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 64);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 23);
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
    }

    // Admin Dashboard Form
    public partial class AdminDashboardForm : Form
    {
        private ContactController contactController;

        public AdminDashboardForm()
        {
            InitializeComponent();
            contactController = new ContactController();
            LoadContacts();
        }

        private void LoadContacts()
        {
            listViewContacts.Items.Clear();
            foreach (var contact in contactController.GetContacts())
            {
                var listViewItem = new ListViewItem(contact.Id.ToString());
                listViewItem.SubItems.Add(contact.Name);
                listViewItem.SubItems.Add(contact.Email);
                listViewItem.SubItems.Add(contact.PhoneNumber);
                listViewContacts.Items.Add(listViewItem);
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            var contact = new Contact
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhone.Text
            };
            contactController.AddContact(contact);
            LoadContacts();
            ClearForm();
        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            if (listViewContacts.SelectedItems.Count > 0)
            {
                var selectedItem = listViewContacts.SelectedItems[0];
                var contact = new Contact
                {
                    Id = int.Parse(selectedItem.Text),
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text
                };
                contactController.UpdateContact(contact);
                LoadContacts();
                ClearForm();
            }
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (listViewContacts.SelectedItems.Count > 0)
            {
                var selectedItem = listViewContacts.SelectedItems[0];
                int contactId = int.Parse(selectedItem.Text);
                contactController.DeleteContact(contactId);
                LoadContacts();
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnUpdateContact = new System.Windows.Forms.Button();
            this.btnDeleteContact = new System.Windows.Forms.Button();
            this.listViewContacts = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 0;
            this.txtName.PlaceholderText = "Nama";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(12, 38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.PlaceholderText = "Email";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(12, 64);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 2;
            this.txtPhone.PlaceholderText = "Nomor Telepon";
            // 
            // btnAddContact
            // 
            this.btnAddContact.Location = new System.Drawing.Point(12, 90);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(100, 23);
            this.btnAddContact.TabIndex = 3;
            this.btnAddContact.Text = "Tambah Kontak";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // btnUpdateContact
            // 
            this.btnUpdateContact.Location = new System.Drawing.Point(118, 90);
            this.btnUpdateContact.Name = "btnUpdateContact";
            this.btnUpdateContact.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateContact.TabIndex = 4;
            this.btnUpdateContact.Text = "Update Kontak";
            this.btnUpdateContact.UseVisualStyleBackColor = true;
            this.btnUpdateContact.Click += new System.EventHandler(this.btnUpdateContact_Click);
            // 
            // btnDeleteContact
            // 
            this.btnDeleteContact.Location = new System.Drawing.Point(224, 90);
            this.btnDeleteContact.Name = "btnDeleteContact";
            this.btnDeleteContact.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteContact.TabIndex = 5;
            this.btnDeleteContact.Text = "Hapus Kontak";
            this.btnDeleteContact.UseVisualStyleBackColor = true;
            this.btnDeleteContact.Click += new System.EventHandler(this.btnDeleteContact_Click);
            // 
            // listViewContacts
            // 
            this.listViewContacts.Location = new System.Drawing.Point(12, 119);
            this.listViewContacts.Name = "listViewContacts";
            this.listViewContacts.Size = new System.Drawing.Size(400, 130);
            this.listViewContacts.TabIndex = 6;
            this.listViewContacts.View = System.Windows.Forms.View.Details;
            this.listViewContacts.Columns.Add("ID", 30);
            this.listViewContacts.Columns.Add("Nama", 100);
            this.listViewContacts.Columns.Add("Email", 100);
            this.listViewContacts.Columns.Add("Telepon", 100);
            // 
            // AdminDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(424, 261);
            this.Controls.Add(this.listViewContacts);
            this.Controls.Add(this.btnDeleteContact);
            this.Controls.Add(this.btnUpdateContact);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtName);
            this.Name = "AdminDashboardForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnUpdateContact;
        private System.Windows.Forms.Button btnDeleteContact;
        private System.Windows.Forms.ListView listViewContacts;
    }

    // Main Program
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }
}