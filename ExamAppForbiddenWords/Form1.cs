using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExamAppForbiddenWords
{
    public delegate bool SearchAllFiles(); // ������� ������ ���� ��������� ������
    public delegate bool SearchAllFilesContainsForbiddenWords(); // ������� ������ ������ ���������� ����������� �����
    public delegate bool CopyFile(); // ������� �������������� � ����������� ������
    public delegate bool PauseDelegate();
    public partial class FormSearchForbiddenWords : Form
    {
        OpenFileDialog openFileDialog;
        string? path;
        string pathSaveFile; // ���� ��� ���������� ����� � ����������� ������������ �������
        List<string>? files;
        List<string> forbiddenWords;
        SearchFileContainForbidden searchFile;
        static bool pause;

        static bool Pause()
        {
            return pause;
        }
        public FormSearchForbiddenWords()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            files = new List<string>();
            forbiddenWords = new List<string>();
            pause = false;

        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Pause")
            {
                buttonStart.Text = "Start";
                pause = true;
            }
            if (buttonStart.Text == "Start")
            {
                buttonStart.Text = "Pause";
                pause = false;
            }
            if(forbiddenWords.Count == 0)
            {
                WorkInString ws = new WorkInString(textBoxForbiddenWords.Text);
                forbiddenWords = ws.GetListWords();
            }
           






            if (forbiddenWords.Count == 0 || pathSaveFile==null)
            {
               
                MessageBox.Show("����������� ����������� � ����� ��� �� ������ ���� ��� ���������� ������ ���������� ����������� �����");
                return;
            }
            else
            {
                searchFile = new SearchFileContainForbidden(new PauseDelegate(Pause));
                try
                {
                    // ������� ������ ���� ������ �� ���� ������� ������ 
                    await Task.Run(() => searchFile.SearchFilesOnDrives(progressBarSearchAllFiles, labelSearchAllFiles));

                    /* for (int i = 0; i < searchFile.files.Count; i++)
                     {
                         if (i % 100 == 0) // ������� ����� ����� ����������
                             textBox1.Text += searchFile.files[i].ToString() + "\r\n";
                     }*/
                    // ������� ������ ������ ���������� ����������� ����� �� ���� ������
                    await Task.Run(() => searchFile.SearchFilesContainsForbiffenWordsAsync(forbiddenWords, pathSaveFile, progressBarContainsForbidden, labelContainsForbidden));

                    // ������� �������������� � ����������� ������ � ��������� �����
                    await Task.Run(() => searchFile.ChangeFileAndCopyAsync(progressBarCopyFiles, labelCopyFiles));
                }
                catch
                {
                    return;// MessageBox.Show(searchFile.exeptions);
                }

            }

        }

        private void buttonLoadForb_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

                using (openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "C:\\Users\\User\\Desktop\\"; // ����� �� ��������� ���������� ������� ����
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileText = System.IO.File.ReadAllText(openFileDialog.FileName);
                        path = openFileDialog.FileName;
                        StringBuilder sb = new StringBuilder();
                        int count = 0;
                        for (int i = 0; i < fileText.Length; i++)
                        {
                            if (count < 1 || fileText[i] != ' ')
                            {
                                sb.Append(fileText[i]);
                                count = 0;
                            }
                            if (fileText[i] == ' ')
                                count++;

                        }
                        fileText = sb.ToString();

                        WorkInString ws = new WorkInString(fileText);
                        forbiddenWords = ws.GetListWords();
                       /* foreach (string forb in forbiddenWords)
                        {
                            listBoxForbid.Items.Add(forb);
                        }*/
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("�������� �������� � ��������� �����" + ex.Message);

            }

        }

        private void buttonPath_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())// SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                fbd.InitialDirectory = "C:\\Users\\User\\Documents\\����� � ������������ �������";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    pathSaveFile = fbd.SelectedPath;

                }

            }

        }


    }
}