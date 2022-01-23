using System;
using System.IO;
using System.Linq;


namespace EnglishLearn
{
   class Program
   {
      static void Main(string[] args)
      {
         int mode = 0;
         Console.WriteLine("Выберите режим теста: \n1. Перевод английский -> русский \n2. Перевод русский -> английский" +
            "\n3. Повторение слов \n4. Импорт слов из файла \n5. (Глаголы) Перевод анлг -> русс" +
            "\n6. (Глаголы) Перевод русс -> англ");
         mode = Convert.ToInt32(Console.ReadLine());
         switch (mode)
         {
            case 1:
               EnglishToTranslate();
               break;
            case 2:
               TranslateToEnglish();
               break;
            case 3:
               RepeatWords();
               break;
            case 4:
               WriteInDB();
               break;
            case 5:
               VerbsEnglishToRussian();
               break;
            case 6:
               VerbsRussianToEnglish();
               break;
         }

         Console.ReadKey();
      }

      static void WriteInDB()
      {
         int numDB;
         string path = string.Empty;
         Console.Write("Введите путь к файлу со словами: ");
         path = Console.ReadLine();
         Console.WriteLine("Выберете базу данных: \n1. Все слова. \n2. Глаголы");
         numDB = int.Parse(Console.ReadLine());

         if (numDB == 1)
         {
            using (StreamReader sr = new StreamReader(path))
            {
               using (englishContext db = new englishContext())
               {
                  string line;
                  while ((line = sr.ReadLine()) != null)
                  {
                     var words = line.Split(new char[] { ' ', ' ' });
                     Word word = new Word() { Word1 = words[0].Trim(), Tranclate = words[1].Trim() };
                     db.Words.Add(word);

                  }
                  db.SaveChanges();
               }

            }
         }
         else
         {
            using (StreamReader sr = new StreamReader(path))
            {
               using (englishContext db = new englishContext())
               {
                  string line;
                  while ((line = sr.ReadLine()) != null)
                  {
                     var words = line.Split(new char[] { ' ', ' ' });
                     Verb verb = new Verb() { EnglishWord = words[0].Trim(), RussianWord = words[1].Trim() };
                     db.Verbs.Add(verb);

                  }
                  db.SaveChanges();
               }

            }
         }
         Console.WriteLine("Перенос слов закочен!");
      }

      static void RepeatWords()
      {
         using (englishContext db = new englishContext())
         {
            int start;
            int end;
            var lastWord = db.Words.OrderBy(w => w.Id).LastOrDefault();
            Console.WriteLine("Всего слов в базе: " + lastWord.Id);
            Console.Write("Введите диапозон: От: ");
            start = Convert.ToInt32(Console.ReadLine());
            start -= 1;
            Console.Write(" До: ");
            end = Convert.ToInt32(Console.ReadLine());
            var words = db.Words.Skip(start).Take(end - start);
            Console.Clear();

            foreach (var word in words)
            {
               Console.WriteLine($"{word.Id}. {word.Word1} - {word.Tranclate}");
            }
         }
      }

      static void TranslateToEnglish()
      {
         using (englishContext db = new englishContext())
         {
            int score = 0;
            int failScore = 0;
            int start;
            int end;
            string translate = string.Empty;
            var lastWord = db.Words.OrderBy(w => w.Id).LastOrDefault();
            Random rand = new Random();
            Console.WriteLine("Всего слов в базе: " + lastWord.Id);
            Console.Write("Введите диапозон: От: ");
            start = Convert.ToInt32(Console.ReadLine());
            Console.Write(" До: ");
            end = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
               int num = rand.Next(start, end);
               var word = db.Words.Where(w => w.Id == num).FirstOrDefault();
               Console.Write(word.Tranclate + " - ");
               translate = Console.ReadLine();
               if (translate == "stop")
               {
                  break;
               }
               var rightWords = db.Words.Where(w => w.Tranclate == word.Tranclate);
               if (!rightWords.Any(w => w.Word1 == translate))
               {
                  failScore++;
                  Console.WriteLine("Неправильно! " + word.Word1);
                  Console.ReadKey();
               }
               else
               {
                  score++;
               }
               Console.Clear();
            }
            Console.WriteLine("Правильных ответов: " + score + "\nОшибок: " + failScore);
         }
      }

