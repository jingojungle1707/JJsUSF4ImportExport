using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    public partial class StringNameInput : Form
    {
        private string _name;
        private NameType _type;
        public string NewName
        {
            get
            {
                return _name;
            }
        }

        public StringNameInput(string currentName, NameType type)
        {
            InitializeComponent();
            _type = type;
            _name = currentName;
            tbStringEntry.Text = currentName;
            tbStringEntry.SelectAll();
            lbFeedback.Text = string.Empty;
        }

        public enum NameType
        {
            Material,
            Mesh
        }

     
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string submission = tbStringEntry.Text.Trim();

            //No change, close dialogue even if the material name would fail validation
            if (submission == _name)
            {
                DialogResult = DialogResult.Cancel;
            }
            //Empty string, prompt re-entry
            if (submission == string.Empty)
            {
                tbStringEntry.Focus();
                tbStringEntry.SelectAll();
                lbFeedback.Text = $"{_type} name cannot be blank!";
                return;
            }

            //If string passes those checks, move to type-specific validation
            bool validated = false;
            switch (_type)
            {
                case NameType.Material: 
                    validated = ValidateNewMaterialName(submission);
                    break;
                case NameType.Mesh: 
                    validated = ValidateNewMeshName(submission);
                    break;
                default: break;
            }

            if (validated)
            {
                _name = tbStringEntry.Text.Trim();
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidateNewMaterialName(string submission)
        {
            if (submission.Length > 20)
            {
                tbStringEntry.Focus();
                tbStringEntry.SelectAll();
                lbFeedback.Text = $"Maximum {_type} name length is 20 characters!";
                return false;
            }
            return true;
        }
        private bool ValidateNewMeshName(string submission)
        {
            if (submission.Length > 20)
            {
                tbStringEntry.Focus();
                tbStringEntry.SelectAll();
                lbFeedback.Text = $"Maximum {_type} name length is 20 characters!";
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
