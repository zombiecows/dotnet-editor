using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TextPad.Properties;
using System.Text.RegularExpressions;

namespace TextPad
{
    public partial class TextPad : Form
    {
        internal TextPad()
        {
            InitializeComponent();
        }
        private void TextPad_Load(object sender, EventArgs e)
        {
            FontFamilyChoice();
            FontSizeChoice();
            toolStrip_Font.SelectedItem = richTextBox.Font.Name;
            toolStrip_FontSizes.SelectedItem = Int32.Parse(richTextBox.Font.Size.ToString());
        }
        private void TextPad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }
        #region Editor
        private void RichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText); // Enables urls
        }
        #endregion
        #region ToolMenu
        private void ToolStrip_New_Click(object sender, EventArgs e)
        {
            New();
        }
        private void ToolStrip_Open_Click(object sender, EventArgs e)
        {
            Open();
        }
        private void ToolStrip_Save_Click(object sender, EventArgs e)
        {
            MenuSave();
        }
        private void ToolStrip_SaveAs_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void ToolStrip_Cut_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }
        private void ToolStrip_Copy_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }
        private void ToolStrip_Paste_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }
        private void ToolStrip_Bold_Click(object sender, EventArgs e)
        {
            SelectBold();
        }

        private void ToolStrip_Italic_Click(object sender, EventArgs e)
        {
            SelectItalic();
        }
        private void ToolStrip_Underline_Click(object sender, EventArgs e)
        {
            SelectUnderline();
        }

        private void ToolStrip_Strikethrough_Click(object sender, EventArgs e)
        {
            SelectStrikethrough();
        }
        private void ToolStrip_Font_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                int start = richTextBox.SelectionStart;
                int end = richTextBox.SelectionLength;
                for (int i = start; i < (start + end); i++)
                {
                    richTextBox.Select(i, 1);
                    Font font = richTextBox.SelectionFont;
                    font = new Font(toolStrip_Font.Text, font.Size, font.Style);
                    richTextBox.SelectionFont = font;
                }
                richTextBox.Select(start, end);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void ToolStrip_FontSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int start = richTextBox.SelectionStart;
                int end = richTextBox.SelectionLength;
                for (int i = start; i < (start + end); i++)
                {
                    richTextBox.Select(i, 1);
                    Font font = richTextBox.SelectionFont;
                    font = new Font(font.FontFamily, Int32.Parse(toolStrip_FontSizes.SelectedItem.ToString()), font.Style);
                    richTextBox.SelectionFont = font;
                }
                richTextBox.Select(start, end);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        void FontFamilyChoice() // Populate combobox with installed fonts
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                toolStrip_Font.Items.Add(fonts.Families[i].Name);
            }
        }
        void FontSizeChoice() // Populate combobox with number range for fontsize
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 120);
            foreach (int n in numbers)
            {
                toolStrip_FontSizes.Items.Add(n);
            }
        }
        #endregion
        #region RightClickMenu
        private void RightClick_Undo_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void RightClick_Redo_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void RightClick_Cut_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void RightClick_Copy_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void RightClick_Paste_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }
        #endregion
        #region MenuFile
        private void MenuFile_New_Click(object sender, EventArgs e)
        {
            New();
        }

        private void MenuFile_Open_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void MenuFile_Save_Click(object sender, EventArgs e)
        {
            MenuSave();
        }
        private void MenuFile_SaveAs_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void MenuFile_Logout_Click(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion
        #region MenuEdit
        private void MenuEdit_Font_Click(object sender, EventArgs e)
        {
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog.Font;
                toolStrip_Font.SelectedItem = fontDialog.Font.Name;
                toolStrip_FontSizes.SelectedItem = Int32.Parse(fontDialog.Font.Size.ToString());
            }
        }
        private void MenuEdit_Undo_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }
        private void MenuEdit_Redo_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }
        private void MenuEdit_Copy_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }
        private void MenuEdit_Cut_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }
        private void MenuEdit_Paste_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }
        private void MenuEdit_SelectAll_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }
        private void MenuEdit_Bold_Click(object sender, EventArgs e)
        {
            SelectBold();
        }
        private void MenuEdit_Italic_Click(object sender, EventArgs e)
        {
            SelectItalic();
        }
        private void MenuEdit_Underline_Click(object sender, EventArgs e)
        {
            SelectUnderline();
        }
        private void MenuEdit_Strikethrough_Click(object sender, EventArgs e)
        {
            SelectStrikethrough();
        }
        #endregion
        #region Help
        private void MenuHelp_About(object sender, EventArgs e)
        {
            Form ab = new About();
            ab.ShowDialog();
        }
        private void MenuHelp_Instructions(object sender, EventArgs e)
        {
            MessageBox.Show(this, Resources.Instructions, "Instructions");
        }
        #endregion
        #region Methods
        // Contains all the app logic for the editor.
        private string openFileName = ""; // open filepath; used to track if a file exists or not.
        void Save()
        {
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    saveFile.CreatePrompt = true;
                    saveFile.OverwritePrompt = true;
                    var filename = saveFile.FileName;
                    var extension = Path.GetExtension(filename);
                    switch (extension.ToLower())
                    {
                        case ".txt":
                            richTextBox.SaveFile(filename, RichTextBoxStreamType.PlainText);
                            break;
                        case ".rtf":
                            richTextBox.SaveFile(filename, RichTextBoxStreamType.RichText);
                            break;
                        default:
                            richTextBox.SaveFile(filename, RichTextBoxStreamType.RichText);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString() + exc.StackTrace);
                }
            }
        }
        void MenuSave()
        {
            if (!String.IsNullOrEmpty(openFileName))
            {
                try
                {
                    richTextBox.SaveFile(openFileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString() + exc.StackTrace);
                }
            }
            else
            {
                Save();
                openFileName = saveFile.FileName;
            }
        }
        void Open()
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openFileName = openFile.FileName;
                    var extension = Path.GetExtension(openFileName);
                    switch (extension.ToLower())
                    {
                        case ".txt":
                            richTextBox.LoadFile(openFileName, RichTextBoxStreamType.PlainText);
                            break;
                        case ".rtf":
                            richTextBox.LoadFile(openFileName, RichTextBoxStreamType.RichText);
                            break;
                        default:
                            richTextBox.LoadFile(openFileName, RichTextBoxStreamType.RichText);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString() + exc.StackTrace);
                }
            }
        }
        void New()
        {
            richTextBox.Clear();
            openFileName = "";
        }
        void Exit()
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) Environment.Exit(0);
        }
        void FindApplyStyle(bool fs, FontStyle apply)
        {
            int start = richTextBox.SelectionStart;
            int end = richTextBox.SelectionLength;
            for (int i = start; i < (start + end); i++)
            {
                richTextBox.Select(i, 1);
                Font font = richTextBox.SelectionFont;
                if (!fs) font = new Font(font.FontFamily, font.Size, font.Style ^ apply); // Adds font style
                else font = new Font(font.FontFamily, font.Size, font.Style & ~apply); // Removes font style
                richTextBox.SelectionFont = font;
            }
            richTextBox.Select(start, end);
        }
        void SelectBold()
        {
            if (richTextBox.SelectedText.Length == 0) return;
            else FindApplyStyle(richTextBox.SelectionFont.Bold, FontStyle.Bold);
        }
        void SelectItalic()
        {
            if (richTextBox.SelectedText.Length == 0) return;
            else FindApplyStyle(richTextBox.SelectionFont.Italic, FontStyle.Italic);
        }
        void SelectUnderline()
        {
            if (richTextBox.SelectedText.Length == 0) return;
            else FindApplyStyle(richTextBox.SelectionFont.Underline, FontStyle.Underline);
        }
        void SelectStrikethrough()
        {
            if (richTextBox.SelectedText.Length == 0) return;
            else FindApplyStyle(richTextBox.SelectionFont.Strikeout, FontStyle.Strikeout);
        }
        #endregion
        private void Timer_Tick(object sender, EventArgs e) // Keeps track of the word/char counts at the bottom
        {
            charCount.Text = "Chars: " + richTextBox.TextLength.ToString();
            wordCount.Text = "Words: " + Regex.Matches(richTextBox.Text, @"['\w']+").Count.ToString();
        }
    }
}
