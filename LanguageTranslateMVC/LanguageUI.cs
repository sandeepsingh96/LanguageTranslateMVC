using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageTranslateMVC
{
    public class LanguageUI:ILanguageUI
    {
        public void Welcome() {

            Console.WriteLine("you are welcome to your own translator application");
        }
        public void Options() { Console.WriteLine("Please select anyone option");
            Console.WriteLine("Press 1 to list all languages");
            Console.WriteLine("Press 2 to detect the language");
            Console.WriteLine("Press 3 to translate language");
            Console.WriteLine("Press 4 to exit the application");
        
        }
        public void DetectLanguage()
        {
            Console.WriteLine("Enter text to detect language");
        }
        public void AskText()
        {
            Console.WriteLine("Please enter the text to translate");
        }
        public void AskTargetLang()
        {
            Console.WriteLine("Please enter the tareget language code from list");

        }
        public void AskSourceLang() { Console.WriteLine("Please enter the source language code from list"); }

    }
}