      static void EnglishToTranslate()
      {
         using (englishContext db = new englishContext())
         {
            int score = 0;
            int failScore = 0;
            int start;
            int end;
            string translate = string.Empty;
            var lastWord = db.Words.OrderBy(w => w.Id).LastOrDefault();
            Random rand = new Random();
            Console.WriteLine("Всего слов в базе: " + lastWord.Id);
            Console.Write("Введите диапозон: От: ");
            start = Convert.ToInt32(Console.ReadLine());
            Console.Write(" До: ");
            end = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
               int num = rand.Next(start, end);
               var word = db.Words.Where(w => w.Id == num).FirstOrDefault();
               Console.Write(word.Word1 + " - ");
               translate = Console.ReadLine();
               if (translate == "stop")
               {
                  break;
               }
               var rightWords = db.Words.Where(w => w.Word1 == word.Word1);
               if (!rightWords.Any(w => w.Tranclate == translate))
               {
                  failScore++;
                  Console.WriteLine("Неправильно! " + word.Tranclate);
                  Console.ReadKey();
               }
               else
               {
                  score++;
               }
               Console.Clear();
            }
            Console.WriteLine("Правильных ответов: " + score + "\nОшибок: " + failScore);
         }
      }

      static void VerbsEnglishToRussian()
      {
         using (englishContext db = new englishContext())
         {
            int score = 0;
            int failScore = 0;
            int start;
            int end;
            string translate = string.Empty;
            var lastWord = db.Verbs.OrderBy(w => w.Id).LastOrDefault();
            Random rand = new Random();
            Console.WriteLine("Всего слов в базе: " + lastWord.Id);
            Console.Write("Введите диапозон: От: ");
            start = Convert.ToInt32(Console.ReadLine());
            Console.Write(" До: ");
            end = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
               int num = rand.Next(start, end);
               var word = db.Verbs.Where(w => w.Id == num).FirstOrDefault();
               Console.Write(word.EnglishWord + " - ");
               translate = Console.ReadLine();
               if (translate == "stop")
               {
                  break;
               }
               var rightWords = db.Verbs.Where(w => w.EnglishWord == word.EnglishWord);
               if (!rightWords.Any(w => w.RussianWord == translate))
               {
                  failScore++;
                  Console.WriteLine("Неправильно! " + word.RussianWord);
                  Console.ReadKey();
               }
               else
               {
                  score++;
               }
               Console.Clear();
            }
            Console.WriteLine("Правильных ответов: " + score + "\nОшибок: " + failScore);
         }
      }

      static void VerbsRussianToEnglish()
      {
         using (englishContext db = new englishContext())
         {
            int score = 0;
            int failScore = 0;
            int start;
            int end;
            string translate = string.Empty;
            var lastWord = db.Verbs.OrderBy(w => w.Id).LastOrDefault();
            Random rand = new Random();
            Console.WriteLine("Всего слов в базе: " + lastWord.Id);
            Console.Write("Введите диапозон: От: ");
            start = Convert.ToInt32(Console.ReadLine());
            Console.Write(" До: ");
            end = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
               int num = rand.Next(start, end);
               var word = db.Verbs.Where(w => w.Id == num).FirstOrDefault();
               Console.Write(word.RussianWord + " - ");
               translate = Console.ReadLine();
               if (translate == "stop")
               {
                  break;
               }
               var rightWords = db.Verbs.Where(w => w.RussianWord == word.RussianWord);
               if (!rightWords.Any(w => w.EnglishWord == translate))
               {
                  failScore++;
                  Console.WriteLine("Неправильно! " + word.EnglishWord);
                  Console.ReadKey();
               }
               else
               {
                  score++;
               }
               Console.Clear();
            }
            Console.WriteLine("Правильных ответов: " + score + "\nОшибок: " + failScore);
         }
      }
   }
}
