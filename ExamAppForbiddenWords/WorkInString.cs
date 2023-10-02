using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppForbiddenWords
{
    public class WorkInString // класс для работы со строками
    {
        string strInput;
        public string strOutput;
        List<string> list;
        char[] charArray = new char []{ '!', ' ', '.', '?', ',', ';', ':', '-', '/', '\\', '`', '~', '+', '-', '*', '#', '$', '^', '(', ')', '%', '@', '№', '"', '\n' , '\r' };
        public WorkInString(string? strInput)
        {
            if (strInput != null)
                this.strInput = strInput;
            else
                strOutput = "Пришла пустая строка";
        }

        public List<string> GetListWords()
        {
            list = new List<string>();
            string[] words = strInput.Split(new char[] { '!', ' ', '.', '?', ',', ';', ':', '-', '/', '\\', '`', '~', '+', '-', '*', '#', '$', '^', '(', ')', '%', '@', '№', '"', '\n', '\r' });
            for (int i = 0; i < words.Length; i++)
            {
                if(words[i] != "")
                    list.Add(words[i]);
                /* bool yes=false;

                 for(int j=0; j < words[i][j].Length; j++)
                 {
                     if (words[i][j] == ' ')
                         yes = true;


                 }
                 if(!yes)*/

            }
            
            return list;
        }

        






    }
}
