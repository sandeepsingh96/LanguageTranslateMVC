using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LanguageTranslateMVC
{
    public class LanguageController:ILanguageController
    {
        private readonly ILanguageTranslatorModel _model;
        private readonly ILanguageUI _view;

        public LanguageController(ILanguageTranslatorModel model, ILanguageUI view)
        {
            _model = model;
            _view = view;
        }
        public async Task ControllerStart()
        {
            _view.Welcome();
            _view.Options();
            string pattern = "^[1-9]+$";
            Regex regex = new Regex(pattern);
            string selectedOption = Console.ReadLine();
            if (string.IsNullOrEmpty(selectedOption) && !regex.IsMatch(selectedOption))
            {
                Console.WriteLine("Please enter valid input");
                selectedOption = Console.ReadLine();
            }

            while (selectedOption != "4")
            {
                switch (selectedOption)
                {
                    case "1":
                        // Display available languages
                       await _model.FetchLanguageListAsync();
                        _view.Options();
                        selectedOption= Console.ReadLine();
                        break;
                    case "2":
                        // Ask for user input and detect language
                        _view.DetectLanguage();
                        string TextToInput = Console.ReadLine();
                        if(string.IsNullOrEmpty(TextToInput))
                        {
                            Console.WriteLine("Please enter valid input");
                            TextToInput= Console.ReadLine();
                        }
                        await _model.DetectLanguageAsync(TextToInput);
                        _view.Options();
                        selectedOption = Console.ReadLine();
                        break;
                    case "3":
                        // Ask for user input and translate text
                        _view.AskTargetLang();
                        string givenTargettedLanguage = Console.ReadLine();
                        if (string.IsNullOrEmpty(givenTargettedLanguage) || _model.ListCheck(givenTargettedLanguage) == false)
                        {
                            Console.WriteLine("Please enter valid input");
                            givenTargettedLanguage = Console.ReadLine();
                        }
                        _view.AskSourceLang();
                        string givenSourceLanguage = Console.ReadLine();
                        if (string.IsNullOrEmpty(givenSourceLanguage) || !_model.ListCheck(givenSourceLanguage))
                        {
                            Console.WriteLine("Please enter valid input");
                            givenSourceLanguage = Console.ReadLine();
                        }
                        _view.AskText();
                        string givenText = Console.ReadLine();
                        if (string.IsNullOrEmpty(givenText))
                        {
                            Console.WriteLine("Please enter valid input");
                            givenText = Console.ReadLine();
                        }
                        await _model.TranslateLanguageAsync(givenText, givenTargettedLanguage, givenSourceLanguage);
                        _view.Options();
                        selectedOption = Console.ReadLine();
                        break;
                    case "4":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        _view.Options();
                        selectedOption = Console.ReadLine();


                        break;
                }

            }


        }
    }
}
