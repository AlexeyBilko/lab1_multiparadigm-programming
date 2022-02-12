using System;
using System.IO;
using System.Text;

namespace task1.lang_with_go_to
{
    class Program
    {
		static void Main(string[] args)
		{
			// слова которые мы не считаем уникальными
			string[] stopWords = {"about", "above", "across", "after", "against", "along", "among", "around", "at", 
								"before", "behind", "below", "beneath", "beside", "besides", "between", "beyond",
								"but", "by", "concerning", "despite", "down", "during", "except", "excepting", "for",
								"from", "in", "inside", "into", "like", "near", "of", "off", "on", "onto", "out",
								"outside", "over", "past", "regarding", "since", "through", "throughout", "to",
								"toward", "under", "underneath", "until", "up", "upon", "up to", "with", "within",
								"without", "it", "is", "are", "am", "the", "a", "an", "this", "that", "those", "theese"};

			string[] rows; // массив строк из текстового файла
			using (StreamReader sr = new StreamReader("data_task1.txt"))
			{
				int counter = 0;
				string row;
				getCountOfRowsLoop:
				if((row = sr.ReadLine()) != null)
				{
					if (row != "")
					{
						counter++;
					}
					goto getCountOfRowsLoop;
				}
				
				rows = new string[counter];
				sr.Close();
			}
			using (StreamReader sr = new StreamReader("data_task1.txt"))
			{
				int counter = 0;
				string row;
				getEachRowFromFileLoop:
				if((row = sr.ReadLine()) != null)
				{
					if (row != "")
					{
						rows[counter] = row;
						counter++;
					}
					goto getEachRowFromFileLoop;
				}
				sr.Close();
			}

			string[] uniqueWords = new string[0]; // массив с уникальными словами
			int[] countOfEachUniqueWord = new int[0]; // массив с кол-вом вхождений каждого слова

			int rowsIterator = 0;
		rowsLoop:

			string[] tmp = rows[rowsIterator].Split(" "); //разделяем строку на слова

			int wordsIterator = 0;
		wordsLoop:

			bool stopWord = false; // является ли данное слово стоп словом, если да - на след. итерацию цикла
			bool ifFound = false; // есть ли это слово в уникальных, если да - обновляем кол-во, если нет - добавляем в уникальные

			int stopWordsIterator = 0;
		stopWordsLoop:

			//к нижнему регистру все символы слова
			char[] tmpStr = tmp[wordsIterator].ToCharArray();
			int tolowerIterator = 0;
			toLowerLoop:
				if (tmpStr[tolowerIterator] >= 65 && tmpStr[tolowerIterator] <= 90)
					tmpStr[tolowerIterator] = (char)(tmpStr[tolowerIterator] + 32);
			tolowerIterator++;
			if (tolowerIterator < tmpStr.Length)
			{
				goto toLowerLoop;
			}
			tmp[wordsIterator] = new string(tmpStr);
			//
			if (stopWords[stopWordsIterator] == tmp[wordsIterator]) //если нашли к следующему слову
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
					if (uniqueWords[ifAlreadyUniqueIterator] == tmp[wordsIterator])
					{
						countOfEachUniqueWord[ifAlreadyUniqueIterator]++;
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

					int[] tmpUniqueWordsCount = new int[countOfEachUniqueWord.Length];
					int countOfUniqueToTmpIterator = 0;

				countOfUniqueToTmpLoop:

					if (tmpUniqueWordsCount.Length != 0)
					{
						tmpUniqueWordsCount[countOfUniqueToTmpIterator] = countOfEachUniqueWord[countOfUniqueToTmpIterator];
					}
					countOfUniqueToTmpIterator++;
					if (countOfUniqueToTmpIterator < tmpUniqueWordsCount.Length)
					{
						goto countOfUniqueToTmpLoop;
					}

					countOfEachUniqueWord = new int[countOfEachUniqueWord.Length + 1];
					int fromTmpToCountOfUniqueIterator = 0;

				fromTmpToCountOfUniqueLoop:

					if (tmpUniqueWordsCount.Length != 0)
					{
						countOfEachUniqueWord[fromTmpToCountOfUniqueIterator] = tmpUniqueWordsCount[fromTmpToCountOfUniqueIterator];
					}
					fromTmpToCountOfUniqueIterator++;
					if (fromTmpToCountOfUniqueIterator < tmpUniqueWordsCount.Length)
					{
						goto fromTmpToCountOfUniqueLoop;
					}
					uniqueWords[uniqueWords.Length - 1] = tmp[wordsIterator];
					countOfEachUniqueWord[countOfEachUniqueWord.Length - 1] = 1;
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

			//сортируем массив слов и их кол-ва по убыванию кол-ва для отображения на экран
			int outerIterator = 0;
		outerLoop:
			int innerIterator = countOfEachUniqueWord.Length - 1;
		innerLoop:
			if (countOfEachUniqueWord[innerIterator] > countOfEachUniqueWord[innerIterator - 1])
			{
				int tmp__ = countOfEachUniqueWord[innerIterator - 1];
				countOfEachUniqueWord[innerIterator - 1] = countOfEachUniqueWord[innerIterator];
				countOfEachUniqueWord[innerIterator] = tmp__;

				string tmp_ = uniqueWords[innerIterator - 1];
				uniqueWords[innerIterator - 1] = uniqueWords[innerIterator];
				uniqueWords[innerIterator] = tmp_;
			}
			innerIterator--;
			if (innerIterator > outerIterator)
			{
				goto innerLoop;
			}
			outerIterator++;
			if (outerIterator < countOfEachUniqueWord.Length)
			{
				goto outerLoop;
			}

			//вывод либо первых 25, либо всех уникальных слов если их меньше 25
			int iter = 0;
			if (uniqueWords.Length >= 25)
				iter = 25;
			else iter = uniqueWords.Length;

			int printIterator = 0;
			printLoop:
				Console.WriteLine(uniqueWords[printIterator] + " - " + countOfEachUniqueWord[printIterator].ToString());
			printIterator++;
			if(printIterator < iter)
            {
				goto printLoop;
            }
		}
    }
}
