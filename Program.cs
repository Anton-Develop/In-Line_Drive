using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using BeOpen.Devices.ItLineScoreboards.NetworkCommunication;
using BeOpen.Devices.ItLineScoreboards.NetworkCommunication.Data;
using System.Linq;

namespace In_Line_Drive
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            
                //создание объекта для отправки данных на табло(в конструктор передается порт для открытия сокета)
                int numberport = 0;
                Console.WriteLine("Введите номер порта");
                numberport=Console.Read();
                
                ScoreboardSender sender = new ScoreboardSender(numberport);
            try
            {
                //поиск табло возвращает словарь с табло(key - MAC)
                var scoreboards = sender.FindScoreboards();
                foreach (var scoreboard in scoreboards)
                {
                    Console.WriteLine($"Найдено табло:{scoreboard.Value}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Поиск табло неудачно"+ex);
            }
            try
            {
                int brigthness = 100;
                //пример установки яркости табло(практически везде можно передать target для установки, это mac).
                //Если передать null запрос будет широковещательным на всю сеть
                sender.SetBrightness(brigthness, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Яркость неудачно" + ex);
            }
            try
            {
                List<StringMessage> stringMessages = new List<StringMessage>();
                stringMessages.Add(new StringMessage(0, StringProfile.StrLeft, "ПП"));

                //Console.WriteLine("Введите MAC Adress In-Line");
                // PhysicalAddress physicalAddress= PhysicalAddress.Parse(Console.Read().ToString());
                sender.SetRstrData(stringMessages, null);



            }
            catch (Exception ex)
            {
                Console.WriteLine("Rstr неудачно" + ex);
            }
            try
            {
                //пример установки ячейки RSTR(передается индекс ячейки в табло, выравнивание строки и сама строка) + target
                sender.SetNvstrData(new StringMessage(0, StringProfile.StrCenter, "ИЖ0"), null);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Nvstr неудачно" + ex);
            }
            
        }
        

    }
    
}
