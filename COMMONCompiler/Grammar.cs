using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace COMMONCompiler
{
    public class Grammar
    {
        public static List<string> listTextbox = new List<string>();
        public static int line=1;
        public static int countError = 0;
        public void GetLinesListbox(ListBox listText)
        {
            foreach (string item in listText.Items)
            {
                listTextbox.Add(item);
            }
        }
        public void GetError(ListBox listText,ListBox listError)
        {
           GetLinesListbox(listText);
           checkCOMMON(listError);
        }

        public void checkCOMMON(ListBox listError) 
        {
            Regex regex;
            Regex regex2;
            string common = "COMMON";
            foreach (string item in listTextbox)
            {
                string mode = "COMMON";
                int counter = 0;
                countError= 0;
                for (int i=0;i<item.Length; i++)
                {
                    
                    switch(mode)
                    {
                        case "COMMON":
                            if (i < common.Length && common[i] != item[i])

                            {
                                listError.Items.Add("Строка №" + line + " Неправильно введен оператор COMMON");
                                countError++;
                            }
                            if (counter==5) mode = " ";
                            counter++;
                            break;

                        case " ":
                            if (item[i] == ' ') mode = "/1";
                            else listError.Items.Add(TextError(line, item[i], i));
                            break;
                        case "/1":
                            if (item[i] == '/') mode = "name";
                            else listError.Items.Add(TextError(line, item[i], i));
                            break;

                        case "name":
                            regex = new Regex(@"[a-z]|[A-Z]");
                            if (regex.IsMatch(item[i].ToString())) mode = "/2";
                            else listError.Items.Add(TextError(line, item[i], i));
                            break;

                        case "/2":
                            regex = new Regex(@"[a-z]|[A-Z]");
                            if (item[i] == '/') mode = "variableFirstLetter";
                            else if (!regex.IsMatch(item[i].ToString())) listError.Items.Add(TextError(line, item[i], i));
                            break;
                        case "variableFirstLetter":
                            regex = new Regex(@"[a-z]|[A-Z]");
                            if (regex.IsMatch(item[i].ToString())) mode = "variableLetterOrDigit";
                            else if (!regex.IsMatch(item[i].ToString())) listError.Items.Add(TextError(line, item[i], i));
                            break;
                        case "variableLetterOrDigit":
                            regex2 = new Regex(@"[a-z]|[A-Z]|[0-9]|,|;");
                            if (regex2.IsMatch(item[i].ToString()))
                            {
                                if(item[i]==',') mode = "variableFirstLetter";
                                else if(item[i] == ';') mode = "END";
                            }
                            else if (!regex2.IsMatch(item[i].ToString())) listError.Items.Add(TextError(line, item[i], i));
                            break;
                        case "END":
                            listError.Items.Add(TextError(line, item[i], i) + ". Ожидание: конец строки");
                            break;
                    }
                    if (i == item.Length - 1)
                    {
                        switch (mode)
                        {
                            case "COMMON":
                            case " ":
                            case "/1":
                            case "name":
                            case "/2":
                            case "variableFirstLetter":
                            case "variableLetterOrDigit":
                                listError.Items.Add("Строка №" + line + " Не до конца введена строка");
                                listError.Items.Add("Количество ошибок: " + countError);
                                break;
                            default:
                                if(countError==0) listError.Items.Add("Строка №" + line + " Ошибок нет");
                                else listError.Items.Add("Количество ошибок: "+ countError);
                                break;
                        }
                    }
                }
                line++;
            }

        }
        public string TextError(int line, char symbol,int position)
        {
            string errorString= "Строка №" + line + " Неожиданный символ '" + symbol + "' на позиции: " + position;
            countError++;
            return errorString;

        }
    }
}
