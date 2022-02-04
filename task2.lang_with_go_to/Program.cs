using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task2.lang_with_go_to
{
    class Program
    {
        static void Main(string[] args)
        {
			// слова которые мы не считаем уникальными
			string[] stopWords = {"as", "about", "above", "across", "after", "against", "along", "among", "around", "at",
								"before", "behind", "below", "beneath", "beside", "besides", "between", "beyond",
								"but", "by", "concerning", "despite", "down", "during", "except", "excepting", "for",
								"from", "in", "inside", "into", "like", "near", "of", "off", "on", "onto", "out",
								"outside", "over", "past", "regarding", "since", "through", "throughout", "to",
								"toward", "under", "underneath", "until", "up", "upon", "up to", "with", "within",
								"without", "it", "is", "are", "am", "the", "a", "an", "this", "that", "those", "theese"};

            string[] rows; // массив строк из текстового файла
            using (StreamReader sr = new StreamReader("data_task2.txt"))
            {
                int counter = 0;
                string row;
            getCountOfRowsLoop:
                if ((row = sr.ReadLine()) != null)
                {
                    counter++;
                    goto getCountOfRowsLoop;
                }

                rows = new string[counter];
                sr.Close();
            }
            using (StreamReader sr = new StreamReader("data_task2.txt"))
            {
                int counter = 0;
                string row;
            getEachRowFromFileLoop:
                if ((row = sr.ReadLine()) != null)
                {
                    rows[counter] = row;
                    counter++;
                    goto getEachRowFromFileLoop;
                }
                sr.Close();
            }

            string[] uniqueWords = new string[0]; // массив с уникальными словами
			string[] pageOfEachUniqueWord = new string[0]; // массив с страницами каждого уникального слова

			int rowsIterator = 0;
		rowsLoop:

			string[] tmp = rows[rowsIterator].Split(" "); //разделяем строку на слова

			int wordsIterator = 0;
		wordsLoop:

			bool stopWord = false; // является ли данное слово стоп словом, если да - на след. итерацию цикла
			bool ifFound = false; // есть ли это слово в уникальных, если да - обновляем кол-во, если нет - добавляем в уникальные

			int stopWordsIterator = 0;
		stopWordsLoop:

			if (stopWords[stopWordsIterator] == tmp[wordsIterator].ToLower()) //если нашли к следующему слову
			{
				stopWord = true;
			}
			stopWordsIterator++;
			if (stopWordsIterator < stopWords.Length)
			{
				goto stopWordsLoop;
			}
			if (!stopWord) // если данное слово не является стоп словом
			{
				int ifAlreadyUniqueIterator = 0;
			ifAlreadyUniqueLoop:

				if (uniqueWords.Length != 0)
				{
					if (uniqueWords[ifAlreadyUniqueIterator] == tmp[wordsIterator].ToLower()) //если нашли, обновляем кол-во и выходим из массива
					{
						int page = rowsIterator / 45 + 1;
						pageOfEachUniqueWord[ifAlreadyUniqueIterator] += (", " + page.ToString());
						ifFound = true;
						goto IfFoundInUnique;
					}
				}
				ifAlreadyUniqueIterator++;
				if (ifAlreadyUniqueIterator < uniqueWords.Length)
				{
					goto ifAlreadyUniqueLoop;
				}

			IfFoundInUnique:

				if (!ifFound) // если не нашли, добавляем новое слово в массив уникальных слов и устанавливаем его количество как 1
				{
					string[] tmpUniqueWords = new string[uniqueWords.Length];
					int uniqueWordsToTmpIterator = 0;

				uniqueWordsToTmpLoop:

					if (tmpUniqueWords.Length != 0)
					{
						tmpUniqueWords[uniqueWordsToTmpIterator] = uniqueWords[uniqueWordsToTmpIterator];
					}
					uniqueWordsToTmpIterator++;
					if (uniqueWordsToTmpIterator < tmpUniqueWords.Length)
					{
						goto uniqueWordsToTmpLoop;
					}

					uniqueWords = new string[uniqueWords.Length + 1];
					int fromTmpToUniqueWordsIterator = 0;

				fromTmpToUniqueWordsLoop:

					if (tmpUniqueWords.Length != 0)
					{
						uniqueWords[fromTmpToUniqueWordsIterator] = tmpUniqueWords[fromTmpToUniqueWordsIterator];
					}
					fromTmpToUniqueWordsIterator++;
					if (fromTmpToUniqueWordsIterator < tmpUniqueWords.Length)
					{
						goto fromTmpToUniqueWordsLoop;
					}

					string[] tmpUniqueWordsCount = new string[pageOfEachUniqueWord.Length];
					int countOfUniqueToTmpIterator = 0;

				countOfUniqueToTmpLoop:

					if (tmpUniqueWordsCount.Length != 0)
					{
						tmpUniqueWordsCount[countOfUniqueToTmpIterator] = pageOfEachUniqueWord[countOfUniqueToTmpIterator];
					}
					countOfUniqueToTmpIterator++;
					if (countOfUniqueToTmpIterator < tmpUniqueWordsCount.Length)
					{
						goto countOfUniqueToTmpLoop;
					}

					pageOfEachUniqueWord = new string[pageOfEachUniqueWord.Length + 1];
					int fromTmpToCountOfUniqueIterator = 0;

				fromTmpToCountOfUniqueLoop:

					if (tmpUniqueWordsCount.Length != 0)
					{
						pageOfEachUniqueWord[fromTmpToCountOfUniqueIterator] = tmpUniqueWordsCount[fromTmpToCountOfUniqueIterator];
					}
					fromTmpToCountOfUniqueIterator++;
					if (fromTmpToCountOfUniqueIterator < tmpUniqueWordsCount.Length)
					{
						goto fromTmpToCountOfUniqueLoop;
					}

					uniqueWords[uniqueWords.Length - 1] = tmp[wordsIterator].ToLower();
					int page = rowsIterator / 45 + 1;
					if (pageOfEachUniqueWord[pageOfEachUniqueWord.Length - 1] == null)
						pageOfEachUniqueWord[pageOfEachUniqueWord.Length - 1] = page.ToString();
					else pageOfEachUniqueWord[pageOfEachUniqueWord.Length - 1] = ", " + page.ToString();
				}
			}
			wordsIterator++;
			if (wordsIterator < tmp.Length)
			{
				goto wordsLoop;
			}
			rowsIterator++;
			if (rowsIterator < rows.Length)
			{
				goto rowsLoop;
			}
            

			//сортировка по алфавиту
            int outerIterator = 0;
		outerLoop:

			int innerIterator = 0;
		innerLoop:

			bool toReplace = false; // меняем ли мы слова при сортировке
			int lengthToCompare; 
			if(uniqueWords[innerIterator].Length > uniqueWords[innerIterator + 1].Length)
            {
				lengthToCompare = uniqueWords[innerIterator + 1].Length;
			}
			else lengthToCompare = uniqueWords[innerIterator].Length;

			char[] charsFromPrev = Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(uniqueWords[innerIterator + 1]));
			char[] charsFromNext = Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(uniqueWords[innerIterator]));

			int compareCharsIterator = 0;

			compareCharsLoop:

                if (charsFromNext[compareCharsIterator] > charsFromPrev[compareCharsIterator])
                {
                    toReplace = true;
					goto toReplaceLabel;
				}
                if (charsFromNext[compareCharsIterator] < charsFromPrev[compareCharsIterator])
                {
                    toReplace = false;
                    goto toReplaceLabel;
                }
			compareCharsIterator++;
			if(compareCharsIterator < lengthToCompare)
            {
				goto compareCharsLoop;
            }

			toReplaceLabel:

            if (toReplace)
            {
                string tmpUniqueWord = uniqueWords[innerIterator];
                uniqueWords[innerIterator] = uniqueWords[innerIterator + 1];
                uniqueWords[innerIterator + 1] = tmpUniqueWord;

                string tmpPagesOFUniqueWord = pageOfEachUniqueWord[innerIterator];
                pageOfEachUniqueWord[innerIterator] = pageOfEachUniqueWord[innerIterator + 1];
                pageOfEachUniqueWord[innerIterator + 1] = tmpPagesOFUniqueWord;
            }
            innerIterator++;
            if (innerIterator < uniqueWords.Length - outerIterator - 1)
            {
                goto innerLoop;
            }
            outerIterator++;
            if (outerIterator < uniqueWords.Length)
            {
                goto outerLoop;
            }

			//вывод на консоль результатов
			int printIterator = 0;
		printLoop:

			if (pageOfEachUniqueWord[printIterator].Split(", ").Length <= 100)
			{
				Console.WriteLine(uniqueWords[printIterator].ToString() + " - " + pageOfEachUniqueWord[printIterator].ToString());
			}
			printIterator++;
			if (printIterator < uniqueWords.Length)
			{
				goto printLoop;
			}
		}
	}
}
