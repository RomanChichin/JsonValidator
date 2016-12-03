using System;
using System.Text.RegularExpressions;

namespace JsonAnalyzer
{
    public class JsonAnalyzer
    {

        /// <summary>
        /// Проверка JSON текста на валидность.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static bool IsValidJson(string jsonString)
        {
            //Проверка входных данных.
            if (jsonString == null || jsonString == string.Empty)
            {
                return false;
            }

            //Если строка начинается на { и заканчивается на }, то начать проверку
            //в противном случае в файле ошибка.
            if (jsonString[0] == '{'
             && jsonString[jsonString.Length - 1] == '}')
            {
                // Объявляем объект класса Regex, который будет работать с шаблонами.
                Regex regex;
                //Количество раз которое будет произведена итеративная проверка.
                byte trustNumberOfInspections = 10;

                //Убираем из файла все лишнее(переносы строки, пробелы итд)
                var analyzedText = jsonString.Replace(" ", string.Empty)
                                             .Replace("\n", string.Empty)
                                             .Replace("\r", string.Empty);

                //Шаблоны, которые могут встетиться в json файле.
                var basePattern = "\"(\\w|@|\\.|:|;|/|\\-|\\+|=|\\?|\\*|\\^|\\$|!|`|'|&|<|>|,|%|#|\\(|\\))+\"";

                var bracketsCheckPattern = "(\\{\\},)*(\\{\\}){1}";

                var commaCheckPattern = "((" + "\"(\\w|@|\\.|;|/|\\-|\\+|=|\\?|\\*|\\^|\\$|!|`|'|&|<|>|,|%|#|\\(|\\))+\"" 
                    + "|false|true|null|\\]|\\}|:(\\d|\\.)+|:\"\")" + "(" + basePattern + "|\\{|\\[))" 
                    + "|(,(\\}|\\]))";

                string[] simplePatterns = {
                       basePattern+":"+basePattern+",?",
                       basePattern+":"+"(\\d|\\.)+" + ",?",
                       basePattern+":"+"(true|false|null|\"\")" + ",?",
                       "null,?"
                    };

                string[] compositePatterns = {
                       basePattern + ":" + "\\[" + "("+basePattern + ",)*" + "("+basePattern + ")?" + "\\]" + ",?",
                       basePattern + ":" + "\\[(\\{\\},)*(\\{\\}){1}\\]" + ",?",
                       basePattern + ":" + "(\\[\\]|\\{\\})" + ",?",
                       basePattern + ":" + "\\{(\\{\\},)*(\\{\\}){1}\\}" + ",?",
                     };

                //Проверяем правильность расстановки запятых в строке
                if (Regex.IsMatch(analyzedText, commaCheckPattern))
                {
                    return false;
                }

                //Убираем из строки все элементы, которые соответствуют простым шаблонам
                foreach (var pattern in simplePatterns)
                {
                    regex = new Regex(pattern);
                    analyzedText = regex.Replace(analyzedText, string.Empty);
                }

                //Убираем из строки все элементы, которые соответствуют составным шаблонам
                for (int i = 0; i < trustNumberOfInspections; i++)
                {
                    foreach (var pattern in compositePatterns)
                    {
                        regex = new Regex(pattern);
                        analyzedText = regex.Replace(analyzedText, string.Empty);
                    }
                }

                //Убираем из строки скобки, которые могли остаться в случае, если было несколько объектов
                regex = new Regex(bracketsCheckPattern);
                analyzedText = regex.Replace(analyzedText, string.Empty);

                //Если на выходе строка пуста или состоит только из {}, значит файл прошел проверку на валидность.
                if (analyzedText == "{}" || analyzedText == string.Empty)
                {
                    return true;
                }
                //Если остались какие-то еще символы, значит в файле есть ошибка.
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
