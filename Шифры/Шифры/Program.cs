using System;
using System.IO;

namespace Шифры
{
    class Program
    {
        static int Incorrect_value(string value)
        {
            while (true)
            {
                try
                {
                    Convert.ToInt32(value);
                    break;
                }
                catch
                {
                    Console.WriteLine("Данные введены некорректно, введите снова");
                    value = Console.ReadLine();
                }
            }
            return (Convert.ToInt32(value));
        }
        static void Crypt_Move(string Text_Name, string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            string line = key.ReadLine();
            Random rnd = new Random();
            int Key_size = (File.ReadAllLines(key_Name).Length - 2);
            if (line.Contains("шифр перестановки"))
            {
                key.ReadLine();
                line = key.ReadLine();
                string[] key_string = new string[Key_size];

                for (int i = 0; i < Key_size; i++)
                {
                    key_string[i] = line;
                    line = key.ReadLine();
                }
                int[] KEY = new int[key_string.Length];
                for (int i = 0; i < key_string.Length; i++)
                {
                    KEY[i] = Convert.ToInt32(key_string[i]);
                }
                int Text_sise = text.Length;
                while (Text_sise % Key_size != 0) { text += text[rnd.Next(0, Text_sise)]; Text_sise++; };
                Console.WriteLine("Введите путь к файлу, в который сохранить шифртекст,должен содержать расширение '.encode'");
                string СrText = Console.ReadLine();
                while (Path.GetExtension(СrText) != ".encode")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой файл");
                    СrText = @Console.ReadLine();
                }
                using (StreamWriter writer = File.CreateText(СrText))
                {
                    Console.WriteLine("Шифротекст был сохранён в файле " + СrText);
                    writer.WriteLine("alg: шифр перестановки\nтекст:");
                    int check = 0;
                    while (check < Text_sise)
                    {
                        char[] block = new char[Key_size];

                        for (int j = 0; j < Key_size; j++)
                            block[KEY[j] - 1] = text[check + j];

                        for (int j = 0; j < Key_size; j++)
                            writer.Write(block[j]);
                        check += Key_size;
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Decrypt_Move(string Text_Name,string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string check_text = Text.ReadLine();
            Text.ReadLine();
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            int Key_size = File.ReadAllLines(key_Name).Length-2;
            string line = key.ReadLine();
            if (line.Contains("шифр перестановки") && check_text.Contains("шифр перестановки"))
            {
                key.ReadLine();
                line = key.ReadLine();
                string[] key_string = new string[Key_size];

                   for (int i = 0; i < Key_size; i++) {
                    key_string[i]= line;
                    line = key.ReadLine();
                }
                int[] KEY = new int[key_string.Length];
                for (int i = 0; i < key_string.Length; i++)
                {
                    KEY[i] = Convert.ToInt32(key_string[i]);
                }
                int Text_sise = text.Length;
                while (Text_sise % KEY.Length != 0) { text += ".";Text_sise++; };
                Console.WriteLine("Введите путь к файлу, в который сохранить текст");
                string СrText = Console.ReadLine();
                using (StreamWriter writer = File.CreateText(СrText))
                {
                    Console.WriteLine("Текст был сохранён в файле " + СrText);
                    writer.WriteLine("alg: шифр перестановки\nтекст:");
                    for (int i = 0; i < Text_sise; i += Key_size)
                    {
                        char[] block = new char[Key_size];

                        for (int j = 0; j < Key_size; j++)
                            block[j] = text[i + KEY[j] - 1];

                        for (int j = 0; j < Key_size; j++)
                            writer.Write(block[j]);
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Crypt_Swap(string Text_Name, string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            string line = key.ReadLine();
            if (line.Contains("шифр замены"))
            {
                key.ReadLine();
                line = key.ReadLine();
                int a = File.ReadAllLines(key_Name).Length - 2;
                string[] old_alph = new string[a];
                string[] new_alph = new string[a];
                char[] text_array = text.ToCharArray();
                bool check = false;
                for (int k = 0; k < old_alph.Length; k++)
                {
                    if (line.Contains(" - ")) old_alph[k] = line.Substring(line.IndexOf('-') - 3, 1);
                    if (line.Contains(" - ")) new_alph[k] = line.Substring(line.IndexOf('-') + 3, 1);
                    line = key.ReadLine();
                }
                Console.WriteLine("Введите путь к файлу, в который сохранить текст, должен содержать расширение '.encode'");
                string СrText = Console.ReadLine();
                while (Path.GetExtension(СrText) != ".encode")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой файл");
                    СrText = @Console.ReadLine();
                }
                using (StreamWriter writer = File.CreateText(СrText))
                {
                    Console.WriteLine("Шифротекст был сохранён в файле " + СrText);
                    writer.WriteLine("alg: шифр замены\nтекст:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        for (int j = 0; j < old_alph.Length; j++)
                        {
                            char K = Convert.ToChar(old_alph[j]);
                            if (text_array[i] == K || Char.ToLower(text_array[i]) == K || Char.ToUpper(text_array[i])== K) { writer.Write(new_alph[j]); check = true; break; }
                        }
                        if (check == false) writer.Write(text_array[i]);
                        check = false;
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Decrypt_Swap(string Text_Name, string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string check_text=Text.ReadLine();
            Text.ReadLine();
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            string line = key.ReadLine();
            if (line.Contains("шифр замены")&&check_text.Contains("шифр замены"))
            {
                key.ReadLine();
                line = key.ReadLine();
                int a = File.ReadAllLines(key_Name).Length - 2;
                string[] old_alph = new string[a];
                string[] new_alph = new string[a];
                char[] text_array = text.ToCharArray();
                bool check = false;
                for (int k = 0; k < old_alph.Length; k++)
                {
                    if (line.Contains(" - ")) new_alph[k] = line.Substring(line.IndexOf('-') - 3, 1);
                    if (line.Contains(" - ")) old_alph[k] = line.Substring(line.IndexOf('-') + 3, 1);
                    line = key.ReadLine();
                }
                Console.WriteLine("Введите путь к файлу, в который сохранить текст");
                string DecrText = Console.ReadLine();
                using (StreamWriter writer = File.CreateText(DecrText))
                {
                    Console.WriteLine("Рассшифрованный текс был сохранён в файле " + DecrText);
                    writer.WriteLine("alg: шифр замены\nтекст:");
                    for (int i = 0; i < text.Length; i++)
                    {
                        for (int j = 0; j < old_alph.Length; j++)
                        {
                            if (text_array[i] == Convert.ToChar(old_alph[j]) || Char.ToLower(text_array[i]) == Convert.ToChar(old_alph[j])) { writer.Write(new_alph[j]); check = true; break; }
                        }
                        if (check == false) writer.Write(text_array[i]);
                        check = false;
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Crypt_Gamma(string Text_Name,string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            string line = key.ReadLine();
            if (line.Contains("шифр гаммирования"))
            {
                key.ReadLine();
                line = key.ReadLine();
                char[] Gamma = line.ToCharArray();
                int block_size = Gamma.Length;
                int Text_sise = text.Length;
                int[] Gamma_index = new int[block_size];
                int Alph_size = (File.ReadAllLines(key_Name).Length - 3);
                string[] Alph_String = new string[Alph_size];
                char[] Alph = new char[Alph_size];
                for (int i = 0; i < Alph_size; i++)
                {
                    Alph_String[i] = key.ReadLine();
                    Alph[i] = Convert.ToChar(Alph_String[i]);
                }
                for (int j = 0; j < block_size; j++)
                {
                    for (int i = 0; i < Alph_size; i++)
                    {
                        if (Gamma[j] == Alph[i]) { Gamma_index[j] = i + 1; break; }
                    }
                }
                Console.WriteLine("Введите путь к файлу, в который сохранить текст, должен содержать расширение '.encode'");
                string CrText = Console.ReadLine();
                while (Path.GetExtension(CrText) != ".encode")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой файл");
                    CrText = Console.ReadLine();
                }
                Random rnd = new Random();
                while (Text_sise % block_size != 0) { text += text[rnd.Next(0, Text_sise)]; Text_sise++; };
                Console.WriteLine(Text_sise);
                char[] text_array = text.ToCharArray();
                using (StreamWriter writer = File.CreateText(CrText))
                {
                    Console.WriteLine("Шифротекст был сохранён в файле " + CrText);
                    writer.WriteLine("alg: шифр гаммирования\nтекст:");
                    for (int i = 0; i < Text_sise; i += block_size)
                    {
                        char[] block = new char[block_size];

                        for (int j = 0; j < block_size; j++)
                        {
                            int Text_index = 0;
                            bool Check = false;
                            block[j] = text_array[i + j];
                            for (int k = 0; k < Alph_size; k++)
                            {
                                if (block[j] == Alph[k] || Char.ToLower(block[j]) == Alph[k] || Char.ToUpper(block[j]) == Alph[k]) { Text_index = k; Check = true; break; }
                            }
                            if (Check == true) { block[j] = Alph[(Text_index + Gamma_index[j]) % Alph_size]; }
                        }
                        for (int j = 0; j < block_size; j++) { writer.Write(block[j]); }
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Decrypt_Gamma(string Text_Name,string key_Name)
        {
            StreamReader Text = new StreamReader(Text_Name);
            string text_check = Text.ReadLine();
            Text.ReadLine();
            string text = Text.ReadToEnd();
            StreamReader key = new StreamReader(key_Name);
            string line = key.ReadLine();
            if (line.Contains("шифр гаммирования") && text_check.Contains("шифр гаммирования"))
            {
                key.ReadLine();
                line = key.ReadLine();
                char[] Gamma = line.ToCharArray();
                int block_size = Gamma.Length;
                int Text_sise = text.Length;
                int[] Gamma_index = new int[block_size];
                int Alph_size = (File.ReadAllLines(key_Name).Length - 3);
                string[] Alph_String = new string[Alph_size];
                char[] Alph = new char[Alph_size];
                for (int i = 0; i < Alph_size; i++)
                {
                    Alph_String[i] = key.ReadLine();
                    Alph[i] = Convert.ToChar(Alph_String[i]);
                }
                for (int j = 0; j < block_size; j++)
                {
                    for (int i = 0; i < Alph_size; i++)
                    {
                        if (Gamma[j] == Alph[i]) { Gamma_index[j] = i + 1; break; }
                    }
                }
                Console.WriteLine("Введите путь к файлу, в который сохранить текст");
                string CrText = Console.ReadLine();
                Random rnd = new Random();
                while (Text_sise % block_size != 0) { text += text[rnd.Next(0, Text_sise)]; Text_sise++; };
                Console.WriteLine(Text_sise);
                char[] text_array = text.ToCharArray();
                using (StreamWriter writer = File.CreateText(CrText))
                {
                    Console.WriteLine("Расшифрованный текст был сохранён в файле " + CrText);
                    writer.WriteLine("alg: шифр гаммирования\nтекст:");
                    for (int i = 0; i < Text_sise; i += block_size)
                    {
                        char[] block = new char[block_size];

                        for (int j = 0; j < block_size; j++)
                        {
                            int Text_index = 0;
                            bool Check = false;
                            block[j] = text_array[i + j];
                            for (int k = 0; k < Alph_size; k++)
                            {
                                if (block[j] == Alph[k] || Char.ToLower(block[j]) == Alph[k] || Char.ToUpper(block[j]) == Alph[k]) { Text_index = k; Check = true; break; }
                            }
                            if (Check == true) { block[j] = Alph[(Text_index - Gamma_index[j] + Alph_size) % Alph_size]; }
                        }
                        for (int j = 0; j < block_size; j++) { writer.Write(block[j]); }
                    }
                }
            }
            else Console.WriteLine("Ключ не подходит");
        }
        static void Generation_key_cipher_swap()
        {
            Console.WriteLine("Введите путь к файлу с алфавитом, дожен содержать расширение '.alph'");
            string Alf_Name = @Console.ReadLine();
            while (!File.Exists(Alf_Name)|| Path.GetExtension(Alf_Name)!= ".alph")
            {
                Console.WriteLine("Неверный формат файла. Укажите другой");
                Alf_Name = @Console.ReadLine();
            }
            StreamReader reader = new StreamReader(Alf_Name);
            string line = reader.ReadLine();
            bool Check = true;
            int a = File.ReadAllLines(Alf_Name).Length;
            Random rnd = new Random();
            string[] old_alph = new string[a];
            string[] new_alph = new string[a];
            string[] help_alph = new string[a];
            for (int i = 0; i <a; i++)
            {

                if (line.Length > 1) { Console.WriteLine("Алфавит имеет недопустимый вид");Check = false; break; }
                old_alph[i] = line;
                new_alph[i] = line;
                help_alph[i] = line;
                line = reader.ReadLine();
            }
            if (Check == true)
            {
                for (int i = 0; i < a; i++)
                {
                    int Rnd = rnd.Next(i, a);
                    new_alph[i] = help_alph[Rnd];
                    string help = help_alph[Rnd];
                    help_alph[Rnd] = help_alph[i];
                    help_alph[i] = help;
                }
                Console.WriteLine("Укажите путь к файлу для хранения полученного ключа, дожен содержать расширение '.key' ");
                string key_Name = @Console.ReadLine();
                while (Path.GetExtension(key_Name) != ".key")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой файл");
                    key_Name = @Console.ReadLine();
                }

                using (StreamWriter writer = File.CreateText(key_Name))
                {
                    writer.WriteLine("alg: шифр замены\nkey:");
                    for (int i = 0; i < a; i++)
                    {
                        writer.WriteLine("\"" + old_alph[i] + "\" - \"" + new_alph[i] + "\"");
                    }
                    Console.WriteLine("Ключ сгенерирован");
                }
            }

        }
        static void Generation_key_cipher_gamma()
        {
            Console.WriteLine("Введите путь к файлу с алфавитом, дожен содержать расширение '.alph'");
            string Alf_Name = @Console.ReadLine();
            while (!File.Exists(Alf_Name) || Path.GetExtension(Alf_Name) != ".alph")
            {
                Console.WriteLine("Неверный формат файла. Укажите другой");
                Alf_Name = @Console.ReadLine();
            }
            int Alph_size = File.ReadAllLines(Alf_Name).Length;
            StreamReader reader = new StreamReader(Alf_Name);
            string line = reader.ReadLine();
            bool Check = true;
            string[] Alph = new string[Alph_size];
            for (int i = 0; i < Alph_size; i++)
            {
                if (line.Length > 1) { Console.WriteLine("Алфавит имеет недопустимый вид"); Check = false; break; }
                Alph[i] = line;
                line = reader.ReadLine();
            }
            if (Check == true)
            {
                Console.WriteLine("Укажите размер блока ключа");
                int block_size = Incorrect_value(Console.ReadLine());
                while (block_size <= 1)
                {
                    Console.WriteLine("Несоответствующий разменр блока");
                    block_size = Incorrect_value(Console.ReadLine());
                }
                Random rnd = new Random();
                Console.WriteLine("Укажите путь к файлу для хранения полученного ключа, дожен содержать расширение '.key' ");
                string key_Name = @Console.ReadLine();
                while (Path.GetExtension(key_Name) != ".key")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой файл");
                    key_Name = @Console.ReadLine();
                }
                using (StreamWriter writer = File.CreateText(key_Name))
                {
                    writer.WriteLine("alg: шифр гаммирования\nkey:");
                    for (int i = 0; i < block_size; i++) {
                        writer.Write(Alph[rnd.Next(1, Alph_size)]);
                    }
                    writer.WriteLine();
                    for(int i = 0; i < Alph_size; i++)
                    {
                        writer.WriteLine(Alph[i]);
                    }
                    Console.WriteLine("Ключ сгенерирован");
                }
            }
        }
        static void Generation_key_cipher_move()
        {
            Console.WriteLine("Укажите размер блока ключа");
            int block_size = Incorrect_value(Console.ReadLine());
            while (block_size <= 1) {
                Console.WriteLine("Несоответствующий разменр блока");
                block_size = Incorrect_value(Console.ReadLine());
            }
            int[] block = new int[block_size];
            int[] help = new int[block_size];
            for(int i = 0; i < block_size; i++)
            {
                help[i] = i + 1;
            }
            Random rand = new Random();
            Console.WriteLine("Укажите путь к файлу для хранения полученного ключа, дожен содержать расширение '.key' ");

                string key_Name = @Console.ReadLine();
                while (Path.GetExtension(key_Name) != ".key")
                {
                    Console.WriteLine("Неверный формат файла. Укажите другой");
                    key_Name = @Console.ReadLine();
                }
                using (StreamWriter writer = File.CreateText(key_Name))
                {
                    writer.WriteLine("alg: шифр перестановки\nkey:");
                    for (int i = 0; i < block_size; i++)
                    {
                        Random rnd = new Random();
                        int Rnd = rnd.Next(i, block_size);
                        block[i] = help[Rnd];
                        int h = help[Rnd];
                        help[Rnd] = help[i];
                        help[i] = h;
                    }
                    for (int i = 0; i < block_size; i++)
                    {
                        writer.WriteLine(block[i]);
                    }
                }
                Console.WriteLine("Ключ успешно сгенерирован");
        }
        static void Main(string[] args)
        {
            while (true) { 
            Console.WriteLine(".>> Главное меню:\n" +
                "\t >>1. Зашифровать/расшифровать\n" +
                "\t >>2. Сгенерировать ключ\n");
            int Choise = Incorrect_value(Console.ReadLine());
            while ((Choise != 1) & (Choise != 2))
            {
                Console.WriteLine("Выберите 1 или 2 !!!");
                Choise = Incorrect_value(Console.ReadLine());
            }
                if (Choise == 1) //Зашифровать, расшифровать
                {
                    Console.WriteLine(".>> Зашифровать/расшифровать\n" +
                        "\t >>1. Зашифровать\n" +
                        "\t >>2. Расшифровать\n");
                    Choise = Incorrect_value(Console.ReadLine());
                    while ((Choise != 1) & (Choise != 2))
                    {
                        Console.WriteLine("Выберите 1 или 2 !!!");
                        Choise = Incorrect_value(Console.ReadLine());
                    }
                    if (Choise == 1)//Зашифровать
                    {
                        try
                        {
                            Console.WriteLine("Укажите путь к тексту, который хотите зашифровать,  дожен содержать расширение '.txt'\n");
                            string Text_Name = @Console.ReadLine();
                            while (!File.Exists(Text_Name) || Path.GetExtension(Text_Name) != ".txt")
                            {
                                Console.WriteLine("Неверный формат файла. Укажите другой");
                                Text_Name = @Console.ReadLine();
                            }
                            Console.WriteLine("Укажите файл ключа, он должен содержать наименование шифра, дожен содержать расширение '.key'");
                            string key_Name = @Console.ReadLine();
                            while (Path.GetExtension(key_Name) != ".key")
                            {
                                Console.WriteLine("Неверный формат файла. Укажите другой файл");
                                key_Name = @Console.ReadLine();
                            }
                            Console.WriteLine(".>> Выберите метод шифровки\n" + "\t >>1. Применить шифр замены\n" + "\t >>2. Применить шифр перестановки\n"+"\t >>3. Применить шифр гаммирования\n");
                            Choise = Incorrect_value(Console.ReadLine());
                            while ((Choise != 1) & (Choise != 2)&(Choise!=3))
                            {
                                Console.WriteLine("Выберите 1, 2 или 3!!!");
                                Choise = Incorrect_value(Console.ReadLine());
                            }
                            switch (Choise)
                            {
                                case 1:
                                    Crypt_Swap(Text_Name, key_Name);
                                    break;
                                case 2:
                                    Crypt_Move(Text_Name, key_Name);
                                    break;
                                case 3:
                                    Crypt_Gamma(Text_Name, key_Name);
                                    break;
                            }
                        }
                        catch { Console.WriteLine("Неверный формат файла. Укажите другой файл (в Main)"); }
                    }

                    else//Расшифровать
                    {
                        try {
                            Console.WriteLine("Укажите путь к тексту, который хотите расшифровать, дожен содержать расширение '.encode'\n");
                            string Text_Name = @Console.ReadLine();
                            while (!File.Exists(Text_Name) || Path.GetExtension(Text_Name) != ".encode")
                            {
                                Console.WriteLine("Неверный формат файла. Укажите другой");
                                Text_Name = @Console.ReadLine();
                            }
                            Console.WriteLine("Укажите файл ключа, он должен содержать название метода шифрования, дожен содержать расширение '.key'");
                            string key_Name = @Console.ReadLine();
                            while (Path.GetExtension(key_Name) != ".key")
                            {
                                Console.WriteLine("Неверный формат файла. Укажите другой файл");
                                key_Name = @Console.ReadLine();
                            }
                            Console.WriteLine(".>> Выберите метод расшифровки\n" + "\t >>1. Шифр замены\n" + "\t >>2. Шифр перестановки\n" + "\t >>3. Применить шифр гаммирования\n");
                            Choise = Incorrect_value(Console.ReadLine());
                            while ((Choise != 1) & (Choise != 2) & (Choise != 3))
                            {
                                Console.WriteLine("Выберите 1, 2 или 3!!!");
                                Choise = Incorrect_value(Console.ReadLine());
                            }
                            switch (Choise)
                            {
                                case 1:
                                    Decrypt_Swap(Text_Name, key_Name);
                                    break;
                                case 2:
                                    Decrypt_Move(Text_Name, key_Name);
                                    break;
                                case 3:
                                    Decrypt_Gamma(Text_Name, key_Name);
                                    break;
                            }
                        }
                        catch { Console.WriteLine("Неверный формат файла. Укажите другой файл в Main"); }
                } }

                else
                {
                    Console.WriteLine(".>> Сгенерировать ключ для следующего алгоритма:\n" +
                        "\t >>1. Шифр замены\n" +
                        "\t >>2. Шифр перестановки\n"+"\t >>3. Шифр гаммирования\n");
                    Choise = Incorrect_value(Console.ReadLine());
                    while ((Choise != 1) & (Choise != 2)&(Choise!=3))
                    {
                        Console.WriteLine("Выберите 1, 2 или 3!!!");
                        Choise = Incorrect_value(Console.ReadLine());
                    }
                    switch (Choise)
                    {
                        case 1:
                            Generation_key_cipher_swap();
                            break;
                        case 2:
                            Generation_key_cipher_move();
                            break;
                        case 3:
                            Generation_key_cipher_gamma();
                            break;
                    }
                }
            }
        }
    }
}
//C:\Users\Геннадий\Desktop\my_alphabet.alph
//C:\Users\Геннадий\Desktop\my_key.key
//C:\Users\Геннадий\Desktop\my_text.txt
//C:\Users\Геннадий\Desktop\my_alphabet.alph