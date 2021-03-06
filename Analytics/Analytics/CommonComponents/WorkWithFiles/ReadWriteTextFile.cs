﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Analytics.CommonComponents.Exceptions;

namespace Analytics
{
    class ReadWriteTextFile
    {
        public static bool Write_to_file(List<string> buf_of_file_lines, 
            string file_path, int action)
        {
            try
            {
                FileMode fm = FileMode.Create;
                if (action == 0)
                {
                    fm = FileMode.Append;
                }
                if (action == 1)
                {
                    fm = FileMode.Create;
                }
                if (action == 2)
                {
                    fm = FileMode.CreateNew;
                }
                if (action == 3)
                {
                    fm = FileMode.Open;
                }
                if (action == 4)
                {
                    fm = FileMode.OpenOrCreate;
                }
                if (action == 5)
                {
                    fm = FileMode.Truncate;
                }
                FileStream file1 = new FileStream(file_path, fm); //создаем файловый поток
                //создаем «потоковый писатель» и связываем его с файловым потоком 
                StreamWriter writer = new StreamWriter(file1, Encoding.UTF8); 
                for (int i = 0; i < buf_of_file_lines.Count; i++)
                {
                    writer.WriteLine(buf_of_file_lines.ElementAt(i)); //записываем в файл
                }
                writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
                return true;
            }
            catch (Exception ex)
            {
                throw new ErrorWithFile("The error with the file: " + file_path);
            }
        }

        //проверка, существует ли файл или можно ли его открыть
        public static bool testExistFile(string filePath)
        {
            try
            {
                FileStream file1 = new FileStream(filePath, FileMode.Open);
                file1.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //обычное чтение файла
        public static List<string> Read_from_file(string file_path)
        {
            List<string> buf_of_file_lines = new List<string>();
            try
            {
                FileStream file1 = new FileStream(file_path, FileMode.Open); //создаем файловый поток
                // создаем «потоковый читатель» и связываем его с файловым потоком 
                StreamReader reader = new StreamReader(file1, Encoding.UTF8);
                while (reader.EndOfStream == false)
                {
                    buf_of_file_lines.Add(reader.ReadLine()); //считываем все данные с потока
                }
                reader.Close(); //закрываем поток
                return buf_of_file_lines;
            }
            catch (Exception ex)
            {
                throw new ErrorWithFile("The error with the file: " + file_path);
            }
        }
    }
}
/*
 * Модуль для работы с файлами, для корректной работы файлы должны быть в кодировке utf8!
 * При успешной записи в файл функция Write_to_file вернет "true", иначе "false".
 * При успешном чтении из файла функция вернет список строк, иначе вернет список, в котором будет одна
 * строка со значением:"Ошибка чтения, файл не существует или не доступен!".
 * При записи в файл, одним из параметров является пареметр action, который принимает значение от 0 
 * до 5 и задает режим записи и создания файла
 */
