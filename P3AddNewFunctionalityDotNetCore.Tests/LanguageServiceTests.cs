using Xunit;
using P3AddNewFunctionalityDotNetCore.Models.Services;

namespace P3AddNewFunctionalityDotNetCore.Tests{

    public class LanguageServiceTests{

        [Fact]
        public void ChangeCulture(){
            ILanguageService languageService = new LanguageService();
            string language = "Spanish";

            string culture = languageService.SetCulture(language);

            Assert.Same("es", culture);
        }
    }
}