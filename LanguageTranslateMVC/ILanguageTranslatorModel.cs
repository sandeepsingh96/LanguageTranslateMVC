using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LanguageTranslateMVC
{
    public interface ILanguageTranslatorModel
    {
        bool ListCheck(string inputLang);
         Task FetchLanguageListAsync();
         Task DetectLanguageAsync(string inputText);
         Task TranslateLanguageAsync(string inputText, string targetLang, string sourceLang);
    }
}
