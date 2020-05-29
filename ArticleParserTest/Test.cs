using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRLayerCheck;
using System.Text;

namespace ArticleParserTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void ArticleTest()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            log.CreateLogFile();

            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Bulletin de correspondance hellenique moderne et contemporain 1 | 2019 " +
                "Le mariage dans le pourtour mediterraneen de l’Europe " +
                "- Controverse The Byzantines between Civil and Sacramental Marriage " +
                "Les Byzantins entre mariage civil et mariage religieux " +
                "Katerina Nikolaou " +
                "Electronic version URL: http://journals.openedition.org/bchmc/285 Publisher Ecole francaise d'Athenes " +
                "Electronic reference " +
                "Katerina Nikolaou, " +
                "« The Byzantines between Civil and Sacramental Marriage », " +
                "Bulletin de correspondance hellenique moderne et contemporain[Online], " +
                "1 | 2019, Online since 01 December 2019, connection on 20 December 2019." +
                "URL : http://journals.openedition.org/bchmc/285 " +
                "Bulletin de correspondance hellenique\n moderne et contemporain hellenique moderne et contemporain");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Katerina Nikolaou";
            expectedArticle.Title = "The Byzantines between Civil and Sacramental Marriage";
            expectedArticle.Year = "2019";
            expectedArticle.Journal.Title = "BCHmc";
            expectedArticle.Journal.Number = "2019";
            expectedArticle.Journal.Volume = "1";
            expectedArticle.IsBook = false;
            AssertAll(expectedArticle, actualArticle);
        }

        [TestMethod]
        public void ArticleTest2()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            log.CreateLogFile();

            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Inquisition et societe au Mexique " +
                "1571 - 1700 " +
                "Solange Alberro " +
                "DOI: 10.4000 / books.cemca.2601 " +
                "Editeur: Centro de estudios mexicanos y centroamericanos " +
                "Annee d'edition : 1988 " +
                "Date de mise en ligne: 2 juin 2014 " +
                "Collection: Hors collection " +
                "ISBN electronique: 9782821846234 " +
                "http://books.openedition.org " +
                "Edition imprimee " +
                "ISBN: 9789686029017 " +
                "Nombre de pages: 489 " +
                "Reference electronique " +
                "ALBERRO, Solange.Inquisition et societe au Mexique: 1571 - 1700.Nouvelle edition[en ligne].Mexico : " +                "Centro de estudios mexicanos y centroamericanos, 1988(genere le 25 avril 2019).Disponible sur" +
                "Internet : < http://books.openedition.org/cemca/2601>. ISBN : 9782821846234. DOI : 10.4000/ " +
                "books.cemca.2601.Ce document a ete genere automatiquement le 25 avril 2019.Il est issu d'une numerisation par " +
                "reconnaissance optique de caracteres. " +
                "© Centro de estudios mexicanos y centroamericanos, 1988 " +
                "Conditions d’utilisation: http://www.openedition.org/6540");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Alberro Solange";
            expectedArticle.Title = "Inquisition et societe au Mexique: 1571 - 170";
            expectedArticle.Year = "2019";
            expectedArticle.Journal.Title = "BCHmc";
            expectedArticle.Journal.Number = "2019";
            expectedArticle.Journal.Volume = "1";
            expectedArticle.IsBook = false;
            AssertAll(expectedArticle, actualArticle);
        }

        [TestMethod]
        public void BookTest()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Qu'est-ce qu'un systeme philosophique ? Cours 2007 et 2008 Jacques Bouveresse DOI: " +
                "10.4000 / books.cdf.1715 Editeur: College de France Annee d'edition : 2012 Date de mise " +
                "en ligne: 4 avril 2013 Collection: Philosophie de la connaissance ISBN electronique: " +
                "9782722601529 http://books.openedition.org " +
                "Reference electronique " +
                "BOUVERESSE, Jacques.Qu'est-ce qu'un systeme philosophique ? Cours 2007 et 2008." +
                "Nouvelle edition [en ligne].Paris : College de France, 2012" +
                "(genere le 02 mai 201 9).Disponible sur Internet : <http://books.openedition.org/cdf/1715>." +
                "ISBN : 9782722601529.DOI : 10.4000 / books.cdf.1715. Ce document a ete genere automatiquement le 2 mai 2019. " +
                "© College de France, 2012 Conditions d’utilisation: http://www.openedition.org/6540");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Bouveresse Jacques";
            expectedArticle.Title = "Qu'est-ce qu'un systeme philosophique";
            expectedArticle.Town = "Paris";
            expectedArticle.Year = "2012";
            expectedArticle.IsBook = true;

            AssertAll(expectedArticle, actualArticle);
        }

        [TestMethod]
        public void BookTest2()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "http://books.openedition.org" +
                "Edición impresa" +
                "ISBN: 9788486839611" +
                "Número de páginas: 537" +
                "Referencia electrónica" +
                "LAGARDÈRE, Vincent. Histoire et société en Occident musulman au Moyen Âge: Analyse du Mi'yār d'al-" +
                "Wanšarīšī. Nueva edición [en línea]. Madrid: Casa de Velázquez, 1995 (generado el 03 juillet 2019)." +
                "Disponible en Internet: <http://books.openedition.org/cvz/2378>. ISBN: 9788490961001." +
                "Este documento fue generado automáticamente el 3 julio 2019. Está derivado de una digitalización" +
                "por un reconocimiento óptico de caracteres." +
                "© Casa de Velázquez, 1995" +
                "Condiciones de uso: http://www.openedition.org/6540");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Lagardère Vincent";
            expectedArticle.Title = "Histoire et société en Occident musulman au Moyen Âge";
            expectedArticle.Town = "Madrid";
            expectedArticle.Year = "1995";
            expectedArticle.IsBook = true;

            AssertAll(expectedArticle, actualArticle);
        }

        [TestMethod]
        public void BookTest3()
        {
            Patterns patterns = new Patterns();
            Log log = new Log();
            ArticleParser articleParser = new ArticleParser(log, patterns);
            Article actualArticle = new Article();
            StringBuilder stringBuilder = new StringBuilder(
                "Inquisition et societe au Mexique" +
                "1571 - 1700 Solange Alberro" +                "DOI: 10.4000 / books.cemca.2601" +                "Editeur: Centro de estudios mexicanos y centroamericanos" +                "Annee d'edition : 1988" +                "Date de mise en ligne: 2 juin 2014" +                "Collection: Hors collection" +                "ISBN electronique: 9782821846234" +                "http://books.openedition.org" +                "Edition imprimee" +                "ISBN: 9789686029017" +                "Nombre de pages: 489" +                "Reference electronique" +                "ALBERRO, Solange.Inquisition et societe au Mexique: 1571-1700.Nouvelle edition[en ligne].Mexico :" +                "Centro de estudios mexicanos y centroamericanos, 1988(genere le 25 avril 2019).Disponible sur" +                "Internet : < http://books.openedition.org/cemca/2601>. ISBN : 9782821846234. DOI : 10.4000/" +                "books.cemca.2601." +                "Ce document a ete genere automatiquement le 25 avril 2019.Il est issu d'une numerisation par" +                "reconnaissance optique de caracteres." +                "© Centro de estudios mexicanos y centroamericanos, 1988" +                "Conditions dТutilisation : " +
                "http://www.openedition.org/6540");
            actualArticle.PdfText = stringBuilder;
            actualArticle = articleParser.ParsePdfText(actualArticle);
            Article expectedArticle = new Article();
            expectedArticle.PdfText = stringBuilder;
            expectedArticle.Autor = "Alberro Solange";
            expectedArticle.Title = "Inquisition et societe au Mexique. 1571-1700";
            expectedArticle.Town = "Mexico";
            expectedArticle.Year = "1988";
            expectedArticle.IsBook = true;

            AssertAll(expectedArticle, actualArticle);
        }

        private void AssertAll(Article expectedArticle, Article actualArticle)
        {
            Assert.AreEqual(expectedArticle.Autor, actualArticle.Autor);
            Assert.AreEqual(expectedArticle.Title, actualArticle.Title);
            Assert.AreEqual(expectedArticle.Town, actualArticle.Town);
            Assert.AreEqual(expectedArticle.Year, actualArticle.Year);
            Assert.AreEqual(expectedArticle.Journal.Title, actualArticle.Journal.Title);
            Assert.AreEqual(expectedArticle.Journal.Number, actualArticle.Journal.Number);
            Assert.AreEqual(expectedArticle.Journal.Volume, actualArticle.Journal.Volume);
            Assert.AreEqual(expectedArticle.IsBook, actualArticle.IsBook);
        }
    }
}